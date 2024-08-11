using DungeonDelvers.Modules.Monsters.Domain.Monsters;

namespace DungeonDelvers.Modules.Monsters.Application.Monsters;

public sealed record MonsterResponse(
    Guid Id,
    string Name,
    string HitPoints,
    int HitPointsAverage)
{
    public static implicit operator MonsterResponse(Monster monster) =>
        new(monster.Id,
            monster.Name,
            monster.HitPoints.Expression,
            monster.HitPoints.Average);
};