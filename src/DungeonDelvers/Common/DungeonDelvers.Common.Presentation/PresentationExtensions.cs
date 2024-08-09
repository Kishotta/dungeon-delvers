using System.Reflection;
using DungeonDelvers.Common.Presentation.Endpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DungeonDelvers.Common.Presentation;

public static class PresentationExtensions
{
    public static IServiceCollection AddEndpoints(
        this IServiceCollection services, 
        Assembly presentationAssembly)
    {
        var serviceDescriptors = presentationAssembly
            .GetTypes()
            .Where(type => type is { IsAbstract: false, IsInterface: false } &&
                           type.IsAssignableTo(typeof(IEndpoint)))
            .Select(type => ServiceDescriptor.Transient(typeof(IEndpoint), type))
            .ToArray();
        
        services.TryAddEnumerable(serviceDescriptors);

        return services;
    }

    public static IApplicationBuilder MapEndpoints(
        this WebApplication app,
        RouteGroupBuilder? routeGroupBuilder = null)
    {
        IEndpointRouteBuilder builder = routeGroupBuilder is null ? 
            app : 
            routeGroupBuilder;

        app.Services
            .GetRequiredService<IEnumerable<IEndpoint>>()
            .ToList()
            .ForEach(endpoint => endpoint.MapEndpoint(builder));

        return app;
    }
}