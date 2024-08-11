using Dapper;
using DungeonDelvers.Common.Application.Data;
using DungeonDelvers.Common.Application.Messaging;
using DungeonDelvers.Common.Domain;
using DungeonDelvers.Modules.Monsters.Application.Monsters.CreateMonster;

namespace DungeonDelvers.Modules.Monsters.Application.Monsters.GetMonsters;

internal sealed class GetMonstersQueryHandler(
    IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetMonstersQuery, IEnumerable<MonsterResponse>>
{
    public async Task<Result<IEnumerable<MonsterResponse>>> Handle(GetMonstersQuery request, CancellationToken cancellationToken)
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
            """;

        var monsters = await dbConnection.QueryAsync<MonsterResponse>(sql);

        return Result.Success(monsters);
    }
}