using Dapper;
using DungeonDelvers.Common.Application.Data;
using DungeonDelvers.Common.Application.EventBus;
using DungeonDelvers.Common.Infrastructure.Serialization;
using MassTransit;
using Newtonsoft.Json;

namespace DungeonDelvers.Common.Infrastructure.Inbox;

public abstract class IntegrationEventConsumer<TIntegrationEvent>(
    IDbConnectionFactory dbConnectionFactory)
    : IConsumer<TIntegrationEvent> 
    where TIntegrationEvent : IntegrationEvent
{
    protected abstract string Schema { get; }
    
    public async Task Consume(ConsumeContext<TIntegrationEvent> context)
    {
        await using var connection = await dbConnectionFactory.OpenConnectionAsync();

        var integrationEvent = context.Message;

        var inboxMessage = new InboxMessage
        {
            Id = integrationEvent.Id,
            Type = integrationEvent.GetType().Name,
            Content = JsonConvert.SerializeObject(integrationEvent, SerializerSettings.Instance),
            OccurredAtUtc = integrationEvent.OccurredAtUtc
        };

        var sql =
            $"""
            INSERT INTO {Schema}.inbox_messages(id, type, content, occurred_at_utc)
            VALUES (@Id, @Type, @Content::json, @OccurredAtUtc);
            """;
        
        await connection.ExecuteAsync(sql, inboxMessage);
    }
}