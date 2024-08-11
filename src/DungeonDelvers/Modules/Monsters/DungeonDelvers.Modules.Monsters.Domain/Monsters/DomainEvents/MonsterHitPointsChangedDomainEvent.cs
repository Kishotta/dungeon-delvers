using DungeonDelvers.Common.Domain;
using DungeonDelvers.Modules.Monsters.Domain.DiceExpressions;

namespace DungeonDelvers.Modules.Monsters.Domain.Monsters.DomainEvents;

public sealed class MonsterHitPointsChangedDomainEvent(Guid monsterId, DiceExpression hitPoints) : DomainEvent
{
    public Guid MonsterId { get; } = monsterId;
    public DiceExpression HitPoints { get; } = hitPoints;
}