namespace DungeonDelvers.Domain;

public struct AbilityScore
{
    public int BaseScore { get; private set; }
    public int Score { get; private set; }
    
    public int Modifier => (Score - 10) / 2;
    
    public AbilityScore(int baseScore)
    {
        BaseScore = Math.Clamp(baseScore, 1, 20);
        Score = BaseScore;
    }
    
    public static AbilityScore operator +(AbilityScore abilityScore, int adjustment)
    {
        abilityScore.Score = Math.Clamp(abilityScore.Score + adjustment, 1, 20);
        return abilityScore;
    }
    
    public static AbilityScore operator -(AbilityScore abilityScore, int adjustment)
    {
        abilityScore.Score = Math.Clamp(abilityScore.Score - adjustment, 1, 20);
        return abilityScore;
    }
}