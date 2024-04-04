using DungeonDelvers.Domain.Effects;

namespace DungeonDelvers.Domain.Tests;

[TestFixture]
public class SizeOverrideTests
{
    [Test]
    public void Apply_SizeOverrideApplied()
    {
        var materializedCharacter = new MaterializedCharacter();
        var effect = new SizeOverride(Size.Gargantuan);
        
        effect.Apply(materializedCharacter);
        
        Assert.That(materializedCharacter.Size, Is.EqualTo(Size.Gargantuan));
    }
}