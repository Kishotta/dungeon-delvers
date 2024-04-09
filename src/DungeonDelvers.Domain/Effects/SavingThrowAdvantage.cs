namespace DungeonDelvers.Domain.Effects;

public class SavingThrowAdvantage (Ability ability, string condition) : Effect
{
    public Ability SavingThrow { get; private set; } = ability;
    
    public override void Apply(MaterializedCharacter materializedCharacter)
    {
        switch (SavingThrow)
        {
            case Ability.Strength:
                materializedCharacter.StrengthSave.AddConditionalAdvantage(condition);
                return;
            case Ability.Dexterity:
                materializedCharacter.DexteritySave.AddConditionalAdvantage(condition);
                break;
            case Ability.Constitution:
                materializedCharacter.ConstitutionSave.AddConditionalAdvantage(condition);
                break;
            case Ability.Intelligence:
                materializedCharacter.IntelligenceSave.AddConditionalAdvantage(condition);
                break;
            case Ability.Wisdom:
                materializedCharacter.WisdomSave.AddConditionalAdvantage(condition);
                break;
            case Ability.Charisma:
                materializedCharacter.CharismaSave.AddConditionalAdvantage(condition);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(ability), SavingThrow, null);
        }
    }
}