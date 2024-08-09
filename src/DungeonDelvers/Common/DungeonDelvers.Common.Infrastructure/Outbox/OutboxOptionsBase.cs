namespace DungeonDelvers.Common.Infrastructure.Outbox;

public abstract class OutboxOptionsBase
{
    public int IntervalInSeconds { get; init; }
    public int BatchSize { get; init; }
}