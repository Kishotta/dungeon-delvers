using Dapper;
using DungeonDelvers.Common.Application.Data;
using DungeonDelvers.Common.Application.Messaging;
using DungeonDelvers.Common.Domain;
using DungeonDelvers.Modules.Monsters.Application.Monsters.CreateMonster;

namespace DungeonDelvers.Modules.Monsters.Application.Monsters.SearchMonsters;

internal sealed class SearchMonstersQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<SearchMonstersQuery, IEnumerable<MonsterResponse>>
{
    public async Task<Result<IEnumerable<MonsterResponse>>> Handle(SearchMonstersQuery request, CancellationToken cancellationToken)
    {
        await using var dbConnection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                 id AS {nameof(MonsterResponse.Id)},
                 name AS {nameof(MonsterResponse.Name)},
                 hit_points_expression AS {nameof(MonsterResponse.HitPoints)},
                 hit_points_average AS {nameof(MonsterResponse.HitPointsAverage)}
             FROM monsters.monsters
             WHERE WORD_SIMILARITY(name, @Search) > 0.3
             ORDER BY WORD_SIMILARITY(name, @Search) DESC
             """;

        var monsters = await dbConnection.QueryAsync<MonsterResponse>(sql, request);

        return Result.Success(monsters);
    }
}