using CharacterManagement.Application.Contracts;
using CharacterManagement.Domain;
using CharacterManagement.Domain.Characters;

namespace CharacterManagement.Application.CreateCharacter;

public record CreateCharacterCommand(Guid UserId, string Name)
    : ICommand<Character>;
