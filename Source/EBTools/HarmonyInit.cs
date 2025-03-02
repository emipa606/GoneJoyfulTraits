using System.Reflection;
using HarmonyLib;
using Verse;

namespace EBTools;

[StaticConstructorOnStartup]
internal static class HarmonyInit
{
    static HarmonyInit()
    {
        new Harmony("EBToolsHarmony").PatchAll(Assembly.GetExecutingAssembly());
    }
}