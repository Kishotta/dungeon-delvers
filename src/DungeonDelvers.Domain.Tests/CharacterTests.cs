using DungeonDelvers.Domain.Effects;

namespace DungeonDelvers.Domain.Tests;

[TestFixture]
public class CharacterTests
{
    private static Race CreateDwarfRace()
    {
        return Race.Create(
            "Dwarf", 
            "Description", 
            CreatureType.Humanoid,
            25u, 
            50,
            350);
    }
    
    private static Character CreateBruenor()
    {
        var characterName = "Bruenor";
        var race = CreateDwarfRace();
        var size = Size.Medium;
        
        var strength = new AbilityScore(10);
        var dexterity = new AbilityScore(10);
        var constitution = new AbilityScore(10);
        var intelligence = new AbilityScore(10);
        var wisdom = new AbilityScore(10);
        var charisma = new AbilityScore(10);

        return Character.Create(
            characterName,
            race, 
            size, 
            strength,
            dexterity,
            constitution,
            intelligence,
            wisdom,
            charisma);
    }
    
    private static Character CreateCharacterWithRace(Race race)
    {
        var characterName = "Test Character";
        var size = Size.Medium;
        
        var strength = new AbilityScore(10);
        var dexterity = new AbilityScore(10);
        var constitution = new AbilityScore(10);
        var intelligence = new AbilityScore(10);
        var wisdom = new AbilityScore(10);
        var charisma = new AbilityScore(10);

        return Character.Create(
            characterName,
            race, 
            size, 
            strength,
            dexterity,
            constitution,
            intelligence,
            wisdom,
            charisma);
    }
    
    [Test]
    public void Create_ValidProperties_PropertiesSet()
    {
        var characterName = "Test Character";
        var race = CreateDwarfRace();
        var size = Size.Medium;

        var strength = new AbilityScore(10);
        var dexterity = new AbilityScore(10);
        var constitution = new AbilityScore(10);
        var intelligence = new AbilityScore(10);
        var wisdom = new AbilityScore(10);
        var charisma = new AbilityScore(10);

        var character = Character.Create(
            characterName,
            race, 
            size, 
            strength,
            dexterity,
            constitution,
            intelligence,
            wisdom,
            charisma);
        
        Assert.Multiple(() =>
        {
            Assert.That(character.Name, Is.EqualTo(characterName));
            Assert.That(character.BaseMovementSpeed, Is.EqualTo(race.BaseMovementSpeed));
            Assert.That(character.Size, Is.EqualTo(size));
            Assert.That(character.CreatureType, Is.EqualTo(race.CreatureType));
        });
    }

    [Test]
    public void Materialize_NoEffects_ReturnsMaterializedCharacterWithDefaults()
    {
        var character = CreateBruenor();
        
        var materializedCharacter = character.Materialize();
        
        Assert.Multiple(() =>
        {
            Assert.That(materializedCharacter.Name, Is.EqualTo(character.Name));
            Assert.That(materializedCharacter.MovementSpeed, Is.EqualTo(character.BaseMovementSpeed));
            Assert.That(materializedCharacter.Size, Is.EqualTo(character.Size));
            Assert.That(materializedCharacter.CreatureType, Is.EqualTo(character.CreatureType));
        });
    }
    
    [Test]
    public void Materialize_RaceTraitSingleEffect_ReturnsMaterializedCharacterWithEffectApplied()
    {
        var trait = Trait.Create("Test Trait", "Test Description");
        trait.AddEffect(new MovementSpeedAdjustment(-10));
        var race = Race.Create("Test Race", "Test Description");
        race.AddTrait(trait);
        var character = CreateCharacterWithRace(race);
        
        var materializedCharacter = character.Materialize();
        
        Assert.Multiple(() =>
        {
            Assert.That(materializedCharacter.Name, Is.EqualTo(character.Name));
            Assert.That(materializedCharacter.MovementSpeed, Is.EqualTo(20));
            Assert.That(materializedCharacter.Size, Is.EqualTo(character.Size));
            Assert.That(materializedCharacter.CreatureType, Is.EqualTo(character.CreatureType));
        });
    }
    
