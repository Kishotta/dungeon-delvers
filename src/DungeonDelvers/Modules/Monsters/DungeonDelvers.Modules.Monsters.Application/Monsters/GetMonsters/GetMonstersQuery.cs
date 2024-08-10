using DungeonDelvers.Common.Application.Messaging;
using DungeonDelvers.Modules.Monsters.Application.Monsters.CreateMonster;

namespace DungeonDelvers.Modules.Monsters.Application.Monsters.GetMonsters;

public class GetMonstersQuery : IQuery<IEnumerable<MonsterResponse>>;