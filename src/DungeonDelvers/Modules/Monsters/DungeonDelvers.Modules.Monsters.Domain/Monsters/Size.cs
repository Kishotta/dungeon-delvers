namespace DungeonDelvers.Modules.Monsters.Domain.Monsters;

[Flags]
public enum Size
{
    None = 0,
    Tiny = 1,
    Small = 1 << 1,
    Medium = 1 << 2,
    Large = 1 << 3,
    Huge = 1 << 4,
    Gargantuan = 1 << 5
}