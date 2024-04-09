using DungeonDelvers.Domain.Effects;

namespace DungeonDelvers.Domain.Choices;

public class SkillProficiencyChoice(Skill options, int numberOfChoices) : Choice
{
    public Skill Options { get; private set; } = options;
    public int NumberOfChoices { get; private set; } = numberOfChoices;

    public Skill Choice { get; set; } = Skill.None;

    public void MakeChoice(Character character, Skill choice)
    {
        if (!Options.HasFlag(choice))
            throw new ArgumentException("Invalid choice");
        
        var choiceCount = GetChoiceCount(choice);
        if (choiceCount != NumberOfChoices)
            throw new ArgumentException($"Invalid number of choices. Expected {NumberOfChoices}, got {choiceCount}");

        if (character.Materialize().SkillProficiencies.HasFlag(choice))
            throw new ArgumentException("Character already has this proficiency.");
        
        Choice = choice;
        
        character.AddEffect(new SkillProficiency(Choice)
        {
            Source = Source
        });

        character.RemoveChoice(this);
    }

    private static int GetChoiceCount(Skill choice) => 
        Convert.ToString((int)choice, 2).Count(c => c == '1');
}