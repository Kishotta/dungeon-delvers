namespace CharacterManagement.Domain.Characters;

public class Character
{
    public Guid Id     { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; } = string.Empty;

    private Character () { }

    public Character (Guid userId, string name)
    {
        Id = Guid.NewGuid ();
        UserId = userId;
        Name = name;
    }
}
