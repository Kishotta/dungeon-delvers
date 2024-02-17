using CharacterManagement.Application.Contracts;
using CharacterManagement.Application.GetCharactersForUser;
using CharacterManagement.Domain;

namespace CharacterManagement.Application.GetCharacterForUser;

public class GetCharacterForUserQueryHandler(ICharacterRepository characterRepository)
    : IQueryHandler<GetCharacterForUserQuery, GetCharacterForUserResponse>
{
    public async Task<Result<GetCharacterForUserResponse>> Handle (GetCharacterForUserQuery request, CancellationToken cancellationToken)
    {
        var character = characterRepository.GetCharacterForUser(request.Id, request.UserId);
        var result = character is null ?
            Result.Failure<GetCharacterForUserResponse>("Character not found") :
            Result.Success(new GetCharacterForUserResponse(character.Id, character.Name));

        return await Task.FromResult (result);
    }
}
