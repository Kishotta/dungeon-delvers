using System.Text.RegularExpressions;
using DungeonDelvers.Common.Domain;

namespace DungeonDelvers.Modules.Monsters.Domain.DiceExpressions;

public partial record DiceExpression
{
    public string Expression { get; private init; } = string.Empty;
    public int DiceCount { get; private init; }
    public int DiceType { get; private init; }
    public int Modifier { get; private init; }
    public int Minimum { get; private init; }
    public int Maximum { get; private init; }
    public int Average { get; private init; }

    [GeneratedRegex(@"\s+")]
    private static partial Regex Whitespace();
    
    [GeneratedRegex(@"^(?<count>\d+)d(?<sides>\d+)(?<modifier>[+-]\d+)?")]
    private static partial Regex DiceExpressionRegex();
    
    private DiceExpression() { }

    public static Result<DiceExpression> Create(string expression) =>
        TrimExpression(expression)
            .Bind<string, DiceExpression>(trimmedExpression =>
            {
                if (!ParseExpression(trimmedExpression, out var diceCount, out var diceType, out var modifier))
                    return Result.Failure<DiceExpression>(DiceExpressionErrors.InvalidExpression);
                
                return new DiceExpression
                {
                    Expression = trimmedExpression,
                    DiceCount = diceCount,
                    DiceType = diceType,
                    Modifier = modifier,
                    Minimum = diceCount + modifier,
                    Maximum = diceCount * diceType + modifier,
                    Average = (diceCount + diceCount * diceType) / 2 + modifier
                };
            });
    
    private static Result<string> TrimExpression(string expression) => 
        Whitespace().Replace(expression, string.Empty);
    
    private static bool ParseExpression(string trimmedExpression, out int diceCount, out int diceType, out int modifier)
    {
        var match = DiceExpressionRegex().Match(trimmedExpression);
        if (!match.Success)
        {
            diceCount = 0;
            diceType = 0;
            modifier = 0;
            return false;
        }
        
        diceCount = int.Parse(match.Groups["count"].Value);
        diceType = int.Parse(match.Groups["sides"].Value);
        modifier = match.Groups["modifier"].Success ? int.Parse(match.Groups["modifier"].Value) : 0;
        return true;
    }
}