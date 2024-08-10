using DungeonDelvers.Common.Domain;

namespace DungeonDelvers.Modules.Monsters.Domain.Monsters;

public sealed class MonsterCreatedDomainEvent(Guid monsterId) : DomainEvent
{
    public Guid MonsterId { get; } = monsterId;
}