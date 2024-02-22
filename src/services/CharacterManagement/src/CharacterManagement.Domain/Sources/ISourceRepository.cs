namespace CharacterManagement.Domain.Sources;

public interface ISourceRepository
{
    Task<IEnumerable<Source>> GetByOwnerIdAsync (Guid userId, CancellationToken cancellationToken);
    Task<Source?> GetByIdAsync (Guid id, CancellationToken cancellationToken);
    Task AddSourceAsync (Source source, CancellationToken cancellationToken);
    void DeleteSource (Source source);
}
