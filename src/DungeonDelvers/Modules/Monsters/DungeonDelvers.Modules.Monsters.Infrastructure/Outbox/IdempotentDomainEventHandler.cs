using DungeonDelvers.Common.Application.Data;
using DungeonDelvers.Common.Application.Messaging;
using DungeonDelvers.Common.Domain;
using DungeonDelvers.Common.Infrastructure.Outbox;

namespace DungeonDelvers.Modules.Monsters.Infrastructure.Outbox;

public sealed class IdempotentDomainEventHandler<TDomainEvent>(
    IDomainEventHandler<TDomainEvent> decorated, 
    IDbConnectionFactory dbConnectionFactory) 
    : IdempotentDomainEventHandlerBase<TDomainEvent>(decorated, dbConnectionFactory)
    where TDomainEvent : IDomainEvent
{
    protected override string Schema => MonstersModule.Schema;
}
