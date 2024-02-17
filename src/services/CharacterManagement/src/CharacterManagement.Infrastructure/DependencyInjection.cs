using CharacterManagement.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CharacterManagement.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CharacterManagementContext> (options =>
        {
            options.UseNpgsql (configuration.GetConnectionString ("CharacterManagementContext"));
        });

        services.AddScoped<ICharacterRepository, CharacterRepository>();

        return services;
    }
}
