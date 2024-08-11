using DungeonDelvers.Common.Application.Messaging;

namespace DungeonDelvers.Modules.Monsters.Application.Monsters.CreateMonster;

public sealed record CreateMonsterCommand(
    bool Official,
    string Name,
    string HitPoints,
    float ChallengeRating) : ICommand<MonsterResponse>;