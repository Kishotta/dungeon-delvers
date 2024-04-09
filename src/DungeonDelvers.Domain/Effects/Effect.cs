namespace DungeonDelvers.Domain.Effects;

public abstract class Effect
{
    public IEffectSource? Source { get; set; }
    
    public abstract MaterializedCharacter Apply(MaterializedCharacter materializedCharacter);

    public Effect CloneWithEffectSource(IEffectSource source)
    {
        var clone = (Effect)MemberwiseClone();
        clone.Source = source;

        return clone;
    }
}