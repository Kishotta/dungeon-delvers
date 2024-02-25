namespace CharacterManagement.Api.Models.Races;

public class Race
{
    public Guid         Id      { get; set; }
    public Guid         OwnerId { get; set; }
    public string       Name    { get; private set; } = string.Empty;
    public IList<Trait>  Traits  { get; private set; } = new List<Trait>();

    public IList<Guid> SourceIds { get; private set; } = new List<Guid>();

    private Race () { }

    public Race (Guid ownerId, string name)
    {
        Id      = Guid.NewGuid ();
        OwnerId = ownerId;
        Name    = name;
    }

    public IEnumerable<Effect> CreateEffectsCopyForCharacter ()
    {
        return from trait in Traits from effect in trait.Effects select effect.Clone ();
    }

    public bool OwnedBy (Guid userId) => OwnerId == userId;

    public void ChangeName (string name)
    {
        Name = name;
    }

    public void AddToSource (Source source)
    {
        SourceIds.Add (source.Id);
        source.RaceIds.Add (Id);
    }

    public void RemoveFromSource (Source source)
    {
        SourceIds.Remove (source.Id);
        source.RaceIds.Remove (Id);
    }
}
