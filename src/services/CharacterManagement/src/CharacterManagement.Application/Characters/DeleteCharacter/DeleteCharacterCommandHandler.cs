using CharacterManagement.Domain;
using CharacterManagement.Domain.Characters;

namespace CharacterManagement.Application.Characters.DeleteCharacter;

public class DeleteCharacterCommandHandler(ICharacterRepository characterRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteCharacterCommand>
{
    public async Task<Result> Handle(DeleteCharacterCommand request, CancellationToken cancellationToken)
    {
        var character = await characterRepository.GetCharacterForUserAsync(request.Id, request.UserId);

        if (character is null)
            return Result.Failure("Character not found");

        if (character.UserId != request.UserId)
            return Result.Failure ("You do not have permission to delete this character");

        characterRepository.DeleteCharacter(character);
        await unitOfWork.SaveChangesAsync (cancellationToken);

        return Result.Success();
    }
}
