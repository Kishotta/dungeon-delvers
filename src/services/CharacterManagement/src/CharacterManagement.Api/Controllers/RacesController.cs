using CharacterManagement.Api.Models;
using CharacterManagement.Api.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CharacterManagement.Api.Controllers;

[Authorize]
[Route ("[controller]")]
public class RacesController(RaceRepository raceRepository,
                             IUnitOfWork unitOfWork)
    : ApiController
{
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
}
