using DungeonDelvers.Modules.Monsters.Domain.Monsters;

namespace DungeonDelvers.Modules.Monsters.Application.Monsters.CreateMonster;

public sealed record MonsterResponse(
    Guid Id,
    string Name,
    string HitPoints,
    int HItPointsMinimum,
    int HitPointsMaximum,
    int HitPointsAverage)
{
    public static implicit operator MonsterResponse(Monster monster) =>
        new(monster.Id,
            monster.Name,
            monster.HitPoints.Expression,
            monster.HitPoints.Minimum,
            monster.HitPoints.Maximum,
            monster.HitPoints.Average);
};