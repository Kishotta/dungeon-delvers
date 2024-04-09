namespace DungeonDelvers.Domain;

public class MaterializedCharacter
{
    public string Name { get; init; }
    public int MovementSpeed { get; set; }
    public Size Size { get; set; }
    public CreatureType CreatureType { get; set; }
    
    public AbilityScore Strength { get; set; }
    public AbilityScore Dexterity { get; set; }
    public AbilityScore Constitution { get; set; }
    public AbilityScore Intelligence { get; set; }
    public AbilityScore Wisdom { get; set; }
    public AbilityScore Charisma { get; set; }
    
    public SavingThrow StrengthSave { get; set; } = new();
    public int StrengthSaveModifier => StrengthSave.Modifier(Ability.Strength, this);
    public SavingThrow DexteritySave { get; set; } = new();
    public int DexteritySaveModifier => DexteritySave.Modifier(Ability.Dexterity, this);
    public SavingThrow ConstitutionSave { get; set; } = new();
    public int ConstitutionSaveModifier => ConstitutionSave.Modifier(Ability.Constitution, this);
    public SavingThrow IntelligenceSave { get; set; } = new();
    public int IntelligenceSaveModifier => IntelligenceSave.Modifier(Ability.Intelligence, this);
    public SavingThrow WisdomSave { get; set; } = new();
    public int WisdomSaveModifier => WisdomSave.Modifier(Ability.Wisdom, this);
    public SavingThrow CharismaSave { get; set; } = new();
    public int CharismaSaveModifier => CharismaSave.Modifier(Ability.Charisma, this);
    
    public Sense Darkvision { get; set; } = new(0u);
    
    public ArmorType ArmorTypeProficiencies { get; set; } = ArmorType.None;
    public WeaponType WeaponTypeProficiencies { get; set; } = WeaponType.None;
    public ToolType ToolProficiencies { get; set; } = ToolType.None;
    public Language LanguageProficiencies { get; set; } = Language.None;
    
    public Skill SkillProficiencies { get; set; } = Skill.None;

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
    }
}