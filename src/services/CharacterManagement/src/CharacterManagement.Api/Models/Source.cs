namespace CharacterManagement.Api.Models;

public class Source
{
    public Guid   Id      { get; set; }
    public Guid   OwnerId { get; set; }
    public string Name    { get; set; } = string.Empty;

    public List<Guid> RaceIds { get; private set; } = new();

    private Source () { }

    public Source (Guid ownerId, string name)
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
