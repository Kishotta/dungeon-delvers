using CharacterManagement.Application.Contracts;
using CharacterManagement.Domain;
using CharacterManagement.Domain.Characters;

namespace CharacterManagement.Application.GetCharacterForUser;

public record GetCharacterForUserQuery(Guid Id, Guid UserId) : IQuery<Character>;
