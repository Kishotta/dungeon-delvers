namespace DungeonDelvers.Domain;

[Flags]
public enum ToolType : long
{
    None = 0L,
    
    AlchemistsSupplies = 1L << 0,
    BrewersSupplies = 1L << 1,
    CalligraphersSupplies = 1L << 2,
    CarpentersTools = 1L << 3,
    CartographersTools = 1L << 4,
    CobblersTools = 1L << 5,
    CooksUtensils = 1L << 6,
    GlassblowersTools = 1L << 7,
    JewelersTools = 1L << 8,
    LeatherworkersTools = 1L << 9,
    MasonsTools = 1L << 10,
    PaintersSupplies = 1L << 11,
    PottersTools = 1L << 12,
    SmithsTools = 1L << 13,
    TinkersTools = 1L << 14,
    WeaversTools = 1L << 15,
    WoodcarversTools = 1L << 16,
    
    DiceSet = 1L << 17,
    DragonchessSet = 1L << 18,
    PlayingCardSet = 1L << 19,
    ThreeDragonAnteSet = 1L << 20,
    
    Bagpipes = 1L << 21,
    Drum = 1L << 22,
    Dulcimer = 1L << 23,
    Flute = 1L << 24,
    Lute = 1L << 25,
    Lyre = 1L << 26,
    Horn = 1L << 27,
    PanFlute = 1L << 28,
    Shawm = 1L << 29,
    Viol = 1L << 30,
    
    DisguiseKit = 1L << 31,
    ForgeryKit = 1L << 32,
    HerbalismKit = 1L << 33,
    NavigatorTools = 1L << 34,
    PoisonersKit = 1L << 35,
    ThievesTools = 1L << 36,
    VehiclesLand = 1L << 37,
    VehiclesWater = 1L << 38,
    VehiclesAir = 1L << 39,
}