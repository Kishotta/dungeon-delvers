using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CharacterManagement.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation (this IServiceCollection services)
    {
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

         (app as WebApplication)!.MapControllers ();

         return app;
     }
}
