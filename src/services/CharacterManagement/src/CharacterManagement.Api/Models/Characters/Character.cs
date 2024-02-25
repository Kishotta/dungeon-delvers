using CharacterManagement.Api.Models.Races;

namespace CharacterManagement.Api.Models.Characters;

public class Character
{
    public Guid         Id       { get; set; }
    public Guid         OwnerId  { get; set; }
    public string       Name     { get; private set; } = string.Empty;
    public string       RaceName { get; private set; } = string.Empty;
    public List<Effect> Effects  { get; private set; } = new();

    private Character () { }

    public Character (Guid ownerId, string name, Race race)
    {
        Id = Guid.NewGuid ();
        OwnerId = ownerId;
        Name = name;

        SetRace (race);
    }

    public void ChangeName (string name)
    {
        Name = name;
    }

    public void SetRace (Race race)
    {
        RaceName = race.Name;
        var raceEffects = race.CreateEffectsCopyForCharacter ();
        Effects.AddRange (raceEffects);
    }

    public MaterializedCharacter Materialize ()
    {
        var materializedCharacter = new MaterializedCharacter
                                    {
                                        Id = Id,
                                        Name = Name,
                                        Race = RaceName,
                                    };


        foreach (var effect in Effects)
        {
            effect.Apply (materializedCharacter);
        }

        return materializedCharacter;
    }
}
