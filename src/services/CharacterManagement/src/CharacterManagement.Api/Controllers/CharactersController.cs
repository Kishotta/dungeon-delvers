using CharacterManagement.Api.Models.Characters;
using CharacterManagement.Api.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace CharacterManagement.Api.Controllers;

[Authorize]
[Route ("[controller]")]
public class CharactersController(
        CharacterManagementContext dbContext,
        IMemoryCache memoryCache)
    : ApiController
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MaterializedCharacter>>> GetCharacters (CancellationToken cancellationToken)
    {
        var cacheKey = $"Characters_{UserId}";
        if (!memoryCache.TryGetValue (cacheKey, out IEnumerable<MaterializedCharacter>? materializedCharacters))
        {
            var characters = await dbContext.Characters
                                            .AsNoTracking ()
                                            .Include (c => c.Effects)
                                            .Where (c => c.OwnerId == UserId)
                                            .ToListAsync (cancellationToken);
            materializedCharacters = characters.Select (c => c.Materialize ());
            memoryCache.Set (cacheKey, materializedCharacters, TimeSpan.FromMinutes (5));
        }

        return Ok (materializedCharacters);
    }

    [ActionName (nameof (GetCharacter))]
    [HttpGet ("{id:guid}")]
    public async Task<ActionResult<MaterializedCharacter>> GetCharacter (Guid id, CancellationToken cancellationToken)
    {
        var cacheKey = $"Character_{id}";
        if (!memoryCache.TryGetValue (cacheKey, out MaterializedCharacter? materializedCharacter))
        {
            var character = await dbContext.Characters
                                           .AsNoTracking ()
                                           .Include (c => c.Effects)
                                           .Where (c => c.OwnerId           == UserId)
                                           .SingleOrDefaultAsync (c => c.Id == id, cancellationToken);
            if (character is null)
                return NotFound ();

            materializedCharacter = character.Materialize ();
            memoryCache.Set (cacheKey, materializedCharacter, TimeSpan.FromMinutes (5));
        }

        return Ok (materializedCharacter);
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

    public record ChangeCharacterNameRequest(string Name);

    [HttpPost ("{id:guid}/name")]
    public async Task<ActionResult<Character>> ChangeCharacterName (Guid id,
                                                                    ChangeCharacterNameRequest request,
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