    [Test]
    public void Materialize_SingleEffect_ReturnsMaterializedCharacterWithEffectApplied()
    {
        var character = CreateBruenor();
        character.AddEffect(new MovementSpeedAdjustment(-10));
        
        var materializedCharacter = character.Materialize();
        
        Assert.Multiple(() =>
        {
            Assert.That(materializedCharacter.Name, Is.EqualTo(character.Name));
            Assert.That(materializedCharacter.MovementSpeed, Is.EqualTo(15));
            Assert.That(materializedCharacter.Size, Is.EqualTo(character.Size));
            Assert.That(materializedCharacter.CreatureType, Is.EqualTo(character.CreatureType));
        });
    }
    
    [Test]
    public void Materialize_RaceTraitMultipleEffect_SameType_ReturnsMaterializedCharacterWithEffectsApplied()
    {
        var trait = Trait.Create("Test Trait", "Test Description");
        trait.AddEffect(new MovementSpeedAdjustment(5));
        trait.AddEffect(new MovementSpeedAdjustment(5));
        var race = Race.Create("Test Race", "Test Description");
        race.AddTrait(trait);
        var character = CreateCharacterWithRace(race);
        
        var materializedCharacter = character.Materialize();
        
        Assert.Multiple(() =>
        {
            Assert.That(materializedCharacter.Name, Is.EqualTo(character.Name));
            Assert.That(materializedCharacter.MovementSpeed, Is.EqualTo(40));
            Assert.That(materializedCharacter.Size, Is.EqualTo(character.Size));
            Assert.That(materializedCharacter.CreatureType, Is.EqualTo(character.CreatureType));
        });
    }
    
    [Test]
    public void Materialize_RaceMultipleTraitSingleEffect_SameType_ReturnsMaterializedCharacterWithEffectsApplied()
    {
        var firstTrait = Trait.Create("First Test Trait", "Test Description");
        firstTrait.AddEffect(new MovementSpeedAdjustment(5));
        
        var secondTrait = Trait.Create("Second Test Trait", "Test Description");
        secondTrait.AddEffect(new MovementSpeedAdjustment(5));

        var race = Race.Create("Test Race", "Test Description");
        race.AddTrait(firstTrait);
        race.AddTrait(secondTrait);
        
        var character = CreateCharacterWithRace(race);
        
        var materializedCharacter = character.Materialize();
        
        Assert.Multiple(() =>
        {
            Assert.That(materializedCharacter.Name, Is.EqualTo(character.Name));
            Assert.That(materializedCharacter.MovementSpeed, Is.EqualTo(40));
            Assert.That(materializedCharacter.Size, Is.EqualTo(character.Size));
            Assert.That(materializedCharacter.CreatureType, Is.EqualTo(character.CreatureType));
        });
    }
    
    [Test]
    public void Materialize_MultipleEffect_SameType_ReturnsMaterializedCharacterWithEffectsApplied()
    {
        var race = Race.Create("Test Race", "Test Description");
        var character = CreateCharacterWithRace(race);
        
        character.AddEffect(new MovementSpeedAdjustment(5));
        character.AddEffect(new MovementSpeedAdjustment(5));
        
        var materializedCharacter = character.Materialize();
        
        Assert.Multiple(() =>
        {
            Assert.That(materializedCharacter.Name, Is.EqualTo(character.Name));
            Assert.That(materializedCharacter.MovementSpeed, Is.EqualTo(40));
            Assert.That(materializedCharacter.Size, Is.EqualTo(character.Size));
            Assert.That(materializedCharacter.CreatureType, Is.EqualTo(character.CreatureType));
        });
    }
    
    [Test]
    public void Materialize_RaceMultipleTraitSingleEffect_DifferentTypes_ReturnsMaterializedCharacterWithEffectsApplied()
    { 
        var firstTrait = Trait.Create("First Test Trait", "Test Description");
        firstTrait.AddEffect(new MovementSpeedAdjustment(5));
        
        var secondTrait = Trait.Create("Second Test Trait", "Test Description");
        secondTrait.AddEffect(new SizeOverride(Size.Gargantuan));
        
        var race = Race.Create("Test Race", "Test Description");
        race.AddTrait(firstTrait);
        race.AddTrait(secondTrait);
        
        var character = CreateCharacterWithRace(race);
        
        var materializedCharacter = character.Materialize();
        
        Assert.Multiple(() =>
        {
            Assert.That(materializedCharacter.Name, Is.EqualTo(character.Name));
            Assert.That(materializedCharacter.MovementSpeed, Is.EqualTo(35));
            Assert.That(materializedCharacter.Size, Is.EqualTo(Size.Gargantuan));
            Assert.That(materializedCharacter.CreatureType, Is.EqualTo(character.CreatureType));
        });
    }
}