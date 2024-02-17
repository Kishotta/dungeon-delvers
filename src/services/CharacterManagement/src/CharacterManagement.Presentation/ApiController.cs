using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace CharacterManagement.Presentation;

[ApiController]
public class ApiController : ControllerBase
{
    protected Guid UserId
    {
        get
        {
            var sub    = User.Claims.FirstOrDefault (c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var userId = Guid.Parse (sub ?? string.Empty);

            return userId;
        }
    }
}
