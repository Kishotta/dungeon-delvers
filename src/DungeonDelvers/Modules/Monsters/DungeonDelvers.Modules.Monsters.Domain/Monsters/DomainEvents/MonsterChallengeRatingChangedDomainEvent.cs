using DungeonDelvers.Common.Domain;
using DungeonDelvers.Modules.Monsters.Domain.ChallengeRatings;

namespace DungeonDelvers.Modules.Monsters.Domain.Monsters.DomainEvents;

public sealed class MonsterChallengeRatingChangedDomainEvent(Guid monsterId, ChallengeRating challengeRating) : DomainEvent
{
    public Guid MonsterId { get; } = monsterId;
    public ChallengeRating ChallengeRating { get; } = challengeRating;
}