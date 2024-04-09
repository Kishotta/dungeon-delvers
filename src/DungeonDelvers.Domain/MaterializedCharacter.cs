namespace DungeonDelvers.Domain;

public class MaterializedCharacter
{
    public string Name { get; init; } = string.Empty;
    public int MovementSpeed { get; set; } = 30;
    public Size Size { get; set; } = Size.Medium;
    public CreatureType CreatureType { get; set; } = CreatureType.Humanoid;
    
    public AbilityScore Strength { get; set; }
    public AbilityScore Dexterity { get; set; }
    public AbilityScore Constitution { get; set; }
    public AbilityScore Intelligence { get; set; }
    public AbilityScore Wisdom { get; set; }
    public AbilityScore Charisma { get; set; }
    
    public SavingThrow StrengthSave { get; set; }
    public int StrengthSaveModifier => StrengthSave.Modifier(Ability.Strength, this);
    public SavingThrow DexteritySave { get; set; }
    public int DexteritySaveModifier => DexteritySave.Modifier(Ability.Dexterity, this);
    public SavingThrow ConstitutionSave { get; set; }
    public int ConstitutionSaveModifier => ConstitutionSave.Modifier(Ability.Constitution, this);
    public SavingThrow IntelligenceSave { get; set; }
    public int IntelligenceSaveModifier => IntelligenceSave.Modifier(Ability.Intelligence, this);
    public SavingThrow WisdomSave { get; set; }
    public int WisdomSaveModifier => WisdomSave.Modifier(Ability.Wisdom, this);
    public SavingThrow CharismaSave { get; set; }
    public int CharismaSaveModifier => CharismaSave.Modifier(Ability.Charisma, this);
    
    public Sense Darkvision { get; set; }
    
    public MaterializedCharacter(Character character)
    {
        Name = character.Name;
        MovementSpeed = (int)character.BaseMovementSpeed;
        Size = character.Size;
        CreatureType = character.CreatureType;
        
        Strength = character.Strength;
        Dexterity = character.Dexterity;
        Constitution = character.Constitution;
        Intelligence = character.Intelligence;
        Wisdom = character.Wisdom;
        Charisma = character.Charisma;
        
        Darkvision = new Sense(0u);
        
        StrengthSave = new SavingThrow();
        DexteritySave = new SavingThrow();
        ConstitutionSave = new SavingThrow();
        IntelligenceSave = new SavingThrow();
        WisdomSave = new SavingThrow();
        CharismaSave = new SavingThrow();
    }
}