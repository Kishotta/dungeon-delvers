namespace CharacterManagement.Domain.Characters;

public interface ICharacterRepository
{
    Task<IEnumerable<Character>> GetCharactersForUserAsync (Guid userId);
    Task<Character?> GetCharacterForUserAsync (Guid id, Guid userId);
    Task AddCharacterAsync (Character character, CancellationToken cancellationToken);
    void DeleteCharacter (Character character);
}
