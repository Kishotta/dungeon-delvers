namespace DungeonDelvers.Domain.Effects;

public abstract class Effect
{
    public string Type { get; set; }
    
    public abstract void Apply(MaterializedCharacter materializedCharacter);
}