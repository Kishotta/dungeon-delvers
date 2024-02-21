using CharacterManagement.Domain.Characters;

namespace CharacterManagement.Application.Characters.CreateCharacter;

public record CreateCharacterCommand(Guid UserId, string Name)
    : ICommand<Character>;
