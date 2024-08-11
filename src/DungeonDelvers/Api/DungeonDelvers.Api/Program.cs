using DungeonDelvers.Api.Extensions;
using DungeonDelvers.Common.Presentation;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var databaseConnectionString = builder.Configuration.GetConnectionString("Database")!;
var cacheConnectionString = builder.Configuration.GetConnectionString("Cache")!;
var keycloakHealthUrl = builder.Configuration.GetValue<string>("KeyCloak:HealthUrl")!;

builder.AddLogging();

builder.Services
    .AddExceptionHandling()
    .AddCorsPolicy(builder.Configuration)
    .AddOpenApi()
    .AddModules(
        builder.Configuration,
        databaseConnectionString,
        cacheConnectionString)
    .AddHealthChecks()
    .AddNpgSql(databaseConnectionString)
    .AddRedis(cacheConnectionString)
    .AddUrlGroup(new Uri(keycloakHealthUrl), HttpMethod.Get, "Keycloak");

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.ApplyMigrations();

app.MapEndpoints();

app.MapHealthChecks("healthz", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseSerilogRequestLogging();

app.UseExceptionHandler();

app.UseCors();

app.UseAuthentication();

app.UseAuthorization();

await app.RunAsync();