using DungeonDelvers.Common.Domain;
using DungeonDelvers.Common.Domain.Auditing;
using DungeonDelvers.Modules.Monsters.Domain.ChallengeRatings;
using DungeonDelvers.Modules.Monsters.Domain.DiceExpressions;
using DungeonDelvers.Modules.Monsters.Domain.Monsters.DomainEvents;

namespace DungeonDelvers.Modules.Monsters.Domain.Monsters;

/// <summary>
/// Represents a creature or entity encountered in the world.
/// A Monster is defined by its various attributes, abilities, and behaviors,
/// as detailed in the Monster Manual, which serves as a reference guide
/// for Dungeon Masters, as well as other source books. Monsters can vary
/// widely in strength, intelligence, and alignment, and are used to challenge
/// players, provide narrative elements, and enhance the overall gameplay experience.
/// </summary>
[Auditable]
public class Monster : Entity
{
    /// <summary>
    /// Whether the Monster is an official creature from a published source book.
    /// </summary>
    public bool Official { get; private set; }
    
    /// <summary>
    /// The name of the Monster.
    /// </summary>
    public string Name { get; private set; } = string.Empty;
    
    /// <summary>
    /// The hit points of the Monster, represented as a DiceExpression.
    /// </summary>
    public DiceExpression HitPoints { get; private set; } = default!;
    
    /// <summary>
    /// The Challenge Rating of the Monster, which is used to determine its difficulty.
    /// </summary>
    public ChallengeRating ChallengeRating { get; private set; } = default!;
    
    private Monster() { }

    /// <summary>
    /// Creates a new Monster with the specified attributes.
    /// </summary>
    /// <param name="official">Whether the Monster is an official creature from a published source book.</param>
    /// <param name="name">The name of the Monster</param>
    /// <param name="hitPoints">The hit points of the Monster, represented as a DiceExpression</param>
    /// <param name="challengeRating">The Challenge Rating of the Monster</param>
    /// <returns></returns>
    public static Monster Create(
        bool official,
        string name,
        DiceExpression hitPoints,
        ChallengeRating challengeRating)
    {
        var monster = new Monster
        {
            Id = Guid.NewGuid(),
            Official = official,
            Name = name,
            HitPoints = hitPoints,
            ChallengeRating = challengeRating
        };

        monster.RaiseDomainEvent(new MonsterCreatedDomainEvent(monster.Id));
        
        return monster;
    }

    /// <summary>
    /// Change the name of the Monster.
    /// </summary>
    /// <remarks>
    /// Does not raise a domain event if the new name is the same as the current name.
    /// </remarks>
    /// <param name="name">The new name of the Monster</param>
    public void ChangeName(string name)
    {
        if (Name == name) return;
        
        Name = name;
        
        RaiseDomainEvent(new MonsterNameChangedDomainEvent(Id, name));
    }

    /// <summary>
    /// Change the hit points of the Monster.
    /// </summary>
    /// <remarks>
    /// Does not raise a domain event if the new hit points are the same as the current hit points.
    /// </remarks>
    /// <param name="hitPoints">The new hit points of the Monster</param>
    public void ChangeHitPoints(DiceExpression hitPoints)
    {
        if (HitPoints == hitPoints) return;
        
        HitPoints = hitPoints;
        
        RaiseDomainEvent(new MonsterHitPointsChangedDomainEvent(Id, hitPoints));
    }

    /// <summary>
    /// Change the Challenge Rating of the Monster.
    /// </summary>
    /// <remarks>
    /// Does not raise a domain event if the new Challenge Rating is the same as the current Challenge Rating.
    /// </remarks>
    /// <param name="challengeRating">The new challenge rating of the Monster</param>
    public void ChangeChallengeRating(ChallengeRating challengeRating)
    {
        if (ChallengeRating == challengeRating) return;
        
        ChallengeRating = challengeRating;
        
        RaiseDomainEvent(new MonsterChallengeRatingChangedDomainEvent(Id, challengeRating));
    }
}