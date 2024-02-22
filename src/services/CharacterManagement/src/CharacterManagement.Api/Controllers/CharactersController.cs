using CharacterManagement.Api.Models;
using CharacterManagement.Api.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CharacterManagement.Api.Controllers;

[Authorize]
[Route ("[controller]")]
public class CharactersController(
        CharacterRepository characterRepository,
        IUnitOfWork unitOfWork)
    : ApiController
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Character>>> GetCharacters (CancellationToken cancellationToken)
    {
        var characters = await characterRepository.GetByOwnerIdAsync (UserId, cancellationToken);
        return Ok (characters);
    }

    [ActionName (nameof (GetCharacter))]
    [HttpGet ("{id:guid}")]
    public async Task<ActionResult<Character>> GetCharacter (Guid id, CancellationToken cancellationToken)
    {
        var character = await characterRepository.GetByIdAsync (id, cancellationToken);
        if (character is null || !character.OwnedBy (UserId))
            return NotFound ();
        return Ok (character);
    }

    public record CreateCharacterRequest(string Name);

    [HttpPost]
    public async Task<ActionResult<Character>> CreateCharacter (CreateCharacterRequest request, CancellationToken cancellationToken)
    {
        var character = new Character (UserId, request.Name);
        await characterRepository.AddCharacterAsync (character, cancellationToken);
        await unitOfWork.SaveChangesAsync (cancellationToken);
        return CreatedAtAction (nameof (GetCharacter), new { id = character.Id }, character);
    }

    public record ChangeNameRequest(string Name);

    [HttpPost ("{id:guid}/name")]
    public async Task<ActionResult<Character>> ChangeCharacterName (Guid id,
                                                                    ChangeNameRequest request,
                                                                    CancellationToken cancellationToken)
    {
        var character = await characterRepository.GetByIdAsync (id, cancellationToken);
        if (character is null || !character.OwnedBy (UserId))
            return NotFound ();
        character.ChangeName (request.Name);
        await unitOfWork.SaveChangesAsync (cancellationToken);
        return Ok (character);
    }

    [HttpDelete ("{id:Guid}")]
    public async Task<IActionResult> DeleteCharacter (Guid id, CancellationToken cancellationToken)
    {
        var character = await characterRepository.GetByIdAsync (id, cancellationToken);
        if (character is null || !character.OwnedBy (UserId))
            return NotFound ();
        characterRepository.DeleteCharacter (character);
        return NoContent ();
    }
}
