using DungeonDelvers.Common.Domain;
using DungeonDelvers.Common.Presentation.ApiResults;
using DungeonDelvers.Common.Presentation.Endpoints;
using DungeonDelvers.Modules.Monsters.Application.Monsters.CreateMonster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace DungeonDelvers.Modules.Monsters.Presentation.Monsters;

internal sealed class CreateMonster : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/monsters", async (Request request, ISender sender) =>
            {
                var result = await sender.Send(new CreateMonsterCommand(request.Name));
                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithName(nameof(CreateMonster))
            .WithTags(Tags.Monsters);
    }

    internal sealed record Request(string Name);
}