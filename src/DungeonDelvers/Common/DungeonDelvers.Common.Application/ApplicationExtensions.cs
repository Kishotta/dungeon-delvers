using System.Reflection;
using DungeonDelvers.Common.Application.Behaviors;
using DungeonDelvers.Common.Application.EventBus;
using DungeonDelvers.Common.Application.Messaging;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DungeonDelvers.Common.Application;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplicationUseCasePipeline(this IServiceCollection services) =>
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(AssemblyReference.Assembly);
            
            configuration.AddOpenBehavior(typeof(ExceptionHandlingPipelineBehavior<,>));
            configuration.AddOpenBehavior(typeof(RequestLoggingPipelineBehavior<,>));
            configuration.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
        });

    public static IServiceCollection AddDomainEventHandlers(
        this IServiceCollection services, 
        Assembly domainAssembly,
        Type idempotentDomainEventHandlerType)
    {
        domainAssembly.GetTypes()
            .Where(type => type.IsAssignableTo(typeof(IDomainEventHandler)))
            .ToList()
            .ForEach(domainEventHandlerType =>
            {
                services.TryAddScoped(domainEventHandlerType);

                var domainEventType = domainEventHandlerType
                    .GetInterfaces()
                    .Single(@interface => @interface.IsGenericType)
                    .GetGenericArguments()
                    .Single();

                var closedIdempotentHandlerType = idempotentDomainEventHandlerType.MakeGenericType(domainEventType);

                services.Decorate(domainEventHandlerType, closedIdempotentHandlerType);
            });

        return services;
    }

    public static IServiceCollection AddUseCases(
        this IServiceCollection services,
        Assembly applicationAssembly)
    {
        services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(applicationAssembly));
        services.AddValidatorsFromAssembly(applicationAssembly, includeInternalTypes: true);

        return services;
    }

    public static IServiceCollection AddIntegrationEventHandlers(
        this IServiceCollection services,
        Assembly presentationAssembly,
        Type idempotentIntegrationEventHandlerType)
    {
        presentationAssembly.GetTypes()
            .Where(type => type.IsAssignableTo(typeof(IIntegrationEventHandler)))
            .ToList()
            .ForEach(integrationEventHandlerType =>
            {
                services.TryAddScoped(integrationEventHandlerType);
                
                var integrationEventType = integrationEventHandlerType
                    .GetInterfaces()
                    .Single(@interface => @interface.IsGenericType)
                    .GetGenericArguments()
                    .Single();

                var closedIdempotentHandlerType =
                    idempotentIntegrationEventHandlerType.MakeGenericType(integrationEventType);
                
                services.Decorate(integrationEventHandlerType, closedIdempotentHandlerType);
            });
        return services;
    }
}