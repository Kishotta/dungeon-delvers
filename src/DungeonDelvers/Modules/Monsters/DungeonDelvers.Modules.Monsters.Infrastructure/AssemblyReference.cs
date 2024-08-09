using System.Reflection;

namespace DungeonDelvers.Modules.Monsters.Infrastructure;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}