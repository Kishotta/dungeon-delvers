namespace DungeonDelvers.Domain.Effects;

public class Darkvision(uint distance) : Effect
{
    public uint Distance { get; private set; } = distance;
    
    public override MaterializedCharacter Apply(MaterializedCharacter materializedCharacter)
    {
        materializedCharacter.Darkvision.Set(Distance);
        return materializedCharacter;
    }
}