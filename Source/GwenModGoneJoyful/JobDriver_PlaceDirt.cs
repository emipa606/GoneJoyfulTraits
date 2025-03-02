using RimWorld;

namespace GBTK_JobTypes;

public class JobDriver_PlaceDirt : JobDriver_Skygaze
{
    public override string GetReport()
    {
        //if (Map.gameConditionManager.ConditionIsActive(GameConditionDefOf.Eclipse))
        //{
        //    return "Placing Dirt";
        //}

        //if (Map.gameConditionManager.ConditionIsActive(GameConditionDefOf.Aurora))
        //{
        //    return "Placing Dirt";
        //}

        //var num = GenCelestial.CurCelestialSunGlow(Map);
        //if (num < 0.1f)
        //{
        //    return "Placing Dirt";
        //}

        //if (num >= 0.65f)
        //{
        //    return "Placing Dirt";
        //}

        //if (GenLocalDate.DayPercent(pawn) < 0.5f)
        //{
        //    return "Placing Dirt";
        //}

        return "Placing Dirt";
    }
}