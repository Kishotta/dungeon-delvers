using CharacterManagement.Domain.Races;

namespace CharacterManagement.Infrastructure;

public class RaceRepository(CharacterManagementContext context) : IRaceRepository
{
    public async Task AddRaceAsync (Race race, CancellationToken cancellationToken)
    {
        await context.Races.AddAsync (race, cancellationToken);
    }
}
