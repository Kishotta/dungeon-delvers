using DungeonDelvers.Domain.Choices;
using DungeonDelvers.Domain.Effects;

namespace DungeonDelvers.Domain.Tests;

[TestFixture]
public class ElfRaceTest
{
    private Race _elf = default!;
    
    [SetUp]
    public void Setup()
    {
        _elf = Race.Create("Elf", "Description", CreatureType.Humanoid, 30u, 100, 750);
        
        var abilityScoreIncreaseTrait = Trait.Create("Ability Score Increase", "Your Dexterity score increases by 2.");
        var abilityScoreAdjustmentEffect = new AbilityScoreAdjustment(Ability.Dexterity, 2);
        abilityScoreIncreaseTrait.AddEffect(abilityScoreAdjustmentEffect);
        _elf.AddTrait(abilityScoreIncreaseTrait);
        
        var sizeTrait = Trait.Create("Size", "Elves range from under 5 to over 6 feet tall and have slender builds. Your size is Medium.");
        var sizeEffect = new SizeOverride(Size.Medium);
        sizeTrait.AddEffect(sizeEffect);
        _elf.AddTrait(sizeTrait);
        
        var darkvisionTrait = Trait.Create("Darkvision", "Accustomed to twilit forests and the night sky, you have superior vision in dark and dim conditions. You can see in dim light within 60 feet of you as if it were bright light, and in darkness as if it were dim light. You can't discern color in darkness, only shades of gray.");
        var darkvisionEffect = new Darkvision(60u);
        darkvisionTrait.AddEffect(darkvisionEffect);
        _elf.AddTrait(darkvisionTrait);
        
        var keenSensesTrait = Trait.Create("Keen Senses", "You have proficiency in the Perception skill.");
        // TODO: Implement skill proficiency effect
        _elf.AddTrait(keenSensesTrait);
        
        var feyAncestryTrait = Trait.Create("Fey Ancestry", "You have advantage on saving throws against being charmed, and magic can't put you to sleep.");
        var feyAncestryEffect = new SavingThrowAdvantage(Ability.Charisma, "charmed");
        feyAncestryTrait.AddEffect(feyAncestryEffect);
        _elf.AddTrait(feyAncestryTrait);
        
        var tranceTrait = Trait.Create("Trance", "Elves don't need to sleep. Instead, they meditate deeply, remaining semiconscious, for 4 hours a day. (The Common word for such meditation is \"trance.\") While meditating, you can dream after a fashion; such dreams are actually mental exercises that have become reflexive through years of practice. After resting in this way, you gain the same benefit that a human does from 8 hours of sleep.");
        _elf.AddTrait(tranceTrait);
        
        var languagesTrait = Trait.Create("Languages", "You can speak, read, and write Common and Elvish.");
        var languagesEffect = new LanguageProficiency(Language.Common | Language.Elvish);
        languagesTrait.AddEffect(languagesEffect);
        _elf.AddTrait(languagesTrait);
    }

    [Test]
    public void CharacterMaterialization_WhenElfRace_ShouldCreateCharacterWithElfEffects()
    {
        var character = Character.Create(
            "Legolas",
            _elf,
            Size.Medium,
            new AbilityScore(10),
            new AbilityScore(10),
            new AbilityScore(10),
            new AbilityScore(10),
            new AbilityScore(10),
            new AbilityScore(10));
        
        var materializedCharacter = character.Materialize();
        
        Assert.Multiple(() =>
        {
            Assert.That(materializedCharacter.Dexterity.Score, Is.EqualTo(12));
            Assert.That(materializedCharacter.Size, Is.EqualTo(Size.Medium));
            Assert.That(materializedCharacter.Darkvision.Range, Is.EqualTo(60u));
            Assert.That(materializedCharacter.CharismaSave.Advantages, Contains.Item("charmed"));
            Assert.That(materializedCharacter.LanguageProficiencies.HasFlag(Language.Common | Language.Elvish));
        });
    }

