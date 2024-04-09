namespace DungeonDelvers.Domain.Tests;

[TestFixture]
public class AbilityScoreTests
{
    [Test]
    public void AbilityScore_ValidValue_CreatesInstance()
    {
        var abilityScore = new AbilityScore(10);
        Assert.That(abilityScore.BaseScore, Is.EqualTo(10));
    }
    
    [Test]
    public void AbilityScore_InvalidValue_ClampsToValidRange()
    {
        var invalidLowScore = new AbilityScore(-5);
        Assert.That(invalidLowScore.BaseScore, Is.EqualTo(1));
        
        var invalidHighScore = new AbilityScore(30);
        Assert.That(invalidHighScore.BaseScore, Is.EqualTo(20));
    }
}