using System.Reflection;

namespace DungeonDelvers.Common.Presentation;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}