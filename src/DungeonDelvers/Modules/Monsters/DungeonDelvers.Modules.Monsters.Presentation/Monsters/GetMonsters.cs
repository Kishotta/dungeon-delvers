using DungeonDelvers.Common.Domain;
using DungeonDelvers.Common.Presentation.ApiResults;
using DungeonDelvers.Common.Presentation.Endpoints;
using DungeonDelvers.Modules.Monsters.Application.Monsters.CreateMonster;
using DungeonDelvers.Modules.Monsters.Application.Monsters.GetMonsters;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace DungeonDelvers.Modules.Monsters.Presentation.Monsters;

internal sealed class GetMonsters : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/monsters", async (ISender sender) =>
            {
                var result = await sender.Send(new GetMonstersQuery());
                return result.Match(Results.Ok<IEnumerable<MonsterResponse>>, ApiResults.Problem);
            }).WithName(nameof(GetMonsters))
            .WithTags(Tags.Monsters);
    }
}