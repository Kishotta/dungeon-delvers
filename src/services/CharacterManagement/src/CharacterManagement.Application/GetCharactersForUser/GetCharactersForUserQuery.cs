using CharacterManagement.Application.Contracts;
using MediatR;

namespace CharacterManagement.Application.GetCharactersForUser;

public record GetCharactersForUserQuery(Guid UserId) : IQuery<IEnumerable<GetCharacterForUserResponse>>;
