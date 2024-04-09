namespace DungeonDelvers.Domain;

[Flags]
public enum WeaponType : long
{
    None = 0L,
    
    Club = 1L << 0,
    Dagger = 1L << 1,
    Greatclub = 1L << 2,
    Handaxe = 1L << 3,
    Javelin = 1L << 4,
    LightHammer = 1L << 5,
    Mace = 1L << 6,
    Quarterstaff = 1L << 7,
    Sickle = 1L << 8,
    Spear = 1L << 9,
    
    SimpleMelee = Club | Dagger | Greatclub | Handaxe | Javelin | LightHammer | Mace | Quarterstaff | Sickle | Spear,
    
    CrossbowLight = 1L << 10,
    Dart = 1L << 11,
    Shortbow = 1L << 12,
    Sling = 1L << 13,
    
    SimpleRanged = CrossbowLight | Dart | Shortbow | Sling,
    
    Battleaxe = 1L << 14,
    Flail = 1L << 15,
    Glaive = 1L << 16,
    Greataxe = 1L << 17,
    Greatsword = 1L << 18,
    Halberd = 1L << 19,
    Lance = 1L << 20,
    Longsword = 1L << 21,
    Maul = 1L << 22,
    Morningstar = 1L << 23,
    Pike = 1L << 24,
    Rapier = 1L << 25,
    Scimitar = 1L << 26,
    Shortsword = 1L << 27,
    Trident = 1L << 28,
    WarPick = 1L << 29,
    Warhammer = 1L << 30,
    Whip = 1L << 31,
    
    MartialMelee = Battleaxe | Flail | Glaive | Greataxe | Greatsword | Halberd | Lance | Longsword | Maul | Morningstar | Pike | Rapier | Scimitar | Shortsword | Trident | WarPick | Warhammer | Whip,
    
    Blowgun = 1L << 32,
    CrossbowHand = 1L << 33,
    CrossbowHeavy = 1L << 34,
    Longbow = 1L << 35,
    Net = 1L << 36,
    
    MartialRanged = Blowgun | CrossbowHand | CrossbowHeavy | Longbow | Net,
    
    Melee = SimpleMelee | MartialMelee,
    
    Ranged = SimpleRanged | MartialRanged,
    
    All = SimpleMelee | SimpleRanged | MartialMelee | MartialRanged,
}
