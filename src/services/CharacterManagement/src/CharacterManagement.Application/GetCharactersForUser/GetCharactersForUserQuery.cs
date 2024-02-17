using CharacterManagement.Application.Contracts;
using CharacterManagement.Domain;
using CharacterManagement.Domain.Characters;
using MediatR;

namespace CharacterManagement.Application.GetCharactersForUser;

public record GetCharactersForUserQuery(Guid UserId) : IQuery<IEnumerable<Character>>;
