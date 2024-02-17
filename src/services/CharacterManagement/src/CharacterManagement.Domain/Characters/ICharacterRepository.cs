namespace CharacterManagement.Domain.Characters;

public interface ICharacterRepository
{
    IEnumerable<Character> GetCharactersForUser (Guid userId);
    Character? GetCharacterForUser (Guid id, Guid userId);
    Task AddCharacter (Character character, CancellationToken cancellationToken);
    void DeleteCharacter (Character character);
}
