using System.Data;
using System.Reflection;
using Dapper;
using DungeonDelvers.Common.Application.Clock;
using DungeonDelvers.Common.Application.Data;
using DungeonDelvers.Common.Domain;
using DungeonDelvers.Common.Infrastructure.Serialization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Quartz;

namespace DungeonDelvers.Common.Infrastructure.Outbox;

[DisallowConcurrentExecution]
public abstract class ProcessOutboxJobBase<TOutboxOptions>(
    IDbConnectionFactory dbConnectionFactory,
    IServiceScopeFactory serviceScopeFactory,
    IDateTimeProvider dateTimeProvider,
    IOptions<OutboxOptionsBase> outboxOptions,
    ILogger<ProcessOutboxJobBase<TOutboxOptions>> logger) : IJob
    where TOutboxOptions : OutboxOptionsBase
{
    protected abstract string ModuleName { get; }
    protected abstract string Schema { get; }
    protected abstract Assembly ApplicationAssembly { get; }
    
    public async Task Execute(IJobExecutionContext context)
    {
        logger.LogInformation("{Module} - Beginning to process outbox messages", ModuleName);

        await using var connection = await dbConnectionFactory.OpenConnectionAsync();
        await using var transaction = await connection.BeginTransactionAsync();

        var outboxMessages = await GetUnprocessedOutboxMessagesAsync(connection, transaction);
        
        foreach (var outboxMessage in outboxMessages)
        {
            Exception? exception = null;
            try
            {
                var domainEvent = JsonConvert.DeserializeObject<IDomainEvent>(outboxMessage.Content, SerializerSettings.Instance)!;

                await PublishDomainEvent(domainEvent);
            }
            catch (Exception caughtException)
            {
                logger.LogError(caughtException, "{Module} - Exception while processing outbox message {MessageId}", ModuleName, outboxMessage.Id);
                
                exception = caughtException;
            }

            await UpdateOutboxMessageAsync(connection, transaction, outboxMessage, exception);
        }
        
        await transaction.CommitAsync();
        
        logger.LogInformation("{Module} - Completed processing outbox messages", ModuleName);
    }

    private async Task PublishDomainEvent(IDomainEvent domainEvent)
    {
        using var scope = serviceScopeFactory.CreateScope();

        var domainEventHandlers = DomainEventHandlersFactory.GetHandlers(
            domainEvent.GetType(),
            scope.ServiceProvider,
            ApplicationAssembly);

        foreach (var domainEventHandler in domainEventHandlers)
        {
            await domainEventHandler.Handle(domainEvent);
        }
    }

    private async Task<IReadOnlyList<OutboxMessageResponse>> GetUnprocessedOutboxMessagesAsync(
        IDbConnection connection,
        IDbTransaction transaction)
    {
        var sql =
            $"""
             SELECT
                id AS {nameof(OutboxMessageResponse.Id)},
                content AS {nameof(OutboxMessageResponse.Content)}
             FROM {Schema}.outbox_messages
             WHERE processed_at_utc IS NULL
             ORDER BY occurred_at_utc
             LIMIT {outboxOptions.Value.BatchSize}
             FOR UPDATE
             """;

        var outboxMessages = await connection.QueryAsync<OutboxMessageResponse>(sql, transaction: transaction);
        return outboxMessages.ToList();
    }
    
    private async Task UpdateOutboxMessageAsync(
        IDbConnection connection,
        IDbTransaction transaction,
        OutboxMessageResponse outboxMessage,
        Exception? exception)
    {
        var sql =
            $"""
            UPDATE {Schema}.outbox_messages
            SET processed_at_utc = @ProcessedAtUtc,
               error = @Error
            WHERE id = @Id
            """;

        await connection.ExecuteAsync(
            sql, 
            new
            {
                outboxMessage.Id,
                ProcessedAtUtc = dateTimeProvider.UtcNow,
                Error = exception?.Message
            }, transaction: transaction);
    }

    internal sealed record OutboxMessageResponse(Guid Id, string Content);
}