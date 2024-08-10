using DungeonDelvers.Common.Application.Messaging;
using DungeonDelvers.Common.Domain;
using DungeonDelvers.Modules.Monsters.Application.Monsters.CreateMonster;
using DungeonDelvers.Modules.Monsters.Domain.Monsters;

namespace DungeonDelvers.Modules.Monsters.Application.Monsters.GetMonsters;

internal sealed class GetMonstersQueryHandler(
    IMonsterRepository monsterRepository)
    : IQueryHandler<GetMonstersQuery, IEnumerable<MonsterResponse>>
{
    public async Task<Result<IEnumerable<MonsterResponse>>> Handle(GetMonstersQuery request, CancellationToken cancellationToken)
    {
        var monsters = await monsterRepository.GetAllAsync(cancellationToken);
            
        var monsterResponses = monsters.Select(monster => (MonsterResponse)monster);

        return Result.Success(monsterResponses);
    }
}