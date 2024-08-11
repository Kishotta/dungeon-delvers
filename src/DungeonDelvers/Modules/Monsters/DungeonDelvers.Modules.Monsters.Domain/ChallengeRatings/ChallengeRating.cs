using DungeonDelvers.Common.Domain;

namespace DungeonDelvers.Modules.Monsters.Domain.ChallengeRatings;

public record ChallengeRating
{
    public float Value { get; private init; }

    private ChallengeRating() { }

    private static readonly float[] ValidValues = [
        0f, 
        0.125f,
        0.25f,
        0.5f, 
        1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 
        11, 12, 13, 14, 15, 16, 17, 18, 19, 20,
        21, 22, 23, 24, 25, 26, 27, 28, 29, 30
    ];

    public static Result<ChallengeRating> Create(float value)
    {
        return ValidValues.Contains(value) 
            ? new ChallengeRating { Value = value }
            : Result.Failure<ChallengeRating>(ChallengeRatingErrors.InvalidValue(value));
    }
}