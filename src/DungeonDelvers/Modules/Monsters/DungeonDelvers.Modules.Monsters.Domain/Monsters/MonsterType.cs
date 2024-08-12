namespace DungeonDelvers.Modules.Monsters.Domain.Monsters;

[Flags]
public enum MonsterType
{
    None = 0,
    Aberration = 1,
    Beast = 1 << 1,
    Celestial = 1 << 2,
    Construct = 1 << 3,
    Dragon = 1 << 4,
    Elemental = 1 << 5,
    Fey = 1 << 6,
    Fiend = 1 << 7,
    Giant = 1 << 8,
    Humanoid = 1 << 9,
    Monstrosity = 1 << 10,
    Ooze = 1 << 11,
    Plant = 1 << 12,
    Undead = 1 << 13
}