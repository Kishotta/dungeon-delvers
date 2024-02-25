using CharacterManagement.Api.Models;
using CharacterManagement.Api.Models.Races;
using CharacterManagement.Api.Persistence;
using CharacterManagement.Api.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CharacterManagement.Api.Controllers;

[Authorize]
[Route ("[controller]")]
public class RacesController(CharacterManagementContext dbContext,
                             RaceRepository raceRepository,
                             SourceRepository sourceRepository,
                             IUnitOfWork unitOfWork)
    : ApiController
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Race>>> GetRaces (CancellationToken cancellationToken)
    {
        var races = await raceRepository.GetByOwnerIdAsync (UserId, cancellationToken);
        return Ok (races);
    }

    [ActionName(nameof(GetRace))]
    [HttpGet ("{id:guid}")]
    public async Task<ActionResult<Race>> GetRace (Guid id, CancellationToken cancellationToken)
    {
        var race = await raceRepository.GetByIdAsync(id, cancellationToken);
        if (race is null || !race.OwnedBy (UserId))
            return NotFound ();
        return Ok (race);
    }

    public record CreateRaceRequest(string Name);

    [HttpPost]
    public async Task<ActionResult<Race>> CreateRace (CreateRaceRequest request, CancellationToken cancellationToken)
    {
        var race = new Race (UserId, request.Name);
        await raceRepository.AddSourceAsync (race, cancellationToken);
        await unitOfWork.SaveChangesAsync (cancellationToken);
        return CreatedAtAction (nameof (GetRace), new { id = race.Id }, race);
    }

    public record ChangeNameRequest(string Name);

    [HttpPost ("{id:guid}/name")]
    public async Task<ActionResult<Race>> ChangeRaceName (Guid id, ChangeNameRequest request, CancellationToken cancellationToken)
    {
        var race = await raceRepository.GetByIdAsync (id, cancellationToken);
        if (race is null || !race.OwnedBy (UserId))
            return NotFound ();
        race.ChangeName (request.Name);
        await unitOfWork.SaveChangesAsync (cancellationToken);
        return Ok (race);
    }

    public record AddTraitRequest(string Name, string Description);

    [ActionName(nameof(GetTrait))]
    [HttpGet ("{id:guid}/traits/{traitId:guid}")]
    public async Task<ActionResult> GetTrait (Guid id, Guid traitId, CancellationToken cancellationToken)
    {
        var race = await dbContext.Races
                                  .Include (r => r.Traits)
                                  .ThenInclude (t => t.Effects)
                                  .Where (r => r.OwnerId == UserId)
                                  .FirstOrDefaultAsync (r => r.Id == id, cancellationToken: cancellationToken);
        if (race is null) return NotFound ();
        var trait = race.Traits.SingleOrDefault(t => t.Id == traitId);
        if (trait is null) return NotFound ();
        return Ok (trait);
    }

    [HttpPost ("{id:guid}/traits")]
    public async Task<ActionResult> AddTrait (Guid id, AddTraitRequest request, CancellationToken cancellationToken)
    {
        var race = dbContext.Races
                            .Include (r => r.Traits)
                            .ThenInclude (t => t.Effects)
                            .Where (r => r.OwnerId == UserId)
                            .FirstOrDefault (r => r.Id == id);
        if (race is null)
            return NotFound ();

        var trait = new Trait (request.Name, request.Description);
        race.Traits.Add (trait);
        await dbContext.SaveChangesAsync (cancellationToken);

        return CreatedAtAction (nameof(GetTrait), new { id = race.Id, traitId = trait.Id }, trait);
    }

    public record AddEffectRequest([property: ModelBinder(BinderType = typeof(DynamicEffectModelBinder))]Effect? Effect);

    [HttpPost ("{id:guid}/traits/{traitId:guid}/effects")]
    public async Task<ActionResult> AddEffect (Guid id, Guid traitId, [FromBody][ModelBinder(BinderType = typeof(DynamicEffectModelBinder))]Effect? effect, CancellationToken cancellationToken)
    {
        if (effect is null) return BadRequest ();

        var race = await dbContext.Races
                                  .Include (r => r.Traits)
                                  .ThenInclude(t => t.Effects)
                                  .Where (r => r.OwnerId == UserId)
                                  .FirstOrDefaultAsync (r => r.Id == id, cancellationToken);
        if (race is null) return NotFound ();
        var trait = race.Traits.FirstOrDefault (t => t.Id == traitId);
        if (trait is null) return NotFound ();
        trait.Effects.Add (effect);
        await dbContext.SaveChangesAsync (cancellationToken);
        return CreatedAtAction (nameof (GetTrait), new { id = race.Id, traitId = trait.Id }, trait);
    }

    public record AddToSourceRequest(Guid SourceId);

    [HttpPost ("{id:guid}/sources")]
    public async Task<ActionResult> AddToSource (Guid id, AddToSourceRequest request, CancellationToken cancellationToken)
    {
        var race = await raceRepository.GetByIdAsync (id, cancellationToken);
        if (race is null || !race.OwnedBy (UserId))
            return NotFound ();

        var source = await sourceRepository.GetByIdAsync (request.SourceId, cancellationToken);
        if (source is null || !source.OwnedBy (UserId))
            return NotFound ();

        race.AddToSource(source);
        await unitOfWork.SaveChangesAsync (cancellationToken);

        return NoContent ();
    }

    public record RemoveFromSourceRequest(Guid SourceId);

    [HttpDelete ("{id:guid}/sources/{sourceId:guid}")]
    public async Task<ActionResult> RemoveFromSource (Guid id, RemoveFromSourceRequest request, CancellationToken cancellationToken)
    {
        var race = await raceRepository.GetByIdAsync (id, cancellationToken);
        if(race is null || !race.OwnedBy (UserId))
            return NotFound ();

        var source = await sourceRepository.GetByIdAsync (request.SourceId, cancellationToken);
        if (source is null || !source.OwnedBy (UserId))
            return NotFound ();

        race.RemoveFromSource (source);
        await unitOfWork.SaveChangesAsync (cancellationToken);

        return NoContent ();
    }


}
