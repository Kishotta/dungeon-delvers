using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CharacterManagement.Presentation;

[Authorize]
[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    [HttpGet("UserSubClaim")]
    public IActionResult Test ()
    {
        var userClaims = User.Claims;
        var userInfo = userClaims.Select (c => new { c.Type, c.Value });
        var id = userClaims.FirstOrDefault (c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        return Ok (id);
    }

    [HttpGet("WellKnownConfiguration")]
    [AllowAnonymous]
    public IActionResult WellKnownConfiguration ()
    {
        var client = new HttpClient ();
        var response = client.GetAsync ("http://keycloak:8080/realms/ddelvers/.well-known/openid-configuration").Result;
        if (response.IsSuccessStatusCode)
            return Ok (response.Content.ReadAsStringAsync ().Result);
        return BadRequest ();
    }
}
