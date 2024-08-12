using DungeonDelvers.Common.Application.Messaging;
using DungeonDelvers.Modules.Monsters.Domain.Monsters;

namespace DungeonDelvers.Modules.Monsters.Application.Monsters.CreateMonster;

public sealed record CreateMonsterCommand(
    bool Official,
    string Name,
    Size Size,
    MonsterType Type,
    Alignment Alignment,
    int ArmorClass,
    string HitPoints,
    float ChallengeRating) : ICommand<MonsterResponse>;