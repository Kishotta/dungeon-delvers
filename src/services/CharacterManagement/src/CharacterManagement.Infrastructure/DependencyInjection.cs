using CharacterManagement.Domain;
using CharacterManagement.Domain.Characters;
using CharacterManagement.Domain.Sources;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CharacterManagement.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var characterManagementDatabaseConnectionString = configuration.GetConnectionString (nameof (CharacterManagementContext));
        if (characterManagementDatabaseConnectionString is null)
            throw new ArgumentNullException (characterManagementDatabaseConnectionString);

        services.AddDbContext<CharacterManagementContext> (
            options => options.UseNpgsql (characterManagementDatabaseConnectionString));

        services.AddScoped<IUnitOfWork> (serviceProvider => serviceProvider.GetRequiredService<CharacterManagementContext> ());
        services.AddScoped<ISourceRepository, SourceRepository> ();
        services.AddScoped<ICharacterRepository, CharacterRepository> ();

        services.AddNpgsqlDataSource (characterManagementDatabaseConnectionString);
        services.AddHealthChecks ()
                .AddNpgSql ();

        return services;
    }
}
