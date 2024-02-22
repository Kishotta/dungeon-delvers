using CharacterManagement.Domain;
using CharacterManagement.Domain.Characters;

namespace CharacterManagement.Application.Characters.DeleteCharacter;

public class DeleteCharacterCommandHandler(ICharacterRepository characterRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteCharacterCommand>
{
    public async Task<Result> Handle(DeleteCharacterCommand request, CancellationToken cancellationToken)
    {
        var character = await characterRepository.GetByIdAsync(request.Id);

        if (character is null || !character.OwnedBy (request.UserId))
            return Result.Failure("Character not found");

        characterRepository.DeleteCharacter(character);
        await unitOfWork.SaveChangesAsync (cancellationToken);

        return Result.Success();
    }
}
