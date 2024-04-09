using DungeonDelvers.Domain.Effects;

namespace DungeonDelvers.Domain.Tests;

[TestFixture]
public class SizeOverrideTests
{
    [Test]
    public void Apply_SizeOverrideApplied()
    {
        var race = Race.Create("Test Race", "Description");
        var character = Character.Create(
            "Test character",
            race,
            Size.Medium,
            new AbilityScore(10),
            new AbilityScore(10),
            new AbilityScore(10),
            new AbilityScore(10),
            new AbilityScore(10),
            new AbilityScore(10));
        var materializedCharacter = new MaterializedCharacter(character);
        var effect = new SizeOverride(Size.Gargantuan);
        
        effect.Apply(materializedCharacter);
        
        Assert.That(materializedCharacter.Size, Is.EqualTo(Size.Gargantuan));
    }
}