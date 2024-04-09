namespace DungeonDelvers.Domain.Effects;

public class ArmorTypeProficiency(ArmorType armorType) : Effect
{
    public ArmorType ArmorType { get; private set; } = armorType;
    
    public override MaterializedCharacter Apply(MaterializedCharacter materializedCharacter)
    {
        materializedCharacter.ArmorTypeProficiencies |= ArmorType;
        return materializedCharacter;
    }
}