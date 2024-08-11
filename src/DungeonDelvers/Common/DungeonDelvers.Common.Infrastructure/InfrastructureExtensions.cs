using System.Reflection;
using Dapper;
using DungeonDelvers.Common.Application.Caching;
using DungeonDelvers.Common.Application.Clock;
using DungeonDelvers.Common.Application.Data;
using DungeonDelvers.Common.Application.EventBus;
using DungeonDelvers.Common.Infrastructure.Auditing;
using DungeonDelvers.Common.Infrastructure.Authentication;
using DungeonDelvers.Common.Infrastructure.Authorization;
using DungeonDelvers.Common.Infrastructure.Caching;
using DungeonDelvers.Common.Infrastructure.Clock;
using DungeonDelvers.Common.Infrastructure.Data;
using DungeonDelvers.Common.Infrastructure.Inbox;
using DungeonDelvers.Common.Infrastructure.Outbox;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Npgsql;
using Quartz;
using StackExchange.Redis;

namespace DungeonDelvers.Common.Infrastructure;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddCommonInfrastructure(
        this IServiceCollection services,
        string databaseConnectionString,
        string cacheConnectionString,
        IEnumerable<IModule> modules)
    {
        services.AddAuthorizationInternal();
        services.AddAuthenticationInternal();
        
        services.AddAuditing();
        
        var npgsqlDataSource = new NpgsqlDataSourceBuilder(databaseConnectionString).Build();
        services.TryAddSingleton(npgsqlDataSource);

        services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();
        
        services.TryAddSingleton<InsertOutboxMessagesInterceptor>();

        services.TryAddSingleton<IDateTimeProvider, DateTimeProvider>();

        services.AddQuartz(configurator =>
        {
            var scheduler = Guid.NewGuid();
            configurator.SchedulerId = $"default-id-{scheduler}";
            configurator.SchedulerName = $"default-name-{scheduler}";
        });
        
        services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);
        
        try
        {
            IConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect(cacheConnectionString);
            services.TryAddSingleton(connectionMultiplexer);

            services.AddStackExchangeRedisCache(options =>
            {
                options.ConnectionMultiplexerFactory = () => Task.FromResult(connectionMultiplexer);
            });
        }
        catch
        {
            // HACK: Allows application to run without a Redis server. This is useful when, for example, generating a database migration.
            services.AddDistributedMemoryCache();
        }
        
        services.TryAddSingleton<ICacheService, CacheService>();
        services.TryAddSingleton<IEventBus, EventBus.EventBus>();

        services.AddMassTransit(configurator =>
        {
            foreach (var module in modules)
            {
                module.ConfigureConsumers(configurator);
            }
            
            configurator.SetKebabCaseEndpointNameFormatter();
            configurator.UsingInMemory((context, config) =>
            {
                config.ConfigureEndpoints(context);
            });
        });
        
        return services;
    }

    public static IServiceCollection ConfigureConsumers(
        this IServiceCollection services,
        Assembly presentationAssembly) =>
        services.AddMassTransit(registrationConfigurator =>
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
        });

    internal static IServiceCollection AddInbox<TInboxOptions, TConfigureProcessInboxJob>(
        this IServiceCollection services,
        IConfigurationSection section)
        where TInboxOptions : InboxOptionsBase
        where TConfigureProcessInboxJob : class, IConfigureOptions<QuartzOptions> =>
        services.Configure<TInboxOptions>(section)
            .ConfigureOptions<TConfigureProcessInboxJob>();
    
    internal static IServiceCollection AddOutbox<TOutboxOptions, TConfigureProcessOutboxJob>(
        this IServiceCollection services,
        IConfigurationSection section)
        where TOutboxOptions : OutboxOptionsBase
        where TConfigureProcessOutboxJob : class, IConfigureOptions<QuartzOptions> =>
        services.Configure<TOutboxOptions>(section)
            .ConfigureOptions<TConfigureProcessOutboxJob>();

    public static async Task InstallPostgresExtensionAsync(this IServiceScope scope, string extension)
    {
        await using var dbConnection = await scope.ServiceProvider.GetRequiredService<IDbConnectionFactory>().OpenConnectionAsync();
        var sql = $"CREATE EXTENSION IF NOT EXISTS {extension};";

        await dbConnection.ExecuteAsync(sql);
    }
    
    public static void ApplyMigrations<TDbContext>(this IServiceScope scope)
        where TDbContext : DbContext
    {
        using var context = scope.ServiceProvider.GetRequiredService<TDbContext>();
        
        context.Database.Migrate();
    }
}