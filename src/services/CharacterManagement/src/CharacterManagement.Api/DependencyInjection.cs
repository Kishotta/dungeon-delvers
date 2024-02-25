using System.Reflection;
using CharacterManagement.Api.Options;
using CharacterManagement.Api.Persistence;
using CharacterManagement.Api.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using CharacterRepository = CharacterManagement.Api.Repositories.CharacterRepository;

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

    public static IServiceCollection AddPresentation (this IServiceCollection services)
    {
        services.AddCors (options =>
        {
            options.AddPolicy("AllowedOrigins",
                builder =>
                {
                    builder.WithOrigins("http://localhost:4200")
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
        });

        services.AddSwagger ()
                .AddControllers ();

        services.AddProblemDetails ();
        services.AddRouting (options => options.LowercaseUrls = true);

        return services;
    }

    private static IServiceCollection AddSwagger (this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer ();
        services.AddSwaggerGen (options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Character Management API", Version = "v1" });

            options.CustomSchemaIds(type => type.ToString());

            // var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            // var xmlPath = Path.Combine (AppContext.BaseDirectory, xmlFile);
            // options.IncludeXmlComments(xmlPath);

            AddSwaggerAuthentication (options);
        });

        return services;
    }

    private static void AddSwaggerAuthentication (SwaggerGenOptions options)
     {
         options.AddSecurityDefinition ("Bearer", new OpenApiSecurityScheme
                                                  {
                                                      In           = ParameterLocation.Header,
                                                      Description  = "Please enter into field the word 'Bearer' following by space and JWT",
                                                      Name         = "Authorization",
                                                      Type         = SecuritySchemeType.Http,
                                                      BearerFormat = "JWT",
                                                      Scheme       = "Bearer"
                                                  });
         options.AddSecurityRequirement (new OpenApiSecurityRequirement
                                         {
                                             {
                                                 new OpenApiSecurityScheme
                                                 {
                                                     Reference = new OpenApiReference
                                                                 {
                                                                     Type = ReferenceType.SecurityScheme,
                                                                     Id   = "Bearer"
                                                                 }
                                                 },
                                                 new string[] { }
                                             }
                                         });
     }

    public static IApplicationBuilder UsePresentation (this IApplicationBuilder app)
    {
        app.UseSwagger ();
        app.UseSwaggerUI ();

        app.UseCors ("AllowedOrigins");

        (app as WebApplication)!.MapControllers ();

        return app;
    }

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var characterManagementDatabaseConnectionString = configuration.GetConnectionString (nameof (CharacterManagementContext));
        if (characterManagementDatabaseConnectionString is null)
            throw new ArgumentNullException (characterManagementDatabaseConnectionString);

        services.AddDbContext<CharacterManagementContext> (
            options => options.UseNpgsql (characterManagementDatabaseConnectionString));

        services.AddScoped<IUnitOfWork> (serviceProvider => serviceProvider.GetRequiredService<CharacterManagementContext> ());
        services.AddScoped<SourceRepository> ();
        services.AddScoped<RaceRepository> ();
        services.AddScoped<CharacterRepository> ();

        services.AddNpgsqlDataSource (characterManagementDatabaseConnectionString);
        services.AddHealthChecks ()
                .AddNpgSql ();

        return services;
    }
}
