using CharacterManagement.Domain.Characters;

namespace CharacterManagement.Infrastructure;

public class CharacterRepository(CharacterManagementContext context) : ICharacterRepository
{
    public IEnumerable<Character> GetCharactersForUser (Guid userId)
    {
        return context.Characters.Where (c => c.UserId == userId);
    }

    public Character? GetCharacterForUser (Guid id, Guid userId)
    {
        return context.Characters.FirstOrDefault (c => c.Id == id && c.UserId == userId);
    }

    public async Task AddCharacter (Character character, CancellationToken cancellationToken)
    {
        await context.Characters.AddAsync (character, cancellationToken);
    }

    public void DeleteCharacter (Character character)
    {
         context.Characters.Remove (character);
    }
}
