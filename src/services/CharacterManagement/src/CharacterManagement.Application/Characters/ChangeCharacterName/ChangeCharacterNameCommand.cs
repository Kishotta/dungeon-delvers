using CharacterManagement.Domain.Characters;

namespace CharacterManagement.Application.Characters.ChangeCharacterName;

public record ChangeCharacterNameCommand(Guid UserId, Guid CharacterId, string Name) : ICommand<Character>;
