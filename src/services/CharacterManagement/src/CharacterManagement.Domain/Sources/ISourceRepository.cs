namespace CharacterManagement.Domain.Sources;

public interface ISourceRepository
{
    Task<IEnumerable<Source>> GetByUserIdAsync (Guid userId);
    Task<Source?> GetByIdAsync (Guid id);
    Task AddSourceAsync (Source source, CancellationToken cancellationToken);
    void DeleteSource (Source source);
}
