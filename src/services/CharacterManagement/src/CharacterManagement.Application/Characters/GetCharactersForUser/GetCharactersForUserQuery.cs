using CharacterManagement.Domain.Characters;

namespace CharacterManagement.Application.Characters.GetCharactersForUser;

public record GetCharactersForUserQuery(Guid UserId) : IQuery<IEnumerable<Character>>;
