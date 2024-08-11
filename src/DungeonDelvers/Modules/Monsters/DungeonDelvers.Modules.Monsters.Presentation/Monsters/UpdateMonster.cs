using DungeonDelvers.Common.Domain;
using DungeonDelvers.Common.Presentation.ApiResults;
using DungeonDelvers.Common.Presentation.Endpoints;
using DungeonDelvers.Modules.Monsters.Application.Monsters.CreateMonster;
using DungeonDelvers.Modules.Monsters.Application.Monsters.UpdateMonster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace DungeonDelvers.Modules.Monsters.Presentation.Monsters;

internal sealed class UpdateMonster : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/monsters/{id:guid}", async (Guid id, Request request, ISender sender) =>
            {
                var result = await sender.Send(new UpdateMonsterCommand(
                    id,
                    request.Name,
                    request.HitPoints,
                    request.ChallengeRating));
                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithName(nameof(UpdateMonster))
            .WithTags(Tags.Monsters);
    }

    internal sealed record Request(string Name, string HitPoints, float ChallengeRating);
}