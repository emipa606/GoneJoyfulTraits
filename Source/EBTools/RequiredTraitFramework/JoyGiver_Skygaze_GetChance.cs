using HarmonyLib;
using RimWorld;
using Verse;

namespace EBTools.RequiredTraitFramework;

[HarmonyPatch(typeof(JoyGiver_Skygaze), nameof(JoyGiver_Skygaze.GetChance))]
internal class JoyGiver_Skygaze_GetChance
{
    private static void Postfix(JoyGiver_Skygaze __instance, Pawn pawn, ref float __result)
    {
        __result *= RequiredTrait_Utils.GetChanceModifierFromTraits(__instance, pawn);
    }
}