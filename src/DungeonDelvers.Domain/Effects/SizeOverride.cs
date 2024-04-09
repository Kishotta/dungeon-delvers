namespace DungeonDelvers.Domain.Effects;

public class SizeOverride(Size size) : Effect
{
    public Size Size { get; private set; } = size;

    public override MaterializedCharacter Apply(MaterializedCharacter materializedCharacter)
    {
        materializedCharacter.Size = Size;
        return materializedCharacter;
    }
}