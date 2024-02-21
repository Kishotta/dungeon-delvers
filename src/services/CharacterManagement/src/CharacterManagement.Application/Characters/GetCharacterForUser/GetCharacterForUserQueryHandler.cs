using CharacterManagement.Domain;
using CharacterManagement.Domain.Characters;

namespace CharacterManagement.Application.Characters.GetCharacterForUser;

public class GetCharacterForUserQueryHandler(ICharacterRepository characterRepository)
    : IQueryHandler<GetCharacterForUserQuery, Character>
{
    public async Task<Result<Character>> Handle (GetCharacterForUserQuery request, CancellationToken cancellationToken)
    {
        var character = await characterRepository.GetCharacterForUserAsync(request.Id, request.UserId);
        var result = character is null ?
            Result.Failure<Character>("Character not found") :
            Result.Success(character);

        return await Task.FromResult (result);
    }
}
