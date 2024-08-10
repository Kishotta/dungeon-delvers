using DungeonDelvers.Common.Domain;

namespace DungeonDelvers.Modules.Monsters.Domain.DiceExpressions;

public static class DiceExpressionErrors
{
    public static readonly Error InvalidExpression =
        Error.Problem(
            "DiceExpression.InvalidExpression",
            "Expression must be of the form xdy(+/-)z");
}