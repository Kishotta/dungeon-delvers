using DungeonDelvers.Domain.Effects;

namespace DungeonDelvers.Domain.Choices;

public class ToolTypeProficiencyChoice(ToolType options) : Choice
{
    public ToolType Options { get; private set; } = options;
    public ToolType Choice { get; private set; } = ToolType.None;
    
    public void MakeChoice(Character character, ToolType choice)
    {
        if (!Options.HasFlag(choice))
            throw new ArgumentException("Invalid choice");
        
        Choice = choice;
        
        character.AddEffect(new ToolTypeProficiency(Choice)
        {
            Source = Source
        });

        character.RemoveChoice(this);
    }
}