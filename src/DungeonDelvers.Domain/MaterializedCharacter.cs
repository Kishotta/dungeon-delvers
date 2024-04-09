namespace DungeonDelvers.Domain;

public class MaterializedCharacter
{
    public string Name { get; set; } = string.Empty;
    public int MovementSpeed { get; set; } = 30;
    public Size Size { get; set; } = Size.Medium;
    public CreatureType CreatureType { get; set; } = CreatureType.Humanoid;
    
    public AbilityScore Strength { get; set; }
    public AbilityScore Dexterity { get; set; }
    public AbilityScore Constitution { get; set; }
    public AbilityScore Intelligence { get; set; }
    public AbilityScore Wisdom { get; set; }
    public AbilityScore Charisma { get; set; }
    
    public Sense Darkvision { get; set; }
}