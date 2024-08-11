using DungeonDelvers.Common.Application.Messaging;
using DungeonDelvers.Modules.Monsters.Application.Monsters.CreateMonster;

namespace DungeonDelvers.Modules.Monsters.Application.Monsters.UpdateMonster;

public sealed record UpdateMonsterCommand(
    Guid MonsterId,
    string Name, 
    string HitPoints,
    float ChallengeRating) : ICommand<MonsterResponse>;