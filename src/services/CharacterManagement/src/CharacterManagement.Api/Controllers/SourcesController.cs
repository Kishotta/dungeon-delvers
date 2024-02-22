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
}
