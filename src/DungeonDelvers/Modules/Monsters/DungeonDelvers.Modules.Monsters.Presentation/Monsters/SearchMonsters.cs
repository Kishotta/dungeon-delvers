using DungeonDelvers.Common.Domain;
using DungeonDelvers.Common.Presentation.ApiResults;
using DungeonDelvers.Common.Presentation.Endpoints;
using DungeonDelvers.Modules.Monsters.Application.Monsters.CreateMonster;
using DungeonDelvers.Modules.Monsters.Application.Monsters.GetMonsters;
using DungeonDelvers.Modules.Monsters.Application.Monsters.SearchMonsters;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace DungeonDelvers.Modules.Monsters.Presentation.Monsters;

internal sealed class SearchMonsters : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/monsters/search", async (string search, ISender sender) =>
            {
                var result = await sender.Send(new SearchMonstersQuery(search));
                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithName(nameof(SearchMonsters))
            .WithTags(Tags.Monsters);
    }
}