using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CharacterManagement.Presentation;

public static class DependencyInjection
{
#pragma warning disable CS1591
    public static IServiceCollection AddPresentation (this IServiceCollection services)
    {
       services.AddSwagger ()
               .AddControllers ();

        services.AddProblemDetails ();
        services.AddRouting (options => options.LowercaseUrls = true);

        return services;
    }
#pragma warning restore CS1591

    private static IServiceCollection AddSwagger (this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer ();
        services.AddSwaggerGen (options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Character Management API", Version = "v1" });

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine (AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);

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

#pragma warning disable CS1591
     public static IApplicationBuilder UsePresentation (this IApplicationBuilder app)
     {
         app.UseSwagger ();
         app.UseSwaggerUI ();

         (app as WebApplication)!.MapControllers ();

         return app;
     }
#pragma warning restore CS1591
}
