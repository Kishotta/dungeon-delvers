using DungeonDelvers.Domain.Effects;

namespace DungeonDelvers.Domain;

public class Trait
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public IReadOnlyList<Effect> Effects => _effects.AsReadOnly();
    
    private readonly List<Effect> _effects = [];
    
    private Trait(string name, string description)
    {
        Name = name;
        Description = description;
    }
    
    public static Trait Create(string name, string description)
    {
        return new Trait(name, description);
    }
    
    public void AddEffect(Effect effect)
    {
        _effects.Add(effect);
    }
}