    [Test]
    public void CharacterMaterialization_WhenHighElfRace_ShouldCreateCharacterWithHighElfEffects()
    {
        var highElf = Race.Create(
            _elf,
            "High Elf",
            "Description");
        
        var abilityScoreIncreaseTrait = Trait.Create("Ability Score Increase", "Your Intelligence score increases by 1.");
        var abilityScoreAdjustmentEffect = new AbilityScoreAdjustment(Ability.Intelligence, 1);
        abilityScoreIncreaseTrait.AddEffect(abilityScoreAdjustmentEffect);
        highElf.AddTrait(abilityScoreIncreaseTrait);
        
        var elfWeaponTrainingTrait = Trait.Create("Elf Weapon Training", "You have proficiency with the longsword, shortsword, shortbow, and longbow.");
        var elfWeaponTrainingEffect = new WeaponTypeProficiency(WeaponType.Longsword | WeaponType.Shortsword | WeaponType.Shortbow | WeaponType.Longbow);
        elfWeaponTrainingTrait.AddEffect(elfWeaponTrainingEffect);
        highElf.AddTrait(elfWeaponTrainingTrait);
        
        var cantripTrait = Trait.Create("Cantrip", "You know one cantrip of your choice from the wizard spell list. Intelligence is your spellcasting ability for it.");
        // TODO: Implement spellcasting ability effect
        highElf.AddTrait(cantripTrait);
        
        var extraLanguageTrait = Trait.Create("Extra Language", "You can speak, read, and write one extra language of your choice.");
        var extraLanguageChoice = new LanguageProficiencyChoice(Language.All);
        extraLanguageTrait.AddChoice(extraLanguageChoice);
        highElf.AddTrait(extraLanguageTrait);
        
        var character = Character.Create(
            "Legolas",
            highElf,
            Size.Medium,
            new AbilityScore(10),
            new AbilityScore(10),
            new AbilityScore(10),
            new AbilityScore(10),
            new AbilityScore(10),
            new AbilityScore(10));
        
        // HACK: This lookup might change in the future
        var characterExtraLanguageChoice = character.Choices.OfType<LanguageProficiencyChoice>().Single();
        characterExtraLanguageChoice.MakeChoice(character, Language.Primordial);
        
        var materializedCharacter = character.Materialize();
        
        Assert.Multiple(() =>
        {
            Assert.That(materializedCharacter.Dexterity.Score, Is.EqualTo(12));
            Assert.That(materializedCharacter.Intelligence.Score, Is.EqualTo(11));
            Assert.That(materializedCharacter.Size, Is.EqualTo(Size.Medium));
            Assert.That(materializedCharacter.Darkvision.Range, Is.EqualTo(60u));
            Assert.That(materializedCharacter.CharismaSave.Advantages, Contains.Item("charmed"));
            Assert.That(materializedCharacter.WeaponTypeProficiencies.HasFlag(WeaponType.Longsword | WeaponType.Shortsword | WeaponType.Shortbow | WeaponType.Longbow));
            Assert.That(materializedCharacter.LanguageProficiencies.HasFlag(Language.Common | Language.Elvish | Language.Aquan | Language.Auran));
        });
    }

    [Test]
    public void CharacterMaterialization_WhenWoodElfRace_ShouldCreateCharacterWithWoodElfEffects()
    {
        var woodElf = Race.Create(
            _elf,
            "Wood Elf",
            "Description");
        
        var abilityScoreIncreaseTrait = Trait.Create("Ability Score Increase", "Your Wisdom score increases by 1.");
        var abilityScoreAdjustmentEffect = new AbilityScoreAdjustment(Ability.Wisdom, 1);
        abilityScoreIncreaseTrait.AddEffect(abilityScoreAdjustmentEffect);
        woodElf.AddTrait(abilityScoreIncreaseTrait);
        
        var elfWeaponTrainingTrait = Trait.Create("Elf Weapon Training", "You have proficiency with the longsword, shortsword, shortbow, and longbow.");
        var elfWeaponTrainingEffect = new WeaponTypeProficiency(WeaponType.Longsword | WeaponType.Shortsword | WeaponType.Shortbow | WeaponType.Longbow);
        elfWeaponTrainingTrait.AddEffect(elfWeaponTrainingEffect);
        woodElf.AddTrait(elfWeaponTrainingTrait);
        
        var fleetOfFootTrait = Trait.Create("Fleet of Foot", "Your base walking speed increases to 35 feet.");
        var movementSpeedAdjustmentEffect = new MovementSpeedAdjustment(5);
        fleetOfFootTrait.AddEffect(movementSpeedAdjustmentEffect);
        woodElf.AddTrait(fleetOfFootTrait);
        
        var maskOfTheWildTrait = Trait.Create("Mask of the Wild", "You can attempt to hide even when you are only lightly obscured by foliage, heavy rain, falling snow, mist, and other natural phenomena.");
        woodElf.AddTrait(maskOfTheWildTrait);
        
        var character = Character.Create(
            "Legolas",
            woodElf,
            Size.Medium,
            new AbilityScore(10),
            new AbilityScore(10),
            new AbilityScore(10),
            new AbilityScore(10),
            new AbilityScore(10),
            new AbilityScore(10));
        
        var materializedCharacter = character.Materialize();
        
        Assert.Multiple(() =>
        {
            Assert.That(materializedCharacter.Dexterity.Score, Is.EqualTo(12));
            Assert.That(materializedCharacter.Wisdom.Score, Is.EqualTo(11));
            Assert.That(materializedCharacter.Size, Is.EqualTo(Size.Medium));
            Assert.That(materializedCharacter.MovementSpeed, Is.EqualTo(35u));
            Assert.That(materializedCharacter.Darkvision.Range, Is.EqualTo(60u));
            Assert.That(materializedCharacter.CharismaSave.Advantages, Contains.Item("charmed"));
            Assert.That(materializedCharacter.WeaponTypeProficiencies.HasFlag(WeaponType.Longsword | WeaponType.Shortsword | WeaponType.Shortbow | WeaponType.Longbow));
            Assert.That(materializedCharacter.LanguageProficiencies.HasFlag(Language.Common | Language.Elvish));
        });
    }
}