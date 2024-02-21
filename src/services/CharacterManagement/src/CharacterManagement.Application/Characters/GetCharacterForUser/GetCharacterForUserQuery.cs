using CharacterManagement.Domain.Characters;

namespace CharacterManagement.Application.Characters.GetCharacterForUser;

public record GetCharacterForUserQuery(Guid Id, Guid UserId) : IQuery<Character>;
