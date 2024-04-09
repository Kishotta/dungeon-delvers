namespace DungeonDelvers.Domain;

public class SavingThrow
{
    public List<string> Advantages { get; private set; } = [];
    public List<string> Disadvantages { get; private set; } = [];

    public int Modifier(Ability ability, MaterializedCharacter materializedCharacter) =>
        ability switch
        {
            Ability.Strength => materializedCharacter.Strength.Modifier,
            Ability.Dexterity => materializedCharacter.Dexterity.Modifier,
            Ability.Constitution => materializedCharacter.Constitution.Modifier,
            Ability.Intelligence => materializedCharacter.Intelligence.Modifier,
            Ability.Wisdom => materializedCharacter.Wisdom.Modifier,
            Ability.Charisma => materializedCharacter.Charisma.Modifier,
            _ => throw new ArgumentOutOfRangeException(nameof(ability), ability, null)
        };

    public void AddConditionalAdvantage(string condition)
    {
        Advantages.Add(condition);
    }
}