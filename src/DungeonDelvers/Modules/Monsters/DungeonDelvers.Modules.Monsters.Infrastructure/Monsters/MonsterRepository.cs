using DungeonDelvers.Modules.Monsters.Domain.Monsters;
using DungeonDelvers.Modules.Monsters.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace DungeonDelvers.Modules.Monsters.Infrastructure.Monsters;

internal sealed class MonsterRepository(MonstersDbContext dbContext) : IMonsterRepository
{
    public async Task<Monster?> GetAsync(Guid monsterId, CancellationToken cancellationToken = default) =>
        await dbContext
            .Monsters
            .SingleOrDefaultAsync(monster => monster.Id == monsterId, cancellationToken);

    public void Add(Monster monster)
    {
        dbContext.Monsters.Add(monster);
    }
}