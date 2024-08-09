using DungeonDelvers.Api.Middleware;

namespace DungeonDelvers.Api.Extensions;

public static class ExceptionExtensions
{
    internal static IServiceCollection AddExceptionHandling(this IServiceCollection services)
    {
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();
        
        return services;
    }
}