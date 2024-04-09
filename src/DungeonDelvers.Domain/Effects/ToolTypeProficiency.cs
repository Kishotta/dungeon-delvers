namespace DungeonDelvers.Domain.Effects;

public class ToolTypeProficiency(ToolType toolType) : Effect
{
    public ToolType ToolType { get; private set; } = toolType;
    
    public override MaterializedCharacter Apply(MaterializedCharacter materializedCharacter)
    {
        materializedCharacter.ToolProficiencies |= ToolType;
        return materializedCharacter;
    }
}