using MediatR;

namespace DungeonDelvers.Common.Domain;

public interface IDomainEvent : INotification
{
    Guid Id { get; }
    DateTime OccurredAtUtc { get; }
}