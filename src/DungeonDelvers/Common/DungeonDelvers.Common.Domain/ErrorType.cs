namespace DungeonDelvers.Common.Domain;

public enum ErrorType
{
    Failure = 0,
    Validation = 1,
    Problem = 2,
    NotFound = 3,
    Conflict = 4,
    Authorization = 5,
}