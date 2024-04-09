using DungeonDelvers.Domain.Effects;

namespace DungeonDelvers.Domain.Tests;

[TestFixture]
public class MovementSpeedAdjustmentTests
{
    private Character _character = default!;
    [SetUp]
    public void SetUp()
    {
        var race = Race.Create(null, "Test Race", "Description");
        _character = Character.Create(
            "Test character",
            race,
            Size.Medium,
            new AbilityScore(10),
            new AbilityScore(10),
            new AbilityScore(10),
            new AbilityScore(10),
            new AbilityScore(10),
            new AbilityScore(10));
    }
    
    [Test]
    public void Apply_PositiveAdjustment_IncreasesMovementSpeed()
    {
        var materializedCharacter = new MaterializedCharacter(_character);
        var effect = new MovementSpeedAdjustment(5);
        
        effect.Apply(materializedCharacter);
        
        Assert.That(materializedCharacter.MovementSpeed, Is.EqualTo(35));
    }
    
    [Test]
    public void Apply_NegativeAdjustment_DecreasesMovementSpeed()
    {
        var materializedCharacter = new MaterializedCharacter(_character);
        var effect = new MovementSpeedAdjustment(-10);
        
        effect.Apply(materializedCharacter);
        
        Assert.That(materializedCharacter.MovementSpeed, Is.EqualTo(20));
    }
    
    [Test]
    public void Apply_AdjustmentBelowZero_ClampsMovementSpeedToZero()
    {
        var materializedCharacter = new MaterializedCharacter(_character);
        var effect = new MovementSpeedAdjustment(-100);
        
        effect.Apply(materializedCharacter);
        
        Assert.That(materializedCharacter.MovementSpeed, Is.EqualTo(0));
    }
}