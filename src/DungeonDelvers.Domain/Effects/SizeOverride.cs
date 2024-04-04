namespace DungeonDelvers.Domain.Effects;

public class SizeOverride : Effect
{
    public Size Size { get; private set; }
    
    public SizeOverride(Size size)
    {
        Size = size;
    }

    public override void Apply(MaterializedCharacter materializedCharacter)
    {
        materializedCharacter.Size = Size;
    }
}