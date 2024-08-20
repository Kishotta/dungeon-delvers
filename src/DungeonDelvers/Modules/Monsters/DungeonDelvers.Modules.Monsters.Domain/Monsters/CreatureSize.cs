namespace DungeonDelvers.Modules.Monsters.Domain.Monsters;

public class CreatureSize
{
    public string Name { get; private init; }

    public static readonly CreatureSize Tiny = new("Tiny");
    public static readonly CreatureSize Small = new("Small");
    public static readonly CreatureSize Medium = new("Medium");
    public static readonly CreatureSize Large = new("Large");
    public static readonly CreatureSize Huge = new("Huge");
    public static readonly CreatureSize Gargantuan = new("Gargantuan");

    private CreatureSize(string name)
    {
        Name = name;
    }
}

public class CreatureType
{
    public string Name { get; private init; }
    
    public static readonly CreatureType Aberration = new("Aberration");
    public static readonly CreatureType Beast = new("Beast");
    public static readonly CreatureType Celestial = new("Celestial");
    public static readonly CreatureType Construct = new("Construct");
    public static readonly CreatureType Dragon = new("Dragon");
    public static readonly CreatureType Elemental = new("Elemental");
    public static readonly CreatureType Fey = new("Fey");
    public static readonly CreatureType Fiend = new("Fiend");
    public static readonly CreatureType Giant = new("Giant");
    public static readonly CreatureType Humanoid = new("Humanoid");
    public static readonly CreatureType Monstrosity = new("Monstrosity");
    public static readonly CreatureType Ooze = new("Ooze");
    public static readonly CreatureType Plant = new("Plant");
    public static readonly CreatureType Undead = new("Undead");
    
    private CreatureType(string name)
    {
        Name = name;
    }
}

public class Alignment
{
    public string Ethical { get; private init; } = string.Empty;
    public string Moral { get; private init; } = string.Empty;
    public string Name { get; private init; } = string.Empty;

    public static readonly Alignment Unaligned = Create(EthicalAlignment.None, MoralAlignment.None);
    
    public static readonly Alignment LawfulGood = Create(EthicalAlignment.Lawful, MoralAlignment.Good);
    public static readonly Alignment LawfulNeutral = Create(EthicalAlignment.Lawful, MoralAlignment.Neutral);
    public static readonly Alignment LawfulEvil = Create(EthicalAlignment.Lawful, MoralAlignment.Evil);
    public static readonly Alignment NeutralGood = Create(EthicalAlignment.Neutral, MoralAlignment.Good);
    public static readonly Alignment TrueNeutral = Create(EthicalAlignment.Neutral, MoralAlignment.Neutral);
    public static readonly Alignment NeutralEvil = Create(EthicalAlignment.Neutral, MoralAlignment.Evil);
    public static readonly Alignment ChaoticGood = Create(EthicalAlignment.Chaotic, MoralAlignment.Good);
    public static readonly Alignment ChaoticNeutral = Create(EthicalAlignment.Chaotic, MoralAlignment.Neutral);
    public static readonly Alignment ChaoticEvil = Create(EthicalAlignment.Chaotic, MoralAlignment.Evil);
    
    public static readonly Alignment Any = Create(EthicalAlignment.Any, MoralAlignment.Any);
    
    private Alignment() { }
    
    public static Alignment Create(
        EthicalAlignment ethicalAlignment, 
        MoralAlignment moralAlignment)
    {
        var ethical = Enum.GetName(ethicalAlignment.GetType(), ethicalAlignment)!;
        var moral = Enum.GetName(moralAlignment.GetType(), moralAlignment)!;
        var name = GetName(ethicalAlignment, moralAlignment);

        return new Alignment
        {
            Ethical = ethical,
            Moral = moral,
            Name = name,
        };
    }

    private static string GetName(EthicalAlignment ethical, MoralAlignment moral) =>
        (ethical, moral) switch
        {
            (EthicalAlignment.None, MoralAlignment.None) => "Unaligned",
            (EthicalAlignment.Any, MoralAlignment.Any) => "Any",
            (_, MoralAlignment.Any) => $"Any {ethical}",
            (EthicalAlignment.Neutral, MoralAlignment.Neutral) => "Neutral",
            (_, _) => $"{ethical} {moral}"
        };
}

[Flags]
public enum EthicalAlignment
{
    None = 0,
    Lawful = 1,
    Neutral = 1 << 1,
    Chaotic = 1 << 2,
    Any = Lawful | Neutral | Chaotic
}

[Flags]
public enum MoralAlignment
{
    None = 0,
    Good = 1,
    Neutral = 1 << 1,
    Evil = 1 << 2,
    Any = Good | Neutral | Evil
}