using DungeonDelvers.Domain.Choices;
using DungeonDelvers.Domain.Effects;

namespace DungeonDelvers.Domain.Tests;

[TestFixture]
public class DwarfRaceTest
{
    private Race _dwarf = default!;
    
    [SetUp]
    public void Setup()
    {
        _dwarf = Race.Create("Dwarf", "Description", CreatureType.Humanoid, 25u, 50, 350);
        
        var abilityScoreIncreaseTrait = Trait.Create("Ability Score Increase", "Your Constitution score increases by 2.");
        var abilityScoreAdjustmentEffect = new AbilityScoreAdjustment(Ability.Constitution, 2);
        abilityScoreIncreaseTrait.AddEffect(abilityScoreAdjustmentEffect);
        _dwarf.AddTrait(abilityScoreIncreaseTrait);
        
        var sizeTrait = Trait.Create("Size", " Dwarves stand between 4 and 5 feet tall and average about 150 pounds. Your size is Medium.");
        var sizeEffect = new SizeOverride(Size.Medium);
        sizeTrait.AddEffect(sizeEffect);
        _dwarf.AddTrait(sizeTrait);
        
        var darkvisionTrait = Trait.Create("Darkvision", "Accustomed to life underground, you have superior vision in dark and dim conditions. You can see in dim light within 60 feet of you as if it were bright light, and in darkness as if it were dim light. You can't discern color in darkness, only shades of gray.");
        var darkvisionEffect = new Darkvision(60u);
        darkvisionTrait.AddEffect(darkvisionEffect);
        _dwarf.AddTrait(darkvisionTrait);
        
        var dwarvenResilienceTrait = Trait.Create("Dwarven Resilience", "You have advantage on saving throws against poison, and you have resistance against poison damage.");
        var savingThrowAdvantageEffect = new SavingThrowAdvantage(Ability.Constitution, "poison");
        dwarvenResilienceTrait.AddEffect(savingThrowAdvantageEffect);
        // TODO: Implement resistance against poison damage
        _dwarf.AddTrait(dwarvenResilienceTrait);
        
        var dwarvenCombatTrainingTrait = Trait.Create("Dwarven Combat Training", "You have proficiency with the battleaxe, handaxe, light hammer, and warhammer.");
        var weaponProficiencyEffect = new WeaponTypeProficiency(WeaponType.Battleaxe | WeaponType.Handaxe | WeaponType.LightHammer | WeaponType.Warhammer);
        dwarvenCombatTrainingTrait.AddEffect(weaponProficiencyEffect);
        _dwarf.AddTrait(dwarvenCombatTrainingTrait);
        
        var toolProficiencyTrait = Trait.Create("Tool Proficiency", "You gain proficiency with the artisan's tools of your choice: smith's tools, brewer's supplies, or mason's tools.");
        var toolProficiencyChoice = new ToolTypeProficiencyChoice(ToolType.SmithsTools | ToolType.BrewersSupplies | ToolType.MasonsTools);
        toolProficiencyTrait.AddChoice(toolProficiencyChoice);
        _dwarf.AddTrait(toolProficiencyTrait);
        
        var languagesTrait = Trait.Create("Languages", "You can speak, read, and write Common and Dwarvish.");
        var languageEffect = new LanguageProficiency(Language.Common | Language.Dwarvish);
        languagesTrait.AddEffect(languageEffect);
        _dwarf.AddTrait(languagesTrait);
    }
    
    [Test]
    public void CharacterMaterialization_WhenDwarfRace_ShouldCreateCharacterWithDwarfEffects()
    {
        var character = Character.Create(
            "Bruenor",
            _dwarf,
            Size.Medium,
            new AbilityScore(10),
            new AbilityScore(10),
            new AbilityScore(10),
            new AbilityScore(10),
            new AbilityScore(10),
            new AbilityScore(10));
        
        // HACK: This lookup might change in the future
        var characterToolProficiencyChoice = character.Choices.OfType<ToolTypeProficiencyChoice>().Single();
        characterToolProficiencyChoice.MakeChoice(character, ToolType.SmithsTools);

        var materializedCharacter = character.Materialize();
        
        Assert.Multiple(() =>
        {
            Assert.That(materializedCharacter.Constitution.Score, Is.EqualTo(12));
            Assert.That(materializedCharacter.Size, Is.EqualTo(Size.Medium));
            Assert.That(materializedCharacter.MovementSpeed, Is.EqualTo(25u));
            Assert.That(materializedCharacter.Darkvision.Range, Is.EqualTo(60u));
            Assert.That(materializedCharacter.ConstitutionSave.Advantages, Contains.Item("poison"));
            Assert.That(materializedCharacter.ToolProficiencies.HasFlag(ToolType.SmithsTools));
            Assert.That(materializedCharacter.LanguageProficiencies.HasFlag(Language.Common | Language.Dwarvish));
        });
    }

