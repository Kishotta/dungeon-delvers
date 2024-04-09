namespace DungeonDelvers.Domain;

public class Sense(uint range)
{
    public uint Range { get; private set; } = range;

    public void Set(uint range)
    {
        Range = Math.Max(Range, range);
    }
}