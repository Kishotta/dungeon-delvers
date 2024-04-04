using DungeonDelvers.Domain.Effects;

namespace DungeonDelvers.Domain.Tests;

public class CharacterTests
{
    [Test]
    public void Create_ValidProperties_PropertiesSet()
    {
        var characterName = "Test Character";
        var race = Race.Create("Test Race", "Test Description");
        var baseMovementSpeed = 30u;
        var size = Size.Medium;
        var creatureType = CreatureType.Humanoid;

        var character = Character.Create(characterName, race, baseMovementSpeed, size, creatureType);
        
        Assert.Multiple(() =>
        {
            Assert.That(character.Name, Is.EqualTo(characterName));
            Assert.That(character.BaseMovementSpeed, Is.EqualTo(baseMovementSpeed));
            Assert.That(character.Size, Is.EqualTo(size));
            Assert.That(character.CreatureType, Is.EqualTo(creatureType));
        });
    }

    [Test]
    public void Materialize_NoEffects_ReturnsMaterializedCharacterWithDefaults()
    {
        var race = Race.Create("Test Race", "Test Description");
        var character = Character.Create(
            "Test Character",
            race,
            40u, // Non-default movement speed
            Size.Medium, 
            CreatureType.Humanoid);
        
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
        var race = Race.Create("Test Race", "Test Description");
        var trait = Trait.Create("Test Trait", "Test Description");
        trait.AddEffect(new MovementSpeedAdjustment(-10));
        race.AddTrait(trait);
        var character = Character.Create(
            "Test Character",
            race,
            40u, // Non-default movement speed
            Size.Medium, 
            CreatureType.Humanoid);
        
        var materializedCharacter = character.Materialize();
        
        Assert.Multiple(() =>
        {
            Assert.That(materializedCharacter.Name, Is.EqualTo(character.Name));
            Assert.That(materializedCharacter.MovementSpeed, Is.EqualTo(30));
            Assert.That(materializedCharacter.Size, Is.EqualTo(character.Size));
            Assert.That(materializedCharacter.CreatureType, Is.EqualTo(character.CreatureType));
        });
    }
    
    [Test]
    public void Materialize_SingleEffect_ReturnsMaterializedCharacterWithEffectApplied()
    {
        var race = Race.Create("Test Race", "Test Description");
        var character = Character.Create(
            "Test Character",
            race,
            40u, // Non-default movement speed
            Size.Medium, 
            CreatureType.Humanoid);
        
        character.AddEffect(new MovementSpeedAdjustment(-10));
        
        var materializedCharacter = character.Materialize();
        
        Assert.Multiple(() =>
        {
            Assert.That(materializedCharacter.Name, Is.EqualTo(character.Name));
            Assert.That(materializedCharacter.MovementSpeed, Is.EqualTo(30));
            Assert.That(materializedCharacter.Size, Is.EqualTo(character.Size));
            Assert.That(materializedCharacter.CreatureType, Is.EqualTo(character.CreatureType));
        });
    }
    
    [Test]
    public void Materialize_RaceTraitMultipleEffect_SameType_ReturnsMaterializedCharacterWithEffectsApplied()
    {
        var race = Race.Create("Test Race", "Test Description");
        var trait = Trait.Create("Test Trait", "Test Description");
        trait.AddEffect(new MovementSpeedAdjustment(5));
        trait.AddEffect(new MovementSpeedAdjustment(5));
        race.AddTrait(trait);
        var character = Character.Create(
            "Test Character",
            race,
            40u, // Non-default movement speed
            Size.Medium, 
            CreatureType.Humanoid);
        
        var materializedCharacter = character.Materialize();
        
        Assert.Multiple(() =>
        {
            Assert.That(materializedCharacter.Name, Is.EqualTo(character.Name));
            Assert.That(materializedCharacter.MovementSpeed, Is.EqualTo(50));
            Assert.That(materializedCharacter.Size, Is.EqualTo(character.Size));
            Assert.That(materializedCharacter.CreatureType, Is.EqualTo(character.CreatureType));
        });
    }
    
    [Test]
    public void Materialize_RaceMultipleTraitSingleEffect_SameType_ReturnsMaterializedCharacterWithEffectsApplied()
    {
        var race = Race.Create("Test Race", "Test Description");
        
        var firstTrait = Trait.Create("First Test Trait", "Test Description");
        firstTrait.AddEffect(new MovementSpeedAdjustment(5));
        race.AddTrait(firstTrait);
        
        var secondTrait = Trait.Create("Second Test Trait", "Test Description");
        secondTrait.AddEffect(new MovementSpeedAdjustment(5));
        race.AddTrait(secondTrait);
        
        var character = Character.Create(
            "Test Character",
            race,
            40u, // Non-default movement speed
            Size.Medium, 
            CreatureType.Humanoid);
        
        var materializedCharacter = character.Materialize();
        
        Assert.Multiple(() =>
        {
            Assert.That(materializedCharacter.Name, Is.EqualTo(character.Name));
            Assert.That(materializedCharacter.MovementSpeed, Is.EqualTo(50));
            Assert.That(materializedCharacter.Size, Is.EqualTo(character.Size));
            Assert.That(materializedCharacter.CreatureType, Is.EqualTo(character.CreatureType));
        });
    }
    
    [Test]
    public void Materialize_MultipleEffect_SameType_ReturnsMaterializedCharacterWithEffectsApplied()
    {
        var race = Race.Create("Test Race", "Test Description");
        var character = Character.Create(
            "Test Character",
            race,
            40u, // Non-default movement speed
            Size.Medium, 
            CreatureType.Humanoid);
        
        character.AddEffect(new MovementSpeedAdjustment(5));
        character.AddEffect(new MovementSpeedAdjustment(5));
        
        var materializedCharacter = character.Materialize();
        
        Assert.Multiple(() =>
        {
            Assert.That(materializedCharacter.Name, Is.EqualTo(character.Name));
            Assert.That(materializedCharacter.MovementSpeed, Is.EqualTo(50));
            Assert.That(materializedCharacter.Size, Is.EqualTo(character.Size));
            Assert.That(materializedCharacter.CreatureType, Is.EqualTo(character.CreatureType));
        });
    }
    
    [Test]
    public void Materialize_RaceMultipleTraitSingleEffect_DifferentTypes_ReturnsMaterializedCharacterWithEffectsApplied()
    { 
        var race = Race.Create("Test Race", "Test Description");
        var firstTrait = Trait.Create("First Test Trait", "Test Description");
        firstTrait.AddEffect(new MovementSpeedAdjustment(5));
        
        var secondTrait = Trait.Create("Second Test Trait", "Test Description");
        secondTrait.AddEffect(new SizeOverride(Size.Gargantuan));
        
        race.AddTrait(firstTrait);
        race.AddTrait(secondTrait);
        
        var character = Character.Create(
            "Test Character",
            race,
            40u, // Non-default movement speed
            Size.Medium, 
            CreatureType.Humanoid);
        
        var materializedCharacter = character.Materialize();
        
        Assert.Multiple(() =>
        {
            Assert.That(materializedCharacter.Name, Is.EqualTo(character.Name));
            Assert.That(materializedCharacter.MovementSpeed, Is.EqualTo(45));
            Assert.That(materializedCharacter.Size, Is.EqualTo(Size.Gargantuan));
            Assert.That(materializedCharacter.CreatureType, Is.EqualTo(character.CreatureType));
        });
    }
}