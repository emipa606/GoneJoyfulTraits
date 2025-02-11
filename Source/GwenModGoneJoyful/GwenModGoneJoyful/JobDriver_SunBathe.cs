using RimWorld;

namespace GBTK_JobTypes;

public class JobDriver_SunBathe : JobDriver_Skygaze
{
    public override string GetReport()
    {
        //if (Map.gameConditionManager.ConditionIsActive(GameConditionDefOf.Eclipse))
        //{
        //    return "Sunbathing";
        //}

        //if (Map.gameConditionManager.ConditionIsActive(GameConditionDefOf.Aurora))
        //{
        //    return "Sunbathing";
        //}

        //var num = GenCelestial.CurCelestialSunGlow(Map);
        //if (num < 0.1f)
        //{
        //    return "Sunbathing";
        //}

        //if (num >= 0.65f)
        //{
        //    return "Sunbathing";
        //}

        //if (GenLocalDate.DayPercent(pawn) < 0.5f)
        //{
        //    return "Sunbathing";
        //}

        return "Sunbathing";
    }
}