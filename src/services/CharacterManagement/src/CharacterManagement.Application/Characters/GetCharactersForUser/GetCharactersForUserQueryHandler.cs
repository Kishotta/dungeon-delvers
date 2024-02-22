using CharacterManagement.Domain;
using CharacterManagement.Domain.Characters;

namespace CharacterManagement.Application.Characters.GetCharactersForUser;

public class GetCharactersForUserQueryHandler (ICharacterRepository characterRepository)
    : IQueryHandler<GetCharactersForUserQuery, IEnumerable<Character>>
{
    public async Task<Result<IEnumerable<Character>>> Handle (GetCharactersForUserQuery request, CancellationToken cancellationToken)
    {
        var characters = await characterRepository.GetByUserIdAsync (request.UserId);

        var result = Result.Success (characters);

        return await Task.FromResult (result);
    }
}
