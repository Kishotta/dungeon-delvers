namespace DungeonDelvers.Domain.Effects;

public class MovementSpeedAdjustment : Effect
{
    public int Adjustment { get; private set; }

    public MovementSpeedAdjustment(int adjustment)
    {
        Adjustment = adjustment;
    }

    public override void Apply(MaterializedCharacter materializedCharacter)
    {
        materializedCharacter.MovementSpeed = int.Max(0, materializedCharacter.MovementSpeed + Adjustment);
    }
}