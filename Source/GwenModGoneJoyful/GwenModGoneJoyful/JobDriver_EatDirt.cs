using RimWorld;

namespace GBTK_JobTypes;

public class JobDriver_EatDirt : JobDriver_Skygaze
{
    public override string GetReport()
    {
        //if (Map.gameConditionManager.ConditionIsActive(GameConditionDefOf.Eclipse))
        //{
        //    return "Eating Dirt";
        //}

        //if (Map.gameConditionManager.ConditionIsActive(GameConditionDefOf.Aurora))
        //{
        //    return "Eating Dirt";
        //}

        //var num = GenCelestial.CurCelestialSunGlow(Map);
        //if (num < 0.1f)
        //{
        //    return "Eating Dirt";
        //}

        //if (num >= 0.65f)
        //{
        //    return "Eating Dirt";
        //}

        //if (GenLocalDate.DayPercent(pawn) < 0.5f)
        //{
        //    return "Eating Dirt";
        //}

        return "Eating Dirt";
    }
}