namespace DungeonDelvers.Common.Infrastructure.Auditing;

public interface IAuditingUserProvider
{
    string GetUserId();
}