using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CharacterManagement.Presentation.Characters;

[Authorize]
[ApiController]
[Route("[controller]")]
public class CharactersController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<CharacterResponse>> GetCharacters ()
    {
        return Ok ();
    }

    [HttpGet("{id:guid}")]
    public ActionResult<IEnumerable<CharacterResponse>> GetCharacter (Guid id)
    {
        return Ok ();
    }

    [HttpPost]
    public IActionResult CreateCharacter (CreateCharacterRequest request)
    {
        var sub       = User.Claims.FirstOrDefault (c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        var userId    = Guid.Parse (sub ?? string.Empty);
        var character = new CharacterResponse (Guid.NewGuid (), userId, request.Name);
        return CreatedAtAction (nameof(GetCharacter), new { character.Id }, character);
    }
}
