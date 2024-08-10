using System.Text.RegularExpressions;
using DungeonDelvers.Common.Domain;

namespace DungeonDelvers.Modules.Monsters.Domain.DiceExpressions;

public partial class DiceExpression
{
    public string Expression { get; private init; } = string.Empty;
    
    // TODO: Store these computed properties in the database
    public int DiceCount => int.Parse(DiceExpressionRegex().Match(Expression).Groups["count"].Value);
    public int DiceType => int.Parse(DiceExpressionRegex().Match(Expression).Groups["sides"].Value);
    public int Modifier => DiceExpressionRegex().Match(Expression).Groups["modifier"].Success ?
            int.Parse(DiceExpressionRegex().Match(Expression).Groups["modifier"].Value) : 0;
    public int Minimum => DiceCount + Modifier;
    public int Maximum => DiceCount * DiceType + Modifier;
    public int Average => (DiceCount + DiceCount * DiceType) / 2 + Modifier;

    [GeneratedRegex(@"\s+")]
    private static partial Regex Whitespace();
    
    [GeneratedRegex(@"^(?<count>\d+)d(?<sides>\d+)(?<modifier>[+-]\d+)?")]
    private static partial Regex DiceExpressionRegex();
    
    private DiceExpression() { }

    public static Result<DiceExpression> Create(string expression) =>
        TrimExpression(expression)
            .Bind<string>(ValidateExpression)
            .Bind<string, DiceExpression>((trimmedExpression) => new DiceExpression
            {
                Expression = trimmedExpression
            });

    private static Result<string> TrimExpression(string expression) => 
        Whitespace().Replace(expression, string.Empty);

    private static Result<string> ValidateExpression(string expression) =>
        !DiceExpressionRegex().IsMatch(expression) ?
            Result.Failure<string>(DiceExpressionErrors.InvalidExpression) : 
            Result.Success(expression);
}