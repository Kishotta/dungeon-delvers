using CharacterManagement.Application.Characters.ChangeCharacterName;
using CharacterManagement.Application.Characters.CreateCharacter;
using CharacterManagement.Application.Characters.DeleteCharacter;
using CharacterManagement.Application.Characters.GetCharacterForUser;
using CharacterManagement.Application.Characters.GetCharactersForUser;
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

    [HttpPost("{id:guid}/name")]
    public async Task<ActionResult<CharacterResponse>> ChangeCharacterName (Guid id, CharacterNameChangeRequest request, CancellationToken cancellationToken)
    {
        var command = new ChangeCharacterNameCommand (UserId, id, request.Name);
        var result  = await sender.Send (command, cancellationToken);
        return result.IsSuccess ? Ok (new CharacterResponse (result.Value.Id, result.Value.Name)) : NotFound ();
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> DeleteCharacter (Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteCharacterCommand (id, UserId);
        var result  = await sender.Send (command, cancellationToken);
        return result.IsSuccess ? NoContent () : NotFound ();
    }
}
