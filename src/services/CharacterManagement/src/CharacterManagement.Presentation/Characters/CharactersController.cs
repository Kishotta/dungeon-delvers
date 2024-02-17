using CharacterManagement.Application.CreateCharacter;
using CharacterManagement.Application.DeleteCharacter;
using CharacterManagement.Application.GetCharacterForUser;
using CharacterManagement.Application.GetCharactersForUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CharacterManagement.Presentation.Characters;

[Authorize]
[Route("[controller]")]
public class CharactersController(ISender sender)
    : ApiController
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CharacterResponse>>> GetCharacters (CancellationToken cancellationToken)
    {
        var query  = new GetCharactersForUserQuery (UserId);
        var result = await sender.Send (query, cancellationToken);
        return Ok (result.Value.Select (c => new CharacterResponse (c.Id, c.Name)));
    }

    [ActionName(nameof(GetCharacter))]
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CharacterResponse>> GetCharacter (Guid id, CancellationToken cancellationToken)
    {
        var query  = new GetCharacterForUserQuery (id, UserId);
        var result = await sender.Send (query, cancellationToken);
        return result.IsSuccess ? Ok (new CharacterResponse (result.Value.Id, result.Value.Name)) : NotFound ();
    }

    [HttpPost]
    public async Task<ActionResult<CharacterResponse>> CreateCharacter (CreateCharacterRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateCharacterCommand (UserId, request.Name);
        var result  = await sender.Send (command, cancellationToken);
        return CreatedAtAction (nameof(GetCharacter), new { id = result.Value.Id },new CharacterResponse(result.Value.Id, result.Value.Name));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteCharacter (Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteCharacterCommand (id, UserId);
        var result  = await sender.Send (command, cancellationToken);
        return result.IsSuccess ? NoContent () : NotFound ();
    }
}
