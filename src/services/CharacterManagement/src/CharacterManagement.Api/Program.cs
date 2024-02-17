using CharacterManagement.Api;
using CharacterManagement.Presentation;

var builder = WebApplication.CreateBuilder (args);

builder.Services
       .AddAuth (builder.Configuration)
       .AddPresentation ();

builder.Services.AddHealthChecks ();

var app = builder.Build ();

app.UsePresentation ()
   .UseAuth()
   .UseHttpsRedirection ()
   .UseHealthChecks("/healthz");

app.Run ();
