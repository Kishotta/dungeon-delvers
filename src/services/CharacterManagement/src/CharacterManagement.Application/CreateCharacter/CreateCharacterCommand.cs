using CharacterManagement.Application.Contracts;

namespace CharacterManagement.Application.CreateCharacter;

public record CreateCharacterCommand(Guid UserId, string Name)
    : ICommand<CreateCharacterResponse>;
