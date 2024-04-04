namespace DungeonDelvers.Domain;

public class MaterializedCharacter
{
    public string Name { get; set; } = string.Empty;
    public int MovementSpeed { get; set; } = 30;
    public Size Size { get; set; } = Size.Medium;
    public CreatureType CreatureType { get; set; } = CreatureType.Humanoid;
}