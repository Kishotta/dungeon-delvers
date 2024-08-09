using DungeonDelvers.Common.Domain;

namespace DungeonDelvers.Common.Application.Authorization;

public interface IPermissionService
{
    Task<Result<PermissionResponse>> GetUserPermissionsAsync(string identityId);
}