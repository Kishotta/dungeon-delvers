using CharacterManagement.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CharacterManagement.Api.Repositories;

public class CharacterRepository(CharacterManagementContext context)
{
    public async Task<IEnumerable<Character>> GetByOwnerIdAsync (Guid userId, CancellationToken cancellationToken)
    {
        return await context.Characters.Where (c => c.OwnerId == userId).ToListAsync(cancellationToken);
    }

    public async Task<Character?> GetByIdAsync (Guid id, CancellationToken cancellationToken)
    {
        return await context.Characters.FirstOrDefaultAsync (c => c.Id == id, cancellationToken);
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
