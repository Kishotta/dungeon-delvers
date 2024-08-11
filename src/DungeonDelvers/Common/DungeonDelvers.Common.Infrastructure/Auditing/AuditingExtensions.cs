using DungeonDelvers.Common.Domain;
using DungeonDelvers.Common.Domain.Auditing;
using DungeonDelvers.Common.Infrastructure.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DungeonDelvers.Common.Infrastructure.Auditing;

internal static class AuditingExtensions
{
    public static IServiceCollection AddAuditing(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        
        services.TryAddSingleton<IAuditingUserProvider, JwtAuditingUserProvider>();
        
        services.TryAddSingleton<WriteAuditLogInterceptor>();
        
        return services;
    }
    
    internal static bool ShouldBeAudited(this EntityEntry entry)
    {
        if (entry.State is EntityState.Added or EntityState.Modified or EntityState.Deleted)
            return entry.Entity is not Audit && entry.IsAuditable();

        return entry
            .References
            .Select(owned => owned.TargetEntry)
            .Any(entityEntry => entityEntry is { State: EntityState.Added or EntityState.Modified or EntityState.Deleted });
    }

    private static bool IsAuditable(this EntityEntry entityEntry)
    {
        var entityAuditableAttribute = Attribute.GetCustomAttribute(
            entityEntry.Entity.GetType(), 
            typeof(AuditableAttribute)) as AuditableAttribute;

        return entityAuditableAttribute is not null;
    }

    internal static bool IsAuditable(this PropertyEntry propertyEntry)
    {
        var entityType = propertyEntry.EntityEntry.Entity.GetType();
        var propertyInfo = entityType.GetProperty(propertyEntry.Metadata.Name);
        if (propertyInfo is null) return false;
        
        var propertyAuditingDisabled = Attribute.IsDefined(propertyInfo, typeof(NotAuditableAttribute));
        return !propertyAuditingDisabled;
    }
}