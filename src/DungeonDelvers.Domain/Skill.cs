namespace DungeonDelvers.Domain;

[Flags]
public enum Skill
{
    None = 0,
    
    Athletics = 1 << 0,
    
    Strength = Athletics,
    
    Acrobatics = 1 << 1,
    SleightOfHand = 1 << 2,
    Stealth = 1 << 3,
    
    Dexterity = Acrobatics | SleightOfHand | Stealth,
    
    Arcana = 1 << 4,
    History = 1 << 5,
    Investigation = 1 << 6,
    Nature = 1 << 7,
    Religion = 1 << 8,
    
    Intelligence = Arcana | History | Investigation | Nature | Religion,
    
    AnimalHandling = 1 << 9,
    Insight = 1 << 10,
    Medicine = 1 << 11,
    Perception = 1 << 12,
    Survival = 1 << 13,
    
    Wisdom = AnimalHandling | Insight | Medicine | Perception | Survival,
    
    Deception = 1 << 14,
    Intimidation = 1 << 15,
    Performance = 1 << 16,
    Persuasion = 1 << 17,
    
    Charisma = Deception | Intimidation | Performance | Persuasion,
    
    All = Strength | Dexterity | Intelligence | Wisdom | Charisma
}