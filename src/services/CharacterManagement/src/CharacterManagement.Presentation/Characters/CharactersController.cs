using CharacterManagement.Application.Characters.ChangeCharacterName;
using CharacterManagement.Application.Characters.CreateCharacter;
using CharacterManagement.Application.Characters.DeleteCharacter;
using CharacterManagement.Application.Characters.GetCharacterForUser;
using CharacterManagement.Application.Characters.GetCharactersForUser;

namespace CharacterManagement.Presentation.Characters;

[Authorize]
[Route("[controller]")]
public class CharactersController(ISender sender)
    : ApiController
{
    /// <summary>
    /// Get Characters
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <response code="200">Characters found</response>
    /// <response code="401">Unauthenticated</response>
    [HttpGet]
    [ProducesResponseType<IEnumerable<CharacterResponse>>(200)]
    [ProducesResponseType(401)]
    public async Task<ActionResult<IEnumerable<CharacterResponse>>> GetCharacters (CancellationToken cancellationToken)
    {
        var query  = new GetCharactersForUserQuery (UserId);
        var result = await sender.Send (query, cancellationToken);
        return Ok (result.Value.Select (c => new CharacterResponse (c.Id, c.Name)));
    }

    /// <summary>
    /// Get Character
    /// </summary>
    /// <param name="id">Character ID</param>
    /// <param name="cancellationToken"></param>
    /// <response code="200">Character found</response>
    /// <response code="401">Unauthenticated</response>
    /// <response code="404">Character not found</response>
    [ActionName(nameof(GetCharacter))]
    [HttpGet("{id:guid}")]
    [ProducesResponseType<CharacterResponse>(200)]
    [ProducesResponseType(401)]
    [ProducesResponseType<ProblemDetails>(404)]
    public async Task<ActionResult<CharacterResponse>> GetCharacter (Guid id, CancellationToken cancellationToken)
    {
        var query  = new GetCharacterForUserQuery (id, UserId);
        var result = await sender.Send (query, cancellationToken);
        return result.IsSuccess ? Ok (new CharacterResponse (result.Value.Id, result.Value.Name)) : NotFound ();
    }

    /// <summary>
    /// Create Character
    /// </summary>
    /// <remarks>
    /// Creates a new character
    /// </remarks>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <response code="201">Character created</response>
    /// <response code="401">Unauthenticated</response>
    [HttpPost]
    [ProducesResponseType<CharacterResponse>(201)]
    [ProducesResponseType(401)]
    public async Task<ActionResult<CharacterResponse>> CreateCharacter (CreateCharacterRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateCharacterCommand (UserId, request.Name);
        var result  = await sender.Send (command, cancellationToken);
        return CreatedAtAction (nameof(GetCharacter), new { id = result.Value.Id },new CharacterResponse(result.Value.Id, result.Value.Name));
    }

    /// <summary>
    /// Change Character Name
    /// </summary>
    /// <remarks>
    /// Changes the name of the character
    /// </remarks>
    /// <param name="id">Character Id</param>
    /// <param name="request">The new name for the character</param>
    /// <param name="cancellationToken"></param>
    /// <response code="200">Character name changed</response>
    /// <response code="401">Unauthenticated</response>
    /// <response code="404">Character not found</response>
    [HttpPost("{id:guid}/name")]
    [ProducesResponseType<CharacterResponse>(200)]
    [ProducesResponseType(401)]
    [ProducesResponseType<ProblemDetails>(404)]
    public async Task<ActionResult<CharacterResponse>> ChangeCharacterName (Guid id, CharacterNameChangeRequest request, CancellationToken cancellationToken)
    {
        var command = new ChangeCharacterNameCommand (UserId, id, request.Name);
        var result  = await sender.Send (command, cancellationToken);
        return result.IsSuccess ? Ok (new CharacterResponse (result.Value.Id, result.Value.Name)) : NotFound ();
    }

    /// <summary>
    /// Delete Character
    /// </summary>
    /// <param name="id">Character Id</param>
    /// <param name="cancellationToken"></param>
    /// <response code="204">Character deleted</response>
    /// <response code="401">Unauthenticated</response>
    /// <response code="404">Character not found</response>
    [HttpDelete("{id:Guid}")]
    [ProducesResponseType<CharacterResponse>(204)]
    [ProducesResponseType(401)]
    [ProducesResponseType<ProblemDetails>(404)]
    public async Task<IActionResult> DeleteCharacter (Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteCharacterCommand (id, UserId);
        var result  = await sender.Send (command, cancellationToken);
        return result.IsSuccess ? NoContent () : NotFound ();
    }
}
