using CharacterManagement.Application.Contracts;
using CharacterManagement.Domain;

namespace CharacterManagement.Application.GetCharactersForUser;

public class GetCharactersForUserQueryHandler (ICharacterRepository characterRepository)
    : IQueryHandler<GetCharactersForUserQuery, IEnumerable<GetCharacterForUserResponse>>
{
    public async Task<Result<IEnumerable<GetCharacterForUserResponse>>> Handle(GetCharactersForUserQuery request, CancellationToken cancellationToken)
    {
        var characters = characterRepository.GetCharactersForUser(request.UserId);

        var result = Result.Success (characters.Select (c => new GetCharacterForUserResponse (c.Id, c.Name)));

        return await Task.FromResult(result);
    }
}
