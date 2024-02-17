using CharacterManagement.Application.Contracts;
using CharacterManagement.Domain;
using CharacterManagement.Domain.Characters;

namespace CharacterManagement.Application.DeleteCharacter;

public class DeleteCharacterCommandHandler(ICharacterRepository characterRepository, IUnitOfWork unitOfWork)
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
        await unitOfWork.SaveChangesAsync (cancellationToken);

        return Result.Success();
    }
}
