using CharacterManagement.Domain.Sources;
using Microsoft.EntityFrameworkCore;

namespace CharacterManagement.Infrastructure;

public class SourceRepository(CharacterManagementContext context) : ISourceRepository
{
    public async Task<IEnumerable<Source>> GetByUserIdAsync (Guid userId)
    {
        return await context.Sources.Where (s => s.OwnerId == userId).ToListAsync();
    }

    public async Task<Source?> GetByIdAsync (Guid id)
    {
        return await context.Sources.FirstOrDefaultAsync (s => s.Id == id);
    }

    public async Task AddSourceAsync (Source source, CancellationToken cancellationToken)
    {
        await context.Sources.AddAsync (source, cancellationToken);
    }

    public void DeleteSource (Source source)
    {
         context.Sources.Remove (source);
    }
}
