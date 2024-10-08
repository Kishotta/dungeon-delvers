using DungeonDelvers.Common.Application.Exceptions;
using DungeonDelvers.Common.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Pipelines.Sockets.Unofficial.Buffers;

namespace DungeonDelvers.Common.Infrastructure.Auditing;

public sealed class WriteAuditLogInterceptor(IAuditingUserProvider auditingUserProvider)
    : SaveChangesInterceptor
{
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData, 
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is not null)
            await WriteAuditLog(eventData.Context, cancellationToken);
        
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }
    
    private async Task WriteAuditLog(
        DbContext context, 
        CancellationToken cancellationToken)
    {
        var audits = CreateAudits(context);
        
        await context.AddRangeAsync(audits, cancellationToken);
    }
    
    private IEnumerable<Audit> CreateAudits(DbContext context)
    {
        var userId = auditingUserProvider.GetUserId();
        var auditEntries = new List<AuditEntry>();
        
        foreach (var entry in context.ChangeTracker.Entries<Entity>())
        {
            if (!entry.ShouldBeAudited()) continue;
            
            var tableName = context
                .Model
                .FindEntityType(entry.Entity.GetType())
                ?.GetTableName() ?? "Unknown Table";
            
            var auditEntry = new AuditEntry(
                entry,
                tableName,
                userId);
            
            auditEntries.Add(auditEntry);

            TrackEntityProperties(entry, auditEntry);

            foreach (var ownedEntity in entry.References)
            {
                TrackOwnedEntityProperties(entry, ownedEntity.TargetEntry!, auditEntry);
            }
        }

        return auditEntries.Select(auditEntry => auditEntry.ToAudit());
    }

    private static void TrackEntityProperties(EntityEntry entry, AuditEntry auditEntry)
    {
        foreach (var property in entry.Properties)
        {
            if (!property.IsAuditable()) continue;

            var propertyName = property.Metadata.Name;
            if (property.Metadata.IsPrimaryKey())
            {
                auditEntry.KeyValues[propertyName] = property.CurrentValue;
                continue;
            }

            switch (entry.State)
            {
                case EntityState.Added:
                    auditEntry.AuditType |= AuditType.Create;
                    auditEntry.NewValues[propertyName] = property.CurrentValue;
                    break;
                case EntityState.Deleted:
                    auditEntry.AuditType |= AuditType.Delete;
                    auditEntry.OldValues[propertyName] = property.OriginalValue;
                    break;
                case EntityState.Modified:
                    if (property.IsModified)
                    {
                        auditEntry.AuditType |= AuditType.Update;
                        auditEntry.ChangedColumns.Add(propertyName);
                        auditEntry.OldValues[propertyName] = property.OriginalValue;
                        auditEntry.NewValues[propertyName] = property.CurrentValue;
                    }
                    break;
                case EntityState.Detached:
                    break;
                case EntityState.Unchanged:
                    auditEntry.AuditType |= AuditType.None;
                    break;
                default:
                    throw new DungeonDelversException(
                        nameof(WriteAuditLog),
                        Error.Failure(
                            "AuditLog.Failure",
                            "Unable to determine entity state for audit log."));
            }
        }
    }
    
    private static void TrackOwnedEntityProperties(EntityEntry parentEntry, EntityEntry entry, AuditEntry auditEntry)
    {
        foreach (var property in entry.Properties)
        {
            if (!property.IsAuditable()) continue;

            var propertyName = $"{property.Metadata.DeclaringType.ClrType.Name}.{property.Metadata.Name}";
            if (property.Metadata.IsPrimaryKey())
            {
                auditEntry.KeyValues[propertyName] = property.CurrentValue;
                continue;
            }

            switch (entry.State)
            {
                case EntityState.Added:
                    auditEntry.AuditType |= parentEntry.State == EntityState.Added
                        ? AuditType.Create
                        : AuditType.Update;
                    auditEntry.NewValues[propertyName] = property.CurrentValue;
                    break;
                case EntityState.Deleted:
                    auditEntry.AuditType |= AuditType.Delete;
                    auditEntry.OldValues[propertyName] = property.OriginalValue;
                    break;
                case EntityState.Modified:
                    if (property.IsModified)
                    {
                        auditEntry.AuditType |= AuditType.Update;
                        auditEntry.ChangedColumns.Add(propertyName);
                        auditEntry.OldValues[propertyName] = property.OriginalValue;
                        auditEntry.NewValues[propertyName] = property.CurrentValue;
                    }
                    break;
                case EntityState.Detached:
                    break;
                case EntityState.Unchanged:
                    auditEntry.AuditType |= AuditType.None;
                    break;
                default:
                    throw new DungeonDelversException(
                        nameof(WriteAuditLog),
                        Error.Failure(
                            "AuditLog.Failure",
                            "Unable to determine entity state for audit log."));
            }
        }
    }
}