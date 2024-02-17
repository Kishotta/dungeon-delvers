using CharacterManagement.Api.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CharacterManagement.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddAuth (this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<IdentityProviderOptions> ()
                .Bind (configuration.GetSection (IdentityProviderOptions.Section))
                .ValidateDataAnnotations ();

        var identityProviderOptions = services.BuildServiceProvider()
                                              .GetRequiredService<IOptions<IdentityProviderOptions>>()
                                              .Value;
        services.AddAuthentication ()
               .AddJwtBearer (options =>
                {
                    var authority = identityProviderOptions.Authority;
                    options.MetadataAddress      = $"{authority}/.well-known/openid-configuration";
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                                                        {
                                                            ValidateIssuerSigningKey = false,
                                                            ValidateIssuer           = true,
                                                            ValidateAudience         = true,
                                                            ValidateLifetime         = true,
                                                            ValidIssuer = identityProviderOptions.Issuer,
                                                            ValidAudience = identityProviderOptions.Audience
                                                        };
                });
        services.AddAuthorizationBuilder ();

        return services;
    }

    public static IApplicationBuilder UseAuth (this IApplicationBuilder app)
    {
        app.UseAuthentication ();
        app.UseAuthorization ();

        return app;
    }
}
