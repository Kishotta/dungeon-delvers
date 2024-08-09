using DungeonDelvers.Common.Application.Exceptions;
using DungeonDelvers.Common.Infrastructure.Auditing;
using Microsoft.AspNetCore.Http;

namespace DungeonDelvers.Common.Infrastructure.Authentication;

public class JwtAuditingUserProvider(IHttpContextAccessor httpContextAccessor) : IAuditingUserProvider
{
    private const string DefaultUser = "Unknown User";
    
    public string GetUserId()
    {
        try
        {
            return httpContextAccessor.HttpContext?.User.GetUserId().ToString() ?? DefaultUser;
        }
        catch (DungeonDelversException)
        {
            return DefaultUser;
        }
    }
}