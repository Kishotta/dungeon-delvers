using CharacterManagement.Api;
using CharacterManagement.Application;
using CharacterManagement.Infrastructure;
using CharacterManagement.Presentation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder (args);

builder.Services
       .AddAuth (builder.Configuration)
       .AddInfrastructure(builder.Configuration)
       .AddApplication()
       .AddPresentation ();

builder.Services.AddHealthChecks ();

var app = builder.Build ();

app.UsePresentation ()
   .UseAuth()
   .UseHttpsRedirection ()
   .UseHealthChecks("/healthz");

// HACK: Apply migrations on startup
// At some point, this should be handled by a kubernetes init container or something.
using var scope = app.Services.CreateScope ();
var       db    = scope.ServiceProvider.GetRequiredService<CharacterManagementContext> ();
db.Database.Migrate ();

app.Run ();
