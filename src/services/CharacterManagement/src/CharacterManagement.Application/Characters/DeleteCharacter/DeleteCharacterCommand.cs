namespace CharacterManagement.Application.Characters.DeleteCharacter;

public record DeleteCharacterCommand(Guid Id, Guid UserId) : ICommand;
