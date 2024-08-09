using DungeonDelvers.Common.Application.Data;
using DungeonDelvers.Common.Application.EventBus;
using DungeonDelvers.Common.Infrastructure.Inbox;

namespace DungeonDelvers.Modules.Monsters.Infrastructure.Inbox;

public class IdempotentIntegrationEventHandler<TIntegrationEvent>(
    IIntegrationEventHandler<TIntegrationEvent> decorated,
    IDbConnectionFactory dbConnectionFactory) 
    : IdempotentIntegrationEventHandlerBase<TIntegrationEvent>(decorated, dbConnectionFactory) 
    where TIntegrationEvent : IntegrationEvent
{
    protected override string Schema => MonstersModule.Schema;
}