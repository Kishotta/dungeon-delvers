namespace DungeonDelvers.Common.Domain;

public abstract class Entity<TId>
{
    public required TId Id { get; init; }
}

public abstract class Entity : Entity<Guid>
{
    public IReadOnlyCollection<IDomainEvent> GetDomainEvents() => _domainEvents.ToList();
    private readonly List<IDomainEvent> _domainEvents = [];

    public void ClearDomainEvents() => _domainEvents.Clear();

    protected void RaiseDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);
}

