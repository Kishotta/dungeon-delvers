namespace CharacterManagement.Domain.Characters;

public interface ICharacterRepository
{
    Task<IEnumerable<Character>> GetByUserIdAsync (Guid userId);
    Task<Character?> GetByIdAsync (Guid id);
    Task AddCharacterAsync (Character character, CancellationToken cancellationToken);
    void DeleteCharacter (Character character);
}
