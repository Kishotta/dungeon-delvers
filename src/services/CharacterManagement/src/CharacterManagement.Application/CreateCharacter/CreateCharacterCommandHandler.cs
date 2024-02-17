using CharacterManagement.Application.Contracts;
using CharacterManagement.Domain;
using CharacterManagement.Domain.Characters;

namespace CharacterManagement.Application.CreateCharacter;

public class CreateCharacterCommandHandler (ICharacterRepository characterRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<CreateCharacterCommand, Character>
{
    public async Task<Result<Character>> Handle (CreateCharacterCommand request, CancellationToken cancellationToken)
    {
        var character = new Character (request.UserId, request.Name);

        await characterRepository.AddCharacter (character, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success (character);
    }
}
