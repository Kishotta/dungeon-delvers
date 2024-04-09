namespace DungeonDelvers.Domain.Effects;

public class Darkvision(uint distance) : Effect
{
    public override void Apply(MaterializedCharacter materializedCharacter)
    {
        materializedCharacter.Darkvision.Set(distance);
    }
}