namespace CharacterManagement.Api.Models.Races;

public class Trait
{
    public Guid Id { get; private set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<Effect> Effects { get; set; } = new();

    private Trait () { }

    public Trait (string name, string description)
    {
        Id          = Guid.NewGuid ();
        Name        = name;
        Description = description;
    }
}
