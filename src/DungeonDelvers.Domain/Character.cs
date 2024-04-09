using DungeonDelvers.Domain.Choices;
using DungeonDelvers.Domain.Effects;

namespace DungeonDelvers.Domain;

public class Character
{
    public string Name { get; private set; }
    public Race Race { get; private set; }
    public uint BaseMovementSpeed { get; private set; }
    public Size Size { get; private set; }
    public CreatureType CreatureType { get; private set; }
    
    public AbilityScore Strength { get; private set; }
    public AbilityScore Dexterity { get; private set; }
    public AbilityScore Constitution { get; private set; }
    public AbilityScore Intelligence { get; private set; }
    public AbilityScore Wisdom { get; private set; }
    public AbilityScore Charisma { get; private set; }
    
    public IReadOnlyList<Effect> Effects => _effects.AsReadOnly();
    
    private readonly List<Effect> _effects = [];
    
    public IReadOnlyList<Choice> Choices => _choices.AsReadOnly();
    private readonly List<Choice> _choices = [];

    private Character(
        string name, 
        Race race, 
        Size size, 
        AbilityScore strength,
        AbilityScore dexterity,
        AbilityScore constitution,
        AbilityScore intelligence,
        AbilityScore wisdom,
        AbilityScore charisma)
    {
        Name = name;
        Race = race;
        BaseMovementSpeed = race.BaseMovementSpeed;
        Size = size;
        CreatureType = race.CreatureType;
        
        Strength = strength;
        Dexterity = dexterity;
        Constitution = constitution;
        Intelligence = intelligence;
        Wisdom = wisdom;
        Charisma = charisma;
        
        _effects.AddRange(race.GetEffects());
        _choices.AddRange(race.GetChoices());
    }

    public static Character Create(
        string name, 
        Race race, 
        Size size, 
        AbilityScore strength,
        AbilityScore dexterity,
        AbilityScore constitution,
        AbilityScore intelligence,
        AbilityScore wisdom,
        AbilityScore charisma)
    {
        return new Character(
            name, 
            race,
            size,
            strength,
            dexterity,
            constitution,
            intelligence,
            wisdom,
            charisma);
    }
    
    public void AddEffect(Effect effect)
    {
        _effects.Add(effect);
    }
    
    public void AddChoice(Choice choice)
    {
        _choices.Add(choice);
    }

    public void RemoveChoice(Choice choice)
    {
        _choices.Remove(choice);
    }

    public MaterializedCharacter Materialize() =>
        _effects.Aggregate(
            new MaterializedCharacter(this),
            (materializedCharacter, effect) => effect.Apply(materializedCharacter));
}