using DungeonDelvers.Domain.Effects;

namespace DungeonDelvers.Domain.Choices;

public abstract class Choice
{
    public IEffectSource? Source { get; set; }
    
    public Choice CloneWithEffectSource(IEffectSource source)
    {
        var clone = (Choice)MemberwiseClone();
        clone.Source = source;

        return clone;
    }
}