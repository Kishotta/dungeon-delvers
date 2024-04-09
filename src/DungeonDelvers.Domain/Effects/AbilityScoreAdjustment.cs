namespace DungeonDelvers.Domain.Effects;

public class AbilityScoreAdjustment(Ability ability, int adjustment) : Effect
{
    public override MaterializedCharacter Apply(MaterializedCharacter materializedCharacter)
    {
        switch (ability)
        {
            case Ability.Strength:
                materializedCharacter.Strength += adjustment;
                break;
            case Ability.Dexterity:
                materializedCharacter.Dexterity += adjustment;
                break;
            case Ability.Constitution:
                materializedCharacter.Constitution += adjustment;
                break;
            case Ability.Intelligence:
                materializedCharacter.Intelligence += adjustment;
                break;
            case Ability.Wisdom:
                materializedCharacter.Wisdom += adjustment;
                break;
            case Ability.Charisma:
                materializedCharacter.Charisma += adjustment;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(ability), ability, null);
        }

        return materializedCharacter;
    }
}