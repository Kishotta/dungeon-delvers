using DungeonDelvers.Common.Domain;

namespace DungeonDelvers.Common.Application.Exceptions;

public sealed class DungeonDelversException(
    string requestName,
    Error? error = default,
    Exception? innerException = default)
    : Exception("Application exception", innerException)
{
    public string RequestName { get; } = requestName;

    public Error? Error { get; } = error;
}
