using HarmonyLib;
using RimWorld;
using Verse;

namespace EBTools.RequiredTraitFramework;

[HarmonyPatch(typeof(JoyGiver_TakeDrug), nameof(JoyGiver_TakeDrug.GetChance))]
internal class JoyGiver_TakeDrug_GetChance
{
    private static void Postfix(JoyGiver_TakeDrug __instance, Pawn pawn, ref float __result)
    {
        __result *= RequiredTrait_Utils.GetChanceModifierFromTraits(__instance, pawn);
    }
}