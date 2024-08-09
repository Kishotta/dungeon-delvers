namespace DungeonDelvers.Api.Extensions;

public static class ApiExtensions
{
    internal static IServiceCollection AddOpenApi(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.CustomSchemaIds(t => t.FullName?.Replace("+", "."));
        });
        
        return services;
    }
}