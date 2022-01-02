using HarmonyLib;
using RimWorld;
using Verse;

namespace EBTools.RequiredTraitFramework;

//Override Default GetChance
[HarmonyPatch(typeof(JoyGiver), "GetChance")]
internal class RequiredTrait_JoyGiver_GetChance
{
    private static void Postfix(JoyGiver __instance, Pawn pawn, ref float __result)
    {
        __result *= RequiredTrait_Utils.GetChanceModifierFromTraits(__instance, pawn);
    }
}

//Override Drug GetChance
[HarmonyPatch(typeof(JoyGiver_TakeDrug), "GetChance")]
internal class RequiredTrait_JoyGiver_TakeDrug_GetChance
{
    private static void Postfix(JoyGiver_TakeDrug __instance, Pawn pawn, ref float __result)
    {
        __result *= RequiredTrait_Utils.GetChanceModifierFromTraits(__instance, pawn);
    }
}

//Override Skywatch Get Chance
[HarmonyPatch(typeof(JoyGiver_Skygaze), "GetChance")]
internal class RequiredTrait_JoyGiver_Skygaze_GetChance
{
    private static void Postfix(JoyGiver_Skygaze __instance, Pawn pawn, ref float __result)
    {
        __result *= RequiredTrait_Utils.GetChanceModifierFromTraits(__instance, pawn);
    }
}