    [Test]
    public void CharacterMaterialization_WhenHillDwarfRace_ShouldCreateCharacterWithHillDwarfEffects()
    {
        var hillDwarf = Race.Create(
            _dwarf, 
            "Hill Dwarf", 
            "Description");
        
        var abilityScoreIncreaseTrait2 = Trait.Create("Ability Score Increase", "Your Wisdom score increases by 1.");
        var abilityScoreAdjustmentEffect2 = new AbilityScoreAdjustment(Ability.Wisdom, 1);
        abilityScoreIncreaseTrait2.AddEffect(abilityScoreAdjustmentEffect2);
        hillDwarf.AddTrait(abilityScoreIncreaseTrait2);
        
        var dwarvenToughnessTrait = Trait.Create("Dwarven Toughness", "Your hit point maximum increases by 1, and it increases by 1 every time you gain a level.");
        // TODO: Implement Dwarven Toughness trait effects
        hillDwarf.AddTrait(dwarvenToughnessTrait);
        
        var character = Character.Create(
            "Bruenor",
            hillDwarf,
            Size.Medium,
            new AbilityScore(10),
            new AbilityScore(10),
            new AbilityScore(10),
            new AbilityScore(10),
            new AbilityScore(10),
            new AbilityScore(10));
        
        // HACK: This lookup might change in the future
        var characterToolProficiencyChoice = character.Choices.OfType<ToolTypeProficiencyChoice>().Single();
        characterToolProficiencyChoice.MakeChoice(character, ToolType.SmithsTools);

        var materializedCharacter = character.Materialize();
        
        Assert.Multiple(() =>
        {
            Assert.That(materializedCharacter.Constitution.Score, Is.EqualTo(12));
            Assert.That(materializedCharacter.Wisdom.Score, Is.EqualTo(11));
            Assert.That(materializedCharacter.Size, Is.EqualTo(Size.Medium));
            Assert.That(materializedCharacter.MovementSpeed, Is.EqualTo(25u));
            Assert.That(materializedCharacter.Darkvision.Range, Is.EqualTo(60u));
            Assert.That(materializedCharacter.ConstitutionSave.Advantages, Contains.Item("poison"));
            Assert.That(materializedCharacter.ToolProficiencies.HasFlag(ToolType.SmithsTools));
            Assert.That(materializedCharacter.WeaponTypeProficiencies.HasFlag(WeaponType.Battleaxe | WeaponType.Handaxe | WeaponType.LightHammer | WeaponType.Warhammer));
            Assert.That(materializedCharacter.LanguageProficiencies.HasFlag(Language.Common | Language.Dwarvish));
        });
    }
    
    [Test]
    public void CharacterMaterialization_WhenMountainDwarfRace_ShouldCreateCharacterWithMountainDwarfEffects()
    {
        var mountainDwarf = Race.Create(
            _dwarf,
            "Mountain Dwarf",
            "Description");
        
        var abilityScoreIncreaseTrait2 = Trait.Create("Ability Score Increase", "Your Strength score increases by 2.");
        var abilityScoreAdjustmentEffect2 = new AbilityScoreAdjustment(Ability.Strength, 2);
        abilityScoreIncreaseTrait2.AddEffect(abilityScoreAdjustmentEffect2);
        mountainDwarf.AddTrait(abilityScoreIncreaseTrait2);
        
        var dwarvenArmorTrainingTrait = Trait.Create("Dwarven Armor Training", "You have proficiency with light and medium armor.");
        var armorProficiencyEffect = new ArmorTypeProficiency(ArmorType.Light | ArmorType.Medium);
        dwarvenArmorTrainingTrait.AddEffect(armorProficiencyEffect);
        mountainDwarf.AddTrait(dwarvenArmorTrainingTrait);
        
        var character = Character.Create(
            "Bruenor",
            mountainDwarf,
            Size.Medium,
            new AbilityScore(10),
            new AbilityScore(10),
            new AbilityScore(10),
            new AbilityScore(10),
            new AbilityScore(10),
            new AbilityScore(10));
        
        // HACK: This lookup might change in the future
        var characterToolProficiencyChoice = character.Choices.OfType<ToolTypeProficiencyChoice>().Single();
        characterToolProficiencyChoice.MakeChoice(character, ToolType.SmithsTools);

        var materializedCharacter = character.Materialize();
        
        Assert.Multiple(() =>
        {
            Assert.That(materializedCharacter.Constitution.Score, Is.EqualTo(12));
            Assert.That(materializedCharacter.Strength.Score, Is.EqualTo(12));
            Assert.That(materializedCharacter.Size, Is.EqualTo(Size.Medium));
            Assert.That(materializedCharacter.MovementSpeed, Is.EqualTo(25u));
            Assert.That(materializedCharacter.Darkvision.Range, Is.EqualTo(60u));
            Assert.That(materializedCharacter.ConstitutionSave.Advantages, Contains.Item("poison"));
            Assert.That(materializedCharacter.ToolProficiencies.HasFlag(ToolType.SmithsTools));
            Assert.That(materializedCharacter.WeaponTypeProficiencies.HasFlag(WeaponType.Battleaxe | WeaponType.Handaxe | WeaponType.LightHammer | WeaponType.Warhammer));
            Assert.That(materializedCharacter.ArmorTypeProficiencies.HasFlag(ArmorType.Padded | ArmorType.Leather | ArmorType.StuddedLeather | ArmorType.Hide | ArmorType.ChainShirt | ArmorType.ScaleMail | ArmorType.Breastplate | ArmorType.HalfPlate));
            Assert.That(materializedCharacter.LanguageProficiencies.HasFlag(Language.Common | Language.Dwarvish));
        });
    }
}