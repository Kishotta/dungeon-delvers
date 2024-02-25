using CharacterManagement.Api.Models;
using CharacterManagement.Api.Models.Races;
using CharacterManagement.Api.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CharacterManagement.Api.Repositories;

public class RaceRepository(CharacterManagementContext context)
{
    public async Task<IEnumerable<Race>> GetByOwnerIdAsync (Guid userId, CancellationToken cancellationToken)
    {
        return await context.Races.Include (r => r.Traits).Where (s => s.OwnerId == userId).ToListAsync(cancellationToken);
    }

    public async Task<Race?> GetByIdAsync (Guid id, CancellationToken cancellationToken)
    {
        return await context.Races.Include (r => r.Traits).FirstOrDefaultAsync (s => s.Id == id, cancellationToken);
    }

    public async Task AddSourceAsync (Race source, CancellationToken cancellationToken)
    {
        await context.Races.AddAsync (source, cancellationToken);
    }

    public void DeleteSource (Race source)
    {
        context.Races.Remove (source);
    }
}
