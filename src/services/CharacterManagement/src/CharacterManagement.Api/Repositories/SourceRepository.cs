using CharacterManagement.Api.Models;
using CharacterManagement.Api.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CharacterManagement.Api.Repositories;

public class SourceRepository(CharacterManagementContext context)
{
    public async Task<IEnumerable<Source>> GetByOwnerIdAsync (Guid userId, CancellationToken cancellationToken)
    {
        return await context.Sources.Where (s => s.OwnerId == userId).ToListAsync(cancellationToken);
    }

    public async Task<Source?> GetByIdAsync (Guid id, CancellationToken cancellationToken)
    {
        return await context.Sources.FirstOrDefaultAsync (s => s.Id == id, cancellationToken);
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
