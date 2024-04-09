using DungeonDelvers.Domain.Effects;

namespace DungeonDelvers.Domain;

public class Race : IEffectSource
{
    public Race? Parent { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public CreatureType CreatureType { get; private set; }

    public uint BaseMovementSpeed { get; private set; }
    public int AgeOfMaturity { get; private set; }
    public int AverageLifespan { get; private set; }
    
    public IReadOnlyList<Trait> Traits => _traits.AsReadOnly();
    
    private readonly List<Trait> _traits = [];
    
    private Race(
        Race? parent,
        string name,
        string description, 
        CreatureType creatureType = CreatureType.Humanoid,
        uint baseMovementSpeed = 30u, 
        int ageOfMaturity = 18, 
        int averageLifespan = 100)
    {
        Parent = parent;
        Name = name;
        Description = description;
        CreatureType = creatureType;
        BaseMovementSpeed = baseMovementSpeed;
        AgeOfMaturity = ageOfMaturity;
        AverageLifespan = averageLifespan;
    }

    public static Race Create(
        Race? parent,
        string name, 
        string description, 
        CreatureType creatureType = CreatureType.Humanoid, 
        uint baseSpeed = 30u, 
        int ageOfMaturity = 18,
        int averageLifespan = 100)
    {
        return new Race(parent, name, description, creatureType, baseSpeed, ageOfMaturity, averageLifespan);
    }
    
    public void AddTrait(Trait trait)
    {
        _traits.Add(trait);
    }

    public IEnumerable<Effect> GetEffects()
    {
        return Traits
            .SelectMany(trait => trait.Effects)
            .Select(effect => effect.CloneWithEffectSource(this))
            .Concat(Parent?.GetEffects() ?? Array.Empty<Effect>());
    }
}