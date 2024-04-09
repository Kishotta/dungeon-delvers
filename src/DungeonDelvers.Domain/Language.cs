namespace DungeonDelvers.Domain;

[Flags]
public enum Language : long
{
    None = 0L,
    
    Common = 1L << 0,
    Dwarvish = 1L << 1,
    Elvish = 1L << 2,
    Giant = 1L << 3,
    Gnomish = 1L << 4,
    Goblin = 1L << 5,
    Halfling = 1L << 6,
    Orc = 1L << 7,
    
    Abyssal = 1L << 8,
    Celestial = 1L << 9,
    Draconic = 1L << 10,
    DeepSpeech = 1L << 11,
    Infernal = 1L << 12,
    
    Aquan = 1L << 13,
    Auran = 1L << 14,
    Ignan = 1L << 15,
    Terran = 1L << 16,
    
    Primordial = Aquan | Auran | Ignan | Terran,
    
    Sylvan = 1L << 17,
    Undercommon = 1L << 18,
    
    Druidic = 1L << 19,
    Gith = 1L << 20,
    ThievesCant = 1L << 21,
    
    All = Common | Dwarvish | Elvish | Giant | Gnomish | Goblin | Halfling | Orc | Abyssal | Celestial | Draconic | DeepSpeech | Infernal | Aquan | Auran | Ignan | Terran | Sylvan | Undercommon | Druidic | Gith | ThievesCant
}