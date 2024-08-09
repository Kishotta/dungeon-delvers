using System.Reflection;

namespace DungeonDelvers.Common.Domain;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}