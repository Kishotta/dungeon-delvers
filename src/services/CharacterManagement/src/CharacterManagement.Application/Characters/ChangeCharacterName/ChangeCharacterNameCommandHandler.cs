using CharacterManagement.Domain;
using CharacterManagement.Domain.Characters;

namespace CharacterManagement.Application.Characters.ChangeCharacterName;

public class ChangeCharacterNameCommandHandler(ICharacterRepository characterRepository, IUnitOfWork unitOfWork) : ICommandHandler<ChangeCharacterNameCommand, Character>
{
    public async Task<Result<Character>> Handle (ChangeCharacterNameCommand command, CancellationToken cancellationToken)
    {
        var character = await characterRepository.GetCharacterForUserAsync (command.CharacterId, command.UserId);
        if (character is null)
            return Result.Failure<Character> ("Character not found");

        character.ChangeName (command.Name);

        await unitOfWork.SaveChangesAsync (cancellationToken);

        return Result.Success (character);
    }
}
