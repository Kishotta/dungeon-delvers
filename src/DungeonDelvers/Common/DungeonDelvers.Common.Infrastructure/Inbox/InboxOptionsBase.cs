namespace DungeonDelvers.Common.Infrastructure.Inbox;

public abstract class InboxOptionsBase
{
    public int IntervalInSeconds { get; init; }
    public int BatchSize { get; init; }
}