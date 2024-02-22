namespace CharacterManagement.Api.Models;

public class Character
{
    public Guid Id     { get; set; }
    public Guid OwnerId { get; set; }
    public string Name { get; private set; } = string.Empty;

    private Character () { }

    public Character (Guid ownerId, string name)
    {
        Id = Guid.NewGuid ();
        OwnerId = ownerId;
        Name = name;
    }

    public bool OwnedBy (Guid userId) => OwnerId == userId;

    public void ChangeName (string name)
    {
        Name = name;
    }
}
