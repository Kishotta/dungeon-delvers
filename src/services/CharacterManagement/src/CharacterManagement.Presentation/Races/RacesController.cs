using CharacterManagement.Application.Contracts;
using CharacterManagement.Domain;
using CharacterManagement.Domain.Races;

namespace CharacterManagement.Presentation.Races;

[Authorize]
[Route ("[controller]")]
public class RacesController(ISender sender)
    : ApiController
{
    [ActionName(nameof(GetRace))]
    [HttpGet ("{id:guid}")]
    public async Task<ActionResult<RaceResponse>> GetRace (Guid id, CancellationToken cancellationToken)
    {
        var query  = new GetRacesQuery (id, UserId);
        var result = await sender.Send (query, cancellationToken);
        return result.IsSuccess ? Ok (new RaceResponse (result.Value.Id, result.Value.Name)) : NotFound ();
    }

    [HttpPost]
    public async Task<ActionResult<RaceResponse>> CreateRace (CreateRaceRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateRaceCommand (UserId, request.Name);
        var result  = await sender.Send (command, cancellationToken);
        return CreatedAtAction (nameof (GetRace), new { Id = result.Value.Id }, new RaceResponse(result.Value.Id, result.Value.Name));
    }
}

public record GetRacesQuery(Guid RaceId, Guid UserId) : IQuery<Race>;

public record CreateRaceCommand(Guid UserId, string Name) : ICommand<Race>;

public record CreateRaceRequest(string Name);

public record RaceResponse(Guid Id, string Name);

public class CreateRaceCommandHandler(IRaceRepository raceRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<CreateRaceCommand, Race>
{
    public async Task<Result<Race>> Handle (CreateRaceCommand request, CancellationToken cancellationToken)
    {
        var race = new Race (request.UserId, request.Name);

        await raceRepository.AddRaceAsync (race, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success (race);
    }
}
