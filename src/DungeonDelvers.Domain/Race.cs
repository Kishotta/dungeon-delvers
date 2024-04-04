namespace DungeonDelvers.Domain;

public class Race
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public IReadOnlyList<Trait> Traits => _traits.AsReadOnly();
    
    private readonly List<Trait> _traits = [];
    
    private Race(string name, string description)
    {
        Name = name;
        Description = description;
    }
    
    public static Race Create(string name, string description)
    {
        return new Race(name, description);
    }
    
    public void AddTrait(Trait trait)
    {
        _traits.Add(trait);
    }
}