using DungeonDelvers.Common.Domain;
using DungeonDelvers.Common.Domain.Auditing;
using DungeonDelvers.Modules.Monsters.Domain.DiceExpressions;

namespace DungeonDelvers.Modules.Monsters.Domain.Monsters;

[Auditable]
public class Monster : Entity
{
    public string Name { get; private set; } = string.Empty;
    public DiceExpression HitPoints { get; private set; } = default!;
    
    private Monster() { }

    public static Monster Create(string name, DiceExpression hitPoints)
    {
        var monster = new Monster
        {
            Id = Guid.NewGuid(),
            Name = name,
            HitPoints = hitPoints
        };

        monster.RaiseDomainEvent(new MonsterCreatedDomainEvent(monster.Id));
        
        return monster;
    }
}