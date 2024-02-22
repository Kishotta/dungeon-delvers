using CharacterManagement.Domain.Characters;
using Microsoft.EntityFrameworkCore;

namespace CharacterManagement.Infrastructure;

public class CharacterRepository(CharacterManagementContext context) : ICharacterRepository
{
    public async Task<IEnumerable<Character>> GetByUserIdAsync (Guid userId)
    {
        return await context.Characters.Where (c => c.OwnerId == userId).ToListAsync();
    }

    public async Task<Character?> GetByIdAsync (Guid id)
    {
        return await context.Characters.FirstOrDefaultAsync (c => c.Id == id);
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
