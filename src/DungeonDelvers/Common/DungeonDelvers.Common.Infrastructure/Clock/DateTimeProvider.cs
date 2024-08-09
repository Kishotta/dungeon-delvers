using DungeonDelvers.Common.Application.Clock;

namespace DungeonDelvers.Common.Infrastructure.Clock;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}