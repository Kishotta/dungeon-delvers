using DungeonDelvers.Domain.Effects;

namespace DungeonDelvers.Domain.Tests;

[TestFixture]
public class DwarfRaceTest
{
    [Test]
    public void CharacterMaterialization_WhenMountainDwarfRace_ShouldCreateCharacterWithMountainDwarfEffects()
    {
        var dwarf = Race.Create(null, "Dwarf", "Description", CreatureType.Humanoid, 25u, 50, 350);
        
        var abilityScoreIncreaseTrait = Trait.Create("Ability Score Increase", "Your Constitution score increases by 2.");
        var abilityScoreAdjustmentEffect = new AbilityScoreAdjustment(Ability.Constitution, 2);
        abilityScoreIncreaseTrait.AddEffect(abilityScoreAdjustmentEffect);
        dwarf.AddTrait(abilityScoreIncreaseTrait);
        
        var sizeTrait = Trait.Create("Size", " Dwarves stand between 4 and 5 feet tall and average about 150 pounds. Your size is Medium.");
        var sizeEffect = new SizeOverride(Size.Medium);
        sizeTrait.AddEffect(sizeEffect);
        dwarf.AddTrait(sizeTrait);
        
        var darkvisionTrait = Trait.Create("Darkvision", "Accustomed to life underground, you have superior vision in dark and dim conditions. You can see in dim light within 60 feet of you as if it were bright light, and in darkness as if it were dim light. You can't discern color in darkness, only shades of gray.");
        var darkvisionEffect = new Darkvision(60u);
        darkvisionTrait.AddEffect(darkvisionEffect);
        dwarf.AddTrait(darkvisionTrait);
        
        var dwarvenResilienceTrait = Trait.Create("Dwarven Resilience", "You have advantage on saving throws against poison, and you have resistance against poison damage.");
        var savingThrowAdvantageEffect = new SavingThrowAdvantage(Ability.Constitution, "poison");
        dwarvenResilienceTrait.AddEffect(savingThrowAdvantageEffect);
        dwarf.AddTrait(dwarvenResilienceTrait);

        var mountainDwarf = Race.Create(dwarf, "Mountain Dwarf", "Description", CreatureType.Humanoid, 25u, 50, 350);
        
        var abilityScoreIncreaseTrait2 = Trait.Create("Ability Score Increase", "Your Strength score increases by 2.");
        var abilityScoreAdjustmentEffect2 = new AbilityScoreAdjustment(Ability.Strength, 2);
        abilityScoreIncreaseTrait2.AddEffect(abilityScoreAdjustmentEffect2);
        mountainDwarf.AddTrait(abilityScoreIncreaseTrait2);
        
        var character = Character.Create(
            "Bruenor",
            mountainDwarf,
            Size.Medium,
            new AbilityScore(10),
            new AbilityScore(10),
            new AbilityScore(10),
            new AbilityScore(10),
            new AbilityScore(10),
            new AbilityScore(10)).Materialize();
        
        Assert.Multiple(() =>
        {
            Assert.That(character.Constitution.Score, Is.EqualTo(12));
            Assert.That(character.Strength.Score, Is.EqualTo(12));
            Assert.That(character.Size, Is.EqualTo(Size.Medium));
            Assert.That(character.MovementSpeed, Is.EqualTo(25u));
            Assert.That(character.Darkvision.Distance, Is.EqualTo(60u));
            Assert.That(character.ConstitutionSave.Advantages, Contains.Item("poison"));
        });
    }
}