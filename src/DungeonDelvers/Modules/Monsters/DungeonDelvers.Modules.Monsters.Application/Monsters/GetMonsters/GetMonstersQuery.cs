using DungeonDelvers.Common.Application.Messaging;
using DungeonDelvers.Modules.Monsters.Application.Monsters.CreateMonster;

namespace DungeonDelvers.Modules.Monsters.Application.Monsters.GetMonsters;

public sealed record GetMonstersQuery : IQuery<IEnumerable<MonsterResponse>>;