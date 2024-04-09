using DungeonDelvers.Domain.Effects;

namespace DungeonDelvers.Domain.Choices;

public class LanguageProficiencyChoice(Language options) : Choice
{
    public Language Options { get; private set; } = options;
    public Language Choice { get; private set; } = Language.None;
    
    public void MakeChoice(Character character, Language choice)
    {
        if (!Options.HasFlag(choice))
            throw new ArgumentException("Invalid choice");
     
        if (character.Materialize().LanguageProficiencies.HasFlag(choice))
            throw new ArgumentException("Character already has this proficiency.");

        Choice = choice;
        
        character.AddEffect(new LanguageProficiency(Choice)
        {
            Source = Source
        });

        character.RemoveChoice(this);
    }
}