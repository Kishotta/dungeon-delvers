using CharacterManagement.Application.Sources.CreateSource;
using CharacterManagement.Application.Sources.GetSource;
using CharacterManagement.Presentation.Characters;

namespace CharacterManagement.Presentation.Sources;

[Authorize]
[Route("[controller]")]
public class SourcesController(ISender sender)
    : ApiController
{
    [ActionName(nameof(GetSource))]
    [HttpGet ("{id:guid}")]
    public async Task<ActionResult<CharacterResponse>> GetSource (Guid id, CancellationToken cancellationToken)
    {
        var query = new GetSourceQuery (id, UserId);
        var result = await sender.Send (query, cancellationToken);
        return result.IsSuccess ? Ok (new SourceResponse (result.Value.Id, result.Value.Name)) : NotFound ();
    }

    [HttpPost]
    public async Task<ActionResult<SourceResponse>> CreateSource (CreateSourceRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateSourceCommand (UserId, request.Name);
        var result  = await sender.Send (command, cancellationToken);
        return CreatedAtAction (nameof (GetSource), new { Id = result.Value.Id }, new SourceResponse(result.Value.Id, result.Value.Name));
    }
}
