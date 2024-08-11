using System.Reflection;
using DungeonDelvers.Common.Application;
using DungeonDelvers.Common.Application.EventBus;
using DungeonDelvers.Common.Infrastructure.Inbox;
using DungeonDelvers.Common.Infrastructure.Outbox;
using DungeonDelvers.Common.Presentation;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DungeonDelvers.Common.Infrastructure;

public interface IModule
{
    void Configure(IConfigurationBuilder configurationBuilder);
    void ConfigureConsumers(IRegistrationConfigurator registrationConfigurator);
    void Add(IServiceCollection services, IConfiguration configuration, string databaseConnectionString);
    Task ApplyMigrationsAsync(IServiceScope scope);
}

public abstract class Module<
    TDbContext,
    TInboxOptions, 
    TProcessInboxJob, 
    TConfigureProcessInboxJob,
    TOutboxOptions, 
    TProcessOutboxJob, 
    TConfigureProcessOutboxJob>  : IModule
    where TDbContext : DbContext
    where TInboxOptions : InboxOptionsBase 
    where TProcessInboxJob : ProcessInboxJobBase<TInboxOptions>
    where TConfigureProcessInboxJob : ConfigureProcessInboxJobBase<TInboxOptions, TProcessInboxJob>
    where TOutboxOptions : OutboxOptionsBase
    where TProcessOutboxJob : ProcessOutboxJobBase<TOutboxOptions>
    where TConfigureProcessOutboxJob : ConfigureProcessOutboxJobBase<TOutboxOptions, TProcessOutboxJob>
{
    protected abstract string ModuleName { get; }
    protected abstract Assembly DomainAssembly { get; }
    protected abstract Assembly ApplicationAssembly { get; }
    protected abstract Assembly PresentationAssembly { get; }

    protected abstract Type IdempotentDomainEventHandlerType { get; }
    protected abstract Type IdempotentIntegrationEventHandlerType { get; }

    public void Configure(IConfigurationBuilder configurationBuilder)
    {
        var moduleName = ModuleName[..1].ToLower() + ModuleName[1..];
        configurationBuilder.AddJsonFile($"modules.{moduleName}.json", false, true);
        configurationBuilder.AddJsonFile($"modules.{moduleName}.Development.json", false, true);
    }

    public void Add(
        IServiceCollection services,
        IConfiguration configuration,
        string databaseConnectionString)
    {
        services.AddDomainEventHandlers(DomainAssembly, IdempotentDomainEventHandlerType)
            .AddUseCases(ApplicationAssembly)
            .AddEndpoints(PresentationAssembly)
            .AddIntegrationEventHandlers(PresentationAssembly, IdempotentIntegrationEventHandlerType)
            .AddInbox<TInboxOptions, TConfigureProcessInboxJob>(configuration.GetSection($"{ModuleName}:Inbox"))
            .AddOutbox<TOutboxOptions, TConfigureProcessOutboxJob>(configuration.GetSection($"{ModuleName}:Outbox"));
        
        ConfigureDatabase(services, databaseConnectionString);
        ConfigureInfrastructure(services, configuration);
    }

    protected abstract void ConfigureDatabase(IServiceCollection services, string connectionString);

    protected virtual void ConfigureInfrastructure(IServiceCollection services, IConfiguration configuration)
    {
        // Default infrastructure service configuration
    }

    public void ConfigureConsumers(IRegistrationConfigurator registrationConfigurator)
    {
        ConfigureConsumers(registrationConfigurator, PresentationAssembly);
    }

    private void ConfigureConsumers(IRegistrationConfigurator registrationConfigurator, Assembly presentationAssembly)
    {
        presentationAssembly.GetTypes()
            .Where(type => type.IsAssignableTo(typeof(IIntegrationEventHandler)))
            .Select(integrationEventHandlerType =>
            {
                var integrationEventType = integrationEventHandlerType
                    .GetInterfaces()
                    .Single(@interface => @interface.IsGenericType)
                    .GetGenericArguments()
                    .Single();
                
                return typeof(IntegrationEventConsumer<>).MakeGenericType(integrationEventType);
            })
            .ToList()
            .ForEach(consumerType =>
            {
                registrationConfigurator.AddConsumer(consumerType);
            });
    }

    public async Task ApplyMigrationsAsync(IServiceScope scope)
    {
        await scope.InstallPostgresExtensionAsync("pg_trgm");
        scope.ApplyMigrations<TDbContext>();
    }
}