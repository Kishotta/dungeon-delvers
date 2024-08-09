using DungeonDelvers.Common.Application.EventBus;
using MassTransit;

namespace DungeonDelvers.Common.Infrastructure.EventBus;

internal sealed class EventBus(IBus bus) : IEventBus
{
    public async Task PublishAsync<TIntegrationEvent>(
        TIntegrationEvent integrationEvent, 
        CancellationToken cancellationToken = default) 
        where TIntegrationEvent : IIntegrationEvent
    {
        await bus.Publish(integrationEvent, cancellationToken);
    }
}