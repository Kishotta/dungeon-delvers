using System.Security.Claims;
using DungeonDelvers.Common.Application.Exceptions;

namespace DungeonDelvers.Common.Infrastructure.Authentication;

public static class ClaimsPrincipalExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal? principal)
    {
        var userId = principal?.FindFirstValue(CustomClaims.Sub);

        return Guid.TryParse(userId, out var parsedUserId)
            ? parsedUserId
            : throw new DungeonDelversException("User identifier is unavailable");
    }
    
    public static string GetIdentityId(this ClaimsPrincipal? principal) =>
        principal?.FindFirstValue(ClaimTypes.NameIdentifier) ??
        throw new DungeonDelversException("User identity is unavailable");

    public static HashSet<string> GetPermissions(this ClaimsPrincipal? principal)
    {
        var permissionClaims = principal?.FindAll(CustomClaims.Permission) ??
                               throw new DungeonDelversException("Permissions are unavailable");
        return permissionClaims.Select(claim => claim.Value).ToHashSet();
    }
}