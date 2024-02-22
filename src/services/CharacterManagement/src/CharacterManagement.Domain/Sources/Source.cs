namespace CharacterManagement.Domain.Sources;

public class Source
{
    public Guid   Id      { get; set; }
    public Guid   OwnerId { get; set; }
    public string Name    { get; set; } = string.Empty;

    private Source () { }

    public Source (Guid ownerId, string name)
    {
        Id      = Guid.NewGuid ();
        OwnerId = ownerId;
        Name    = name;
    }

    public void ChangeName (string name)
    {
        Name = name;
    }
}
