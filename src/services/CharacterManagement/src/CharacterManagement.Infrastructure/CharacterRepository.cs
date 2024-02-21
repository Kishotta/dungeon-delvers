using CharacterManagement.Domain.Characters;
using Microsoft.EntityFrameworkCore;

namespace CharacterManagement.Infrastructure;

public class CharacterRepository(CharacterManagementContext context) : ICharacterRepository
{
    public async Task<IEnumerable<Character>> GetCharactersForUserAsync (Guid userId)
    {
        return await context.Characters.Where (c => c.UserId == userId).ToListAsync();
    }

    public async Task<Character?> GetCharacterForUserAsync (Guid id, Guid userId)
    {
        return await context.Characters.FirstOrDefaultAsync (c => c.Id == id && c.UserId == userId);
    }

    public async Task AddCharacterAsync (Character character, CancellationToken cancellationToken)
    {
        await context.Characters.AddAsync (character, cancellationToken);
    }

    public void DeleteCharacter (Character character)
    {
         context.Characters.Remove (character);
    }
}
