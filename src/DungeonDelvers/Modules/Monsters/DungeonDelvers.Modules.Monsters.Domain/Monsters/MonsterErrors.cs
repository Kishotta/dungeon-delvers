using DungeonDelvers.Common.Domain;

namespace DungeonDelvers.Modules.Monsters.Domain.Monsters;

public static class MonsterErrors
{
    public static Error NotFound(Guid monsterId) =>
        Error.NotFound(
            "Monsters.NotFound",
            $"The monster with identifier {monsterId} was not found.");
}