namespace DungeonDelvers.Domain;

public class Sense(uint distance)
{
    public uint Distance { get; private set; } = distance;

    public void Set(uint distance)
    {
        Distance = Math.Max(Distance, distance);
    }
}