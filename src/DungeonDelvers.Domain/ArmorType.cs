namespace DungeonDelvers.Domain;

[Flags]
public enum ArmorType : long
{
    None = 0L,
    
    Padded = 1L << 0,
    Leather = 1L << 1,
    StuddedLeather = 1L << 2,
    
    Light = Padded | Leather | StuddedLeather,
    
    Hide = 1L << 3,
    ChainShirt = 1L << 4,
    ScaleMail = 1L << 5,
    Breastplate = 1L << 6,
    HalfPlate = 1L << 7,
    
    Medium = Hide | ChainShirt | ScaleMail | Breastplate | HalfPlate,
    
    RingMail = 1L << 8,
    ChainMail = 1L << 9,
    Splint = 1L << 10,
    Plate = 1L << 11,
    
    Heavy = RingMail | ChainMail | Splint | Plate,
    
    Shields = 1L << 12,
}