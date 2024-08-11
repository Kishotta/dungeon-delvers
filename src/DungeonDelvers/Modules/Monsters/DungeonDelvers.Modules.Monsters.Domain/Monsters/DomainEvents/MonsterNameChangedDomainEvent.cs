using DungeonDelvers.Common.Domain;

namespace DungeonDelvers.Modules.Monsters.Domain.Monsters.DomainEvents;

public sealed class MonsterNameChangedDomainEvent(Guid monsterId, string name) : DomainEvent
{
    public Guid MonsterId { get; } = monsterId;
    public string Name { get; } = name;
}