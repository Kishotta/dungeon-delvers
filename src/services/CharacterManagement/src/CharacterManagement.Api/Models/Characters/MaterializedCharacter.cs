namespace CharacterManagement.Api.Models.Characters;

public class MaterializedCharacter
{
    public Guid   Id            { get; set; }
    public string Name          { get; set; } = string.Empty;
    public string Race          { get; set; } = string.Empty;
    public string Size          { get; set; } = string.Empty;
    public string CreatureType  { get; set; } = string.Empty;
    public int    MovementSpeed { get; set; }
    public int    SwimSpeed     { get; set; }
    public int    ClimbSpeed    { get; set; }
    public int    FlySpeed      { get; set; }
    public int    BurrowSpeed   { get; set; }

}
