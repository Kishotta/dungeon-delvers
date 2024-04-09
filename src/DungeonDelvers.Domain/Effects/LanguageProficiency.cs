namespace DungeonDelvers.Domain.Effects;

public class LanguageProficiency(Language language) : Effect
{
    public Language Language { get; private set; } = language;
    
    public override MaterializedCharacter Apply(MaterializedCharacter materializedCharacter)
    {
        materializedCharacter.LanguageProficiencies |= Language;
        return materializedCharacter;
    }
}