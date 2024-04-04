using DungeonDelvers.Domain.Effects;

namespace DungeonDelvers.Domain.Tests;

[TestFixture]
public class MovementSpeedAdjustmentTests
{
    [Test]
    public void Apply_PositiveAdjustment_IncreasesMovementSpeed()
    {
        var materializedCharacter = new MaterializedCharacter();
        var effect = new MovementSpeedAdjustment(5);
        
        effect.Apply(materializedCharacter);
        
        Assert.That(materializedCharacter.MovementSpeed, Is.EqualTo(35));
    }
    
    [Test]
    public void Apply_NegativeAdjustment_DecreasesMovementSpeed()
    {
        var materializedCharacter = new MaterializedCharacter();
        var effect = new MovementSpeedAdjustment(-10);
        
        effect.Apply(materializedCharacter);
        
        Assert.That(materializedCharacter.MovementSpeed, Is.EqualTo(20));
    }
    
    [Test]
    public void Apply_AdjustmentBelowZero_ClampsMovementSpeedToZero()
    {
        var materializedCharacter = new MaterializedCharacter();
        var effect = new MovementSpeedAdjustment(-100);
        
        effect.Apply(materializedCharacter);
        
        Assert.That(materializedCharacter.MovementSpeed, Is.EqualTo(0));
    }
}