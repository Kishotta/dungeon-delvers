namespace DungeonDelvers.Domain.Effects;

public interface IEffectSource
{
    public IEnumerable<Effect> GetEffects();
}