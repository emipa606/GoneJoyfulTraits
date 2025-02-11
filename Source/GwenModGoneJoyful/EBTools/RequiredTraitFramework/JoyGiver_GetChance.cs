using HarmonyLib;
using RimWorld;
using Verse;

namespace EBTools.RequiredTraitFramework;

//Override Default GetChance
[HarmonyPatch(typeof(JoyGiver), nameof(JoyGiver.GetChance))]
internal class JoyGiver_GetChance
{
    public static void Postfix(JoyGiver __instance, Pawn pawn, ref float __result)
    {
        __result *= RequiredTrait_Utils.GetChanceModifierFromTraits(__instance, pawn);
    }
}