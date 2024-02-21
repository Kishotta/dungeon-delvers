namespace CharacterManagement.Domain.Characters;

public class Character
{
    public Guid Id     { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; private set; } = string.Empty;

    private Character () { }

    public Character (Guid userId, string name)
    {
        Id = Guid.NewGuid ();
        UserId = userId;
        Name = name;
    }

    public void ChangeName (string name)
    {
        Name = name;
    }
}
