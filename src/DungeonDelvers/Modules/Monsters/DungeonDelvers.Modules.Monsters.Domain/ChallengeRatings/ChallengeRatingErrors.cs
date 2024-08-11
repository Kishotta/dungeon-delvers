using DungeonDelvers.Common.Domain;

namespace DungeonDelvers.Modules.Monsters.Domain.ChallengeRatings;

public static class ChallengeRatingErrors
{
    public static Error InvalidValue(float value) =>
        Error.Problem(
            "ChallengeRating.InvalidValue",
            $"{value:0.##} is not a valid challenge rating");
}