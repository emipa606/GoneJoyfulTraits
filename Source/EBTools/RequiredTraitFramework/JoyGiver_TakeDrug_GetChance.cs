using HarmonyLib;
using RimWorld;
using Verse;

namespace EBTools.RequiredTraitFramework;

[HarmonyPatch(typeof(JoyGiver_TakeDrug), nameof(JoyGiver_TakeDrug.GetChance))]
internal static class JoyGiver_TakeDrug_GetChance
{
    public static void Postfix(JoyGiver_TakeDrug __instance, Pawn pawn, ref float __result)
    {
        __result *= RequiredTrait_Utils.GetChanceModifierFromTraits(__instance, pawn);
    }
}