using HarmonyLib;
using Verse;

namespace EBTools;

[StaticConstructorOnStartup]
internal static class HarmonyInit
{
    static HarmonyInit()
    {
        var harmony = new Harmony("EBToolsHarmony");
        // harmony.PatchAll(Assembly.GetExecutingAssembly());
        harmony.PatchAll();
    }
}