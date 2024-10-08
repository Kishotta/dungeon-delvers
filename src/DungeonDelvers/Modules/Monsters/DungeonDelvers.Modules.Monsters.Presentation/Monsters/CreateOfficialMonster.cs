using DungeonDelvers.Common.Domain;
using DungeonDelvers.Common.Presentation.ApiResults;
using DungeonDelvers.Common.Presentation.Endpoints;
using DungeonDelvers.Modules.Monsters.Application.Monsters.CreateMonster;
using DungeonDelvers.Modules.Monsters.Domain.Monsters;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace DungeonDelvers.Modules.Monsters.Presentation.Monsters;

internal sealed class CreateOfficialMonster : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/monsters/official", async (Request request, ISender sender) =>
            {
                var result = await sender.Send(new CreateMonsterCommand(
                    true,
                    request.Name,
                    request.Size,
                    request.Type,
                    request.Alignment,
                    request.ArmorClass,
                    request.HitPoints,
                    request.ChallengeRating));
                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithName(nameof(CreateOfficialMonster))
            .WithTags(Tags.Monsters);
    }

    internal sealed record Request(
        bool Official,
        string Name,
        Size Size,
        MonsterType Type,
        Alignment Alignment,
        int ArmorClass,
        string HitPoints,
        float ChallengeRating);
}