using DungeonDelvers.Domain.Choices;
using DungeonDelvers.Domain.Effects;

namespace DungeonDelvers.Domain;

public class Trait
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    
    public IReadOnlyList<Effect> Effects => _effects.AsReadOnly();
    
    private readonly List<Effect> _effects = [];
    
    public IReadOnlyList<Choice> Choices => _choices.AsReadOnly();
    
    private readonly List<Choice> _choices = [];
    
    private Trait(string name, string description)
    {
        Name = name;
        Description = description;
    }
    
    public static Trait Create(
        string name,
        string description) => 
        new(name, description);

    public void AddEffect(Effect effect)
    {
        _effects.Add(effect);
    }
    
    public void AddChoice(Choice choice)
    {
        _choices.Add(choice);
    }
}