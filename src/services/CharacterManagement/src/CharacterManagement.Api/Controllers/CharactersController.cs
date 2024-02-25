using CharacterManagement.Api.Models;
using CharacterManagement.Api.Models.Characters;
using CharacterManagement.Api.Persistence;
using CharacterManagement.Api.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CharacterManagement.Api.Controllers;

[Authorize]
[Route ("[controller]")]
public class CharactersController(
        CharacterManagementContext dbContext)
    : ApiController
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Character>>> GetCharacters (CancellationToken cancellationToken)
    {
        var characters = await dbContext.Characters
                                        .AsNoTracking ()
                                        .Include (c => c.Effects)
                                        .Where (c => c.OwnerId == UserId)
                                        .ToListAsync (cancellationToken);

        return Ok (characters.Select (c => c.Materialize()));
    }

    [ActionName (nameof (GetCharacter))]
    [HttpGet ("{id:guid}")]
    public async Task<ActionResult<Character>> GetCharacter (Guid id, CancellationToken cancellationToken)
    {
        var character = await dbContext.Characters
                                       .AsNoTracking ()
                                       .Include (c => c.Effects)
                                       .Where (c => c.OwnerId == UserId)
                                       .SingleOrDefaultAsync (c => c.Id == id, cancellationToken);
        if (character is null)
            return NotFound ();
        return Ok (character.Materialize());
    }

    public record CreateCharacterRequest(string Name, Guid RaceId);

    [HttpPost]
    public async Task<ActionResult<MaterializedCharacter>> CreateCharacter (CreateCharacterRequest request, CancellationToken cancellationToken)
    {
        var race = await dbContext.Races
                                 .AsNoTracking ()
                                  .Include (r => r.Traits)
                                  .ThenInclude(t => t.Effects)
                                  .Where (r => r.OwnerId == UserId)
                                 .SingleOrDefaultAsync (r => r.Id == request.RaceId, cancellationToken);
        if (race is null)
            return BadRequest ();

        var character = new Character (UserId, request.Name, race);
        await dbContext.Characters.AddAsync (character, cancellationToken);
        await dbContext.SaveChangesAsync (cancellationToken);

        var materializedCharacter = character.Materialize ();

        return CreatedAtAction (nameof (GetCharacter), new { id = character.Id }, materializedCharacter);
    }

    public record ChangeNameRequest(string Name);

    [HttpPost ("{id:guid}/name")]
    public async Task<ActionResult<Character>> ChangeCharacterName (Guid id,
                                                                    ChangeNameRequest request,
                                                                    CancellationToken cancellationToken)
    {
        var character = await dbContext.Characters
                                       .Where (c => c.OwnerId == UserId)
                                       .SingleOrDefaultAsync (c => c.Id == id, cancellationToken);
        if (character is null)
            return NotFound ();
        character.ChangeName (request.Name);
        await dbContext.SaveChangesAsync (cancellationToken);
        return Ok (character);
    }

    [HttpDelete ("{id:Guid}")]
    public async Task<IActionResult> DeleteCharacter (Guid id, CancellationToken cancellationToken)
    {
        var character = await dbContext.Characters
                                       .Where (c => c.OwnerId == UserId)
                                       .SingleOrDefaultAsync (c => c.Id == id, cancellationToken);
        if (character is null)
            return NotFound ();
        dbContext.Characters.Remove (character);
        return NoContent ();
    }
}
