using DungeonDelvers.Common.Application.Messaging;
using DungeonDelvers.Modules.Monsters.Application.Monsters.CreateMonster;

namespace DungeonDelvers.Modules.Monsters.Application.Monsters.SearchMonsters;

public sealed record SearchMonstersQuery(string Search) : IQuery<IEnumerable<MonsterResponse>>;