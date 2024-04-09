namespace DungeonDelvers.Domain.Effects;

public class MovementSpeedAdjustment(int adjustment) : Effect
{
    public int Adjustment { get; private set; } = adjustment;

    public override void Apply(MaterializedCharacter materializedCharacter)
    {
        materializedCharacter.MovementSpeed = int.Max(0, materializedCharacter.MovementSpeed + Adjustment);
    }
}