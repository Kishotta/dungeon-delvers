using CharacterManagement.Application.Contracts;

namespace CharacterManagement.Application.DeleteCharacter;

public record DeleteCharacterCommand(Guid Id, Guid UserId) : ICommand;
