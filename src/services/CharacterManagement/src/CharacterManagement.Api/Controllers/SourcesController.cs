using CharacterManagement.Api.Models;
using CharacterManagement.Api.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CharacterManagement.Api.Controllers;

[Authorize]
[Route("[controller]")]
public class SourcesController(SourceRepository sourceRepository,
                               IUnitOfWork unitOfWork)
    : ApiController
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Source>>> GetSources(CancellationToken cancellationToken)
    {
        var sources = await sourceRepository.GetByOwnerIdAsync(UserId, cancellationToken);
        return Ok(sources);
    }

    [ActionName(nameof(GetSource))]
    [HttpGet ("{id:guid}")]
    public async Task<ActionResult<Source>> GetSource (Guid id, CancellationToken cancellationToken)
    {
        var source = await sourceRepository.GetByIdAsync (id, cancellationToken);
        if (source is null || !source.OwnedBy (UserId))
            return NotFound ();
        return Ok(source);
    }

    public record CreateSourceRequest(string Name);

    [HttpPost]
    public async Task<ActionResult<Source>> CreateSource (CreateSourceRequest request, CancellationToken cancellationToken)
    {
        var source = new Source (UserId, request.Name);
        await sourceRepository.AddSourceAsync (source, cancellationToken);
        await unitOfWork.SaveChangesAsync (cancellationToken);
        return CreatedAtAction (nameof (GetSource), new { id = source.Id }, source);
    }

    public record ChangeNameRequest(string Name);

    [HttpPost ("{id:guid}/name")]
    public async Task<ActionResult<Source>> ChangeSourceName (Guid id,
                                                            ChangeNameRequest request,
                                                            CancellationToken cancellationToken)
    {
        var source = await sourceRepository.GetByIdAsync (id, cancellationToken);
        if (source is null || !source.OwnedBy (UserId))
            return NotFound ();
        source.ChangeName (request.Name);
        await unitOfWork.SaveChangesAsync (cancellationToken);
        return Ok (source);
    }

    [HttpDelete ("{id:Guid}")]
    public async Task<IActionResult> DeleteSource (Guid id, CancellationToken cancellationToken)
    {
        var source = await sourceRepository.GetByIdAsync (id, cancellationToken);
        if (source is null || !source.OwnedBy (UserId))
            return NotFound ();
        sourceRepository.DeleteSource (source);
        await unitOfWork.SaveChangesAsync (cancellationToken);
        return NoContent ();
    }
}
