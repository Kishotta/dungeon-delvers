using DungeonDelvers.Domain.Effects;

namespace DungeonDelvers.Domain;

public class Character
{
    public string Name { get; private set; }
    public Race Race { get; private set; }
    public uint BaseMovementSpeed { get; private set; }
    public Size Size { get; private set; }
    public CreatureType CreatureType { get; private set; }
    
    public IReadOnlyList<Effect> Effects => _effects.AsReadOnly();
    
    
    private readonly List<Effect> _effects = [];

    private Character(string name, Race race, uint baseMovementSpeed, Size size, CreatureType creatureType)
    {
        Name = name;
        Race = race;
        BaseMovementSpeed = baseMovementSpeed;
        Size = size;
        CreatureType = creatureType;
    }
    
    public static Character Create(string name, Race race, uint baseMovementSpeed, Size size, CreatureType creatureType)
    {
        return new Character(name, race, baseMovementSpeed, size, creatureType);
    }
    
    public void AddEffect(Effect effect)
    {
        _effects.Add(effect);
    }

    public MaterializedCharacter Materialize()
    {
        var materializedCharacter = new MaterializedCharacter
        {
            Name = Name,
            MovementSpeed = (int)BaseMovementSpeed,
            Size = Size,
            CreatureType = CreatureType,
        };

        foreach (var effect in Race.Traits.SelectMany(t => t.Effects))
        {
            effect.Apply(materializedCharacter);
        }
        
        foreach (var effect in _effects)
        {
            effect.Apply(materializedCharacter);
        }

        return materializedCharacter;
    }
}