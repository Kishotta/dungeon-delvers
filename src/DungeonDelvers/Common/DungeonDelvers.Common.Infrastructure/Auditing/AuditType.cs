namespace DungeonDelvers.Common.Infrastructure.Auditing;

[Flags]
public enum AuditType
{
    None = 0,
    Create = 1,
    Update = 2,
    Delete = 4
}