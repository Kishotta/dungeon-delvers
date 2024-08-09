using DungeonDelvers.Common.Domain;
using DungeonDelvers.Common.Domain.Auditing;

namespace DungeonDelvers.Modules.Monsters.Domain.Monsters;

[Auditable]
public class Monster : Entity
{
    public string Name { get; private set; } = string.Empty;
    
    private Monster() { }

    public static Monster Create(string name)
    {
        var monster = new Monster
        {
            Id = Guid.NewGuid(),
            Name = name
        };

        return monster;
    }
}