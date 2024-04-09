namespace DungeonDelvers.Domain.Effects;

public class SavingThrowAdvantage (Ability ability, string condition) : Effect
{
    public Ability SavingThrow { get; private set; } = ability;
    public string Condition { get; private set; } = condition;
    
    public override MaterializedCharacter Apply(MaterializedCharacter materializedCharacter)
    {
        switch (SavingThrow)
        {
            case Ability.Strength:
                materializedCharacter.StrengthSave.AddConditionalAdvantage(Condition);
                break;
            case Ability.Dexterity:
                materializedCharacter.DexteritySave.AddConditionalAdvantage(Condition);
                break;
            case Ability.Constitution:
                materializedCharacter.ConstitutionSave.AddConditionalAdvantage(Condition);
                break;
            case Ability.Intelligence:
                materializedCharacter.IntelligenceSave.AddConditionalAdvantage(Condition);
                break;
            case Ability.Wisdom:
                materializedCharacter.WisdomSave.AddConditionalAdvantage(Condition);
                break;
            case Ability.Charisma:
                materializedCharacter.CharismaSave.AddConditionalAdvantage(Condition);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(ability), SavingThrow, null);
        }

        return materializedCharacter;
    }
}