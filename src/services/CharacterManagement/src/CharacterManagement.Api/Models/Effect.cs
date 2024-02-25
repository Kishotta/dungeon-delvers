using CharacterManagement.Api.Models.Characters;

namespace CharacterManagement.Api.Models;

public abstract class Effect
{
    public Guid Id { get; set; } = Guid.NewGuid ();

    public abstract void Apply (MaterializedCharacter materializedCharacter);

    public Effect Clone ()
    {
        var clone = (Effect)MemberwiseClone ();
        clone.Id = Guid.NewGuid ();
        return clone;
    }
}
