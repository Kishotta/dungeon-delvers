using CharacterManagement.Domain.Sources;

namespace CharacterManagement.Domain.Races;

public class Race
{
    public Guid   Id      { get; set; }
    public Guid   OwnerId { get; set; }
    public string Name    { get; private set; } = string.Empty;

    public List<Source> Sources { get; private set; } = new();

    private Race () { }

    public Race (Guid ownerId, string name)
    {
        Id      = Guid.NewGuid ();
        OwnerId = ownerId;
        Name    = name;
    }

    public bool OwnedBy (Guid userId) => OwnerId == userId;

    public void ChangeName (string name)
    {
        Name = name;
    }
}
