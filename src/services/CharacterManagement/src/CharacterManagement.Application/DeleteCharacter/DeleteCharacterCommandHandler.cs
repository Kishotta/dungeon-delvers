using CharacterManagement.Application.Contracts;
using CharacterManagement.Domain;

namespace CharacterManagement.Application.DeleteCharacter;

public class DeleteCharacterCommandHandler(ICharacterRepository characterRepository)
    : ICommandHandler<DeleteCharacterCommand>
{
    public async Task<Result> Handle(DeleteCharacterCommand request, CancellationToken cancellationToken)
    {
        var character = characterRepository.GetCharacterForUser(request.Id, request.UserId);
        if (character == null)
        {
            return Result.Failure("Character does not exist");
        }

        if (character.UserId != request.UserId)
        {
            return Result.Failure("You do not have permission to delete this character");
        }

        characterRepository.DeleteCharacter(character);
        await characterRepository.SaveChanges (cancellationToken);

        return Result.Success();
    }
}
