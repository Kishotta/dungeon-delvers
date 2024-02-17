using CharacterManagement.Application.Contracts;
using CharacterManagement.Domain;

namespace CharacterManagement.Application.CreateCharacter;

public class CreateCharacterCommandHandler (ICharacterRepository characterRepository)
    : ICommandHandler<CreateCharacterCommand, CreateCharacterResponse>
{
    public async Task<Result<CreateCharacterResponse>> Handle (CreateCharacterCommand request, CancellationToken cancellationToken)
    {
        var character = new Character
                        {
                            Id = Guid.NewGuid(),
                            UserId = request.UserId,
                            Name = request.Name
                        };

        await characterRepository.AddCharacter (character, cancellationToken);
        await characterRepository.SaveChanges(cancellationToken);

        return Result.Success (new CreateCharacterResponse (character.Id, character.UserId, character.Name));
    }
}
