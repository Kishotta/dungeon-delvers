namespace DungeonDelvers.Modules.Monsters.Domain.Monsters;

public interface IMonsterRepository
{
    Task<Monster?> GetAsync(Guid monsterId, CancellationToken cancellationToken = default);
    void Add(Monster monster);
}