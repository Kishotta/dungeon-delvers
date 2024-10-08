using DungeonDelvers.Common.Application;
using DungeonDelvers.Common.Infrastructure;
using DungeonDelvers.Common.Presentation;
using DungeonDelvers.Modules.Monsters.Infrastructure;

namespace DungeonDelvers.Api.Extensions;

public static class ModuleExtensions
{
    internal static IServiceCollection AddModules(
        this IServiceCollection services,
        IConfigurationManager configuration,
        string databaseConnectionString,
        string cacheConnectionString)
    {
        var modules = GetModules().ToList();
        
        services.AddApplicationUseCasePipeline()
            .AddCommonPresentation()
            .AddCommonInfrastructure(databaseConnectionString, cacheConnectionString, modules);
        
        foreach (var module in modules)
        {
            module.Configure(configuration);
            module.Add(services, configuration, databaseConnectionString);
        }
        
        
        
        return services;
    }
    
    internal static async Task ApplyMigrationsAsync(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        
        var modules = GetModules();
        foreach (var module in modules)
        {
            await module.ApplyMigrationsAsync(scope);
        }
    }
    
    private static IEnumerable<IModule> GetModules()
    {
        return [
            new MonstersModule()
        ];
    }
}