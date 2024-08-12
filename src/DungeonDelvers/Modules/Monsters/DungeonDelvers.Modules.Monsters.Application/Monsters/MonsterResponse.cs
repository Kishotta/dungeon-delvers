using DungeonDelvers.Modules.Monsters.Domain.Monsters;

namespace DungeonDelvers.Modules.Monsters.Application.Monsters;

public sealed record MonsterResponse(
    Guid Id,
    string Name,
    string Size,
    string Type,
    string Alignment,
    int ArmorClass,
    string HitPoints,
    int HitPointsAverage,
    float ChallengeRating)
{
    public static implicit operator MonsterResponse(Monster monster) =>
        new(monster.Id,
            monster.Name,
            Enum.GetName(typeof(Size), monster.Size)!,
            Enum.GetName(typeof(MonsterType), monster.Type)!,
            Enum.GetName(typeof(Alignment), monster.Alignment)!,
            monster.ArmorClass,
            monster.HitPoints.Expression,
            monster.HitPoints.Average,
            monster.ChallengeRating);
};