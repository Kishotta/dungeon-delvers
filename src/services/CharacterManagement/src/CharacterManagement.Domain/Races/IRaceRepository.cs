namespace CharacterManagement.Domain.Races;

public interface IRaceRepository
{
    Task AddRaceAsync (Race race, CancellationToken cancellationToken);
}