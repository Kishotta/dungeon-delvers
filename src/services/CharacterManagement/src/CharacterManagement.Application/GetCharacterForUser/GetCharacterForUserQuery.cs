using CharacterManagement.Application.Contracts;
using CharacterManagement.Application.GetCharactersForUser;

namespace CharacterManagement.Application.GetCharacterForUser;

public record GetCharacterForUserQuery(Guid Id, Guid UserId) : IQuery<GetCharacterForUserResponse>;
