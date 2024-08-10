using DungeonDelvers.Common.Application.Messaging;

namespace DungeonDelvers.Modules.Monsters.Application.Monsters.CreateMonster;

public sealed record CreateMonsterCommand(string Name, string HitPoints) : ICommand<MonsterResponse>;