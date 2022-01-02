using System.Collections.Generic;
using RimWorld;
using Verse;
using Verse.AI;

namespace GBTK_JobTypes;

//pawn likes to take non watery path. not sure how to change it. at least with this method it seeks out new water cells
public class JoyGiver_EnjoyWaterTiles : JoyGiver
{
    public override Job TryGiveJob(Pawn pawn)
    {
        if (PawnUtility.WillSoonHaveBasicNeed(pawn))
        {
            return null;
        }

        if (!JoyUtility.EnjoyableOutsideNow(pawn))
        {
            return null;
        }

        bool CellValidator(IntVec3 x)
        {
            return x.GetTerrain(pawn.Map).extinguishesFire;
        }

        bool Validator(Region x)
        {
            return x.TryFindRandomCellInRegionUnforbidden(pawn, CellValidator, out _);
        }

        if (!CellFinder.TryFindClosestRegionWith(pawn.GetRegion(), TraverseParms.For(pawn), Validator, 500,
                out var reg))
        {
            return null;
        }

        if (!reg.TryFindRandomCellInRegionUnforbidden(pawn, CellValidator, out var root))
        {
            return null;
        }

        if (!WalkPathFinder.TryFindWalkPath(pawn, root, out var list))
        {
            return null;
        }

        if (!reg.TryFindRandomCellInRegionUnforbidden(pawn, CellValidator, out var root2))
        {
            return null;
        }

        if (!WalkPathFinder.TryFindWalkPath(pawn, root2, out var list2))
        {
            return null;
        }

        if (!reg.TryFindRandomCellInRegionUnforbidden(pawn, CellValidator, out var root4))
        {
            return null;
        }

        if (!WalkPathFinder.TryFindWalkPath(pawn, root4, out var list4))
        {
            return null;
        }

        if (!reg.TryFindRandomCellInRegionUnforbidden(pawn, CellValidator, out _))
        {
            return null;
        }

        if (!WalkPathFinder.TryFindWalkPath(pawn, root4, out var list5))
        {
            return null;
        }

        var job = new Job(def.jobDef, list[0])
        {
            targetQueueA = new List<LocalTargetInfo>()
        };
        //can't figure out how to get hte pawn to wait so this has them sit for an extra tick. sadly not more than that.
        list[1] = list[0];
        list[2] = list2[0];
        list[3] = list2[0];
        list[4] = list4[0];
        list[5] = list4[0];
        list[6] = list5[0];
        list[7] = list5[0];
        job.targetQueueA.Add(list[0]);
        job.targetQueueA.Add(list[1]);
        job.targetQueueA.Add(list[2]);
        job.targetQueueA.Add(list[3]);
        job.targetQueueA.Add(list[4]);
        job.targetQueueA.Add(list[5]);
        job.targetQueueA.Add(list[6]);
        job.targetQueueA.Add(list[7]);
        /* adding this made htem go back to the start again. its intersting. still nots rue how this translates to wandering only in water
        for (int i = 1; i < list.Count; i++)
        {
            job.targetQueueA.Add(list[i]);
        }
        */
        job.locomotionUrgency = LocomotionUrgency.Jog;
        return job;
    }
}

public class JoyGiver_GoForWalk_InRain : JoyGiver
{
    public override Job TryGiveJob(Pawn pawn)
    {
        var DoesCurrentWeatherMakePawnWet = "Null";
        if (pawn.Map.weatherManager.curWeather.exposedThought != null)
        {
            DoesCurrentWeatherMakePawnWet = pawn.Map.weatherManager.curWeather.exposedThought.ToString();
        }

        if (DoesCurrentWeatherMakePawnWet != "SoakingWet")
        {
            return null;
        }

        if (!JoyUtility.EnjoyableOutsideNow(pawn))
        {
            return null;
        }

        if (PawnUtility.WillSoonHaveBasicNeed(pawn))
        {
            return null;
        }

        bool CellValidator(IntVec3 x)
        {
            return !PawnUtility.KnownDangerAt(x, pawn.Map, pawn) && !x.GetTerrain(pawn.Map).avoidWander &&
                   x.Standable(pawn.Map) && !x.Roofed(pawn.Map);
        }

        bool Validator(Region x)
        {
            return x.Room.PsychologicallyOutdoors && !x.IsForbiddenEntirely(pawn) &&
                   x.TryFindRandomCellInRegionUnforbidden(pawn, CellValidator, out _);
        }

        if (!CellFinder.TryFindClosestRegionWith(pawn.GetRegion(), TraverseParms.For(pawn), Validator, 100,
                out var reg))
        {
            return null;
        }

        if (!reg.TryFindRandomCellInRegionUnforbidden(pawn, CellValidator, out var root))
        {
            return null;
        }

        if (!WalkPathFinder.TryFindWalkPath(pawn, root, out var list))
        {
            return null;
        }

        var job = new Job(def.jobDef, list[0])
        {
            targetQueueA = new List<LocalTargetInfo>()
        };
        for (var i = 1; i < list.Count; i++)
        {
            job.targetQueueA.Add(list[i]);
        }

        job.locomotionUrgency = LocomotionUrgency.Walk;
        return job;
    }
}

public class JoyGiver_GoForWalk_InSnow : JoyGiver
{
    public override Job TryGiveJob(Pawn pawn)
    {
        float HowMuchSnowIsThere = 0;
        if (pawn.Map.weatherManager.curWeather.snowRate > 0.0f)
        {
            HowMuchSnowIsThere = pawn.Map.weatherManager.curWeather.snowRate;
        }

        if (HowMuchSnowIsThere < 0.1f)
        {
            return null;
        }

        if (!JoyUtility.EnjoyableOutsideNow(pawn))
        {
            return null;
        }

        if (PawnUtility.WillSoonHaveBasicNeed(pawn))
        {
            return null;
        }

        bool CellValidator(IntVec3 x)
        {
            return !PawnUtility.KnownDangerAt(x, pawn.Map, pawn) && !x.GetTerrain(pawn.Map).avoidWander &&
                   x.Standable(pawn.Map) && !x.Roofed(pawn.Map);
        }

        bool Validator(Region x)
        {
            return x.Room.PsychologicallyOutdoors && !x.IsForbiddenEntirely(pawn) &&
                   x.TryFindRandomCellInRegionUnforbidden(pawn, CellValidator, out _);
        }

        if (!CellFinder.TryFindClosestRegionWith(pawn.GetRegion(), TraverseParms.For(pawn), Validator, 100,
                out var reg))
        {
            return null;
        }

        if (!reg.TryFindRandomCellInRegionUnforbidden(pawn, CellValidator, out var root))
        {
            return null;
        }

        if (!WalkPathFinder.TryFindWalkPath(pawn, root, out var list))
        {
            return null;
        }

        var job = new Job(def.jobDef, list[0])
        {
            targetQueueA = new List<LocalTargetInfo>()
        };
        for (var i = 1; i < list.Count; i++)
        {
            job.targetQueueA.Add(list[i]);
        }

        job.locomotionUrgency = LocomotionUrgency.Walk;
        return job;
    }
}

public class JoyGiver_GBKT_RunBackAndForth : JoyGiver
{
    public override Job TryGiveJob(Pawn pawn)
    {
        if (PawnUtility.WillSoonHaveBasicNeed(pawn))
        {
            return null;
        }

        bool CellValidator(IntVec3 x)
        {
            return !PawnUtility.KnownDangerAt(x, pawn.Map, pawn) && !x.GetTerrain(pawn.Map).avoidWander &&
                   x.Standable(pawn.Map) && !x.Roofed(pawn.Map);
        }

        bool Validator(Region x)
        {
            return x.TryFindRandomCellInRegionUnforbidden(pawn, CellValidator, out _);
        }

        if (!CellFinder.TryFindClosestRegionWith(pawn.GetRegion(), TraverseParms.For(pawn), Validator, 500,
                out var reg))
        {
            return null;
        }

        if (!reg.TryFindRandomCellInRegionUnforbidden(pawn, CellValidator, out var root))
        {
            return null;
        }

        if (!WalkPathFinder.TryFindWalkPath(pawn, root, out var list))
        {
            return null;
        }

        var job = new Job(def.jobDef, list[0])
        {
            targetQueueA = new List<LocalTargetInfo>()
        };
        //can't figure out how to get hte pawn to wait so this has them sit for an extra tick. sadly not more than that.
        //list[1] = list[1];
        list[1] = list[2];
        //list[2] = list[0];
        list[3] = list[2];
        list[4] = list[0];
        list[5] = list[2];
        list[6] = list[0];
        list[7] = list[2];
        list[8] = list[0];
        list[2] = list[0];
        job.targetQueueA.Add(list[0]);
        job.targetQueueA.Add(list[1]);
        job.targetQueueA.Add(list[2]);
        job.targetQueueA.Add(list[3]);
        job.targetQueueA.Add(list[4]);
        job.targetQueueA.Add(list[5]);
        job.targetQueueA.Add(list[6]);
        job.targetQueueA.Add(list[7]);
        job.targetQueueA.Add(list[8]);
        /* adding this made htem go back to the start again. its intersting. still nots rue how this translates to wandering only in water
        for (int i = 1; i < list.Count; i++)
        {
            job.targetQueueA.Add(list[i]);
        }
        */
        job.locomotionUrgency = LocomotionUrgency.Sprint;
        return job;
    }
}

public class JoyGiver_PlaceSomeDirt : JoyGiver
{
    public override Job TryGiveJob(Pawn pawn)
    {
        if (PawnUtility.WillSoonHaveBasicNeed(pawn))
        {
            return null;
        }

        bool CellValidator(IntVec3 x)
        {
            return !PawnUtility.KnownDangerAt(x, pawn.Map, pawn) && !x.GetTerrain(pawn.Map).avoidWander &&
                   x.Standable(pawn.Map) && x.Roofed(pawn.Map);
        }

        bool Validator(Region x)
        {
            return !x.IsForbiddenEntirely(pawn) && x.TryFindRandomCellInRegionUnforbidden(pawn, CellValidator, out _);
        }

        if (!CellFinder.TryFindClosestRegionWith(pawn.GetRegion(), TraverseParms.For(pawn), Validator, 500,
                out var reg))
        {
            return null;
        }

        if (!reg.TryFindRandomCellInRegionUnforbidden(pawn, CellValidator, out var root))
        {
            return null;
        }

        if (!WalkPathFinder.TryFindWalkPath(pawn, root, out var list))
        {
            return null;
        }

        var job = new Job(def.jobDef, list[0])
        {
            targetQueueA = new List<LocalTargetInfo> { list[0] },
            locomotionUrgency = LocomotionUrgency.Walk
        };
        return job;
    }
}

public class JobDriver_PlaceDirt : JobDriver_Skygaze
{
    public override string GetReport()
    {
        if (Map.gameConditionManager.ConditionIsActive(GameConditionDefOf.Eclipse))
        {
            return "Placing Dirt";
        }

        if (Map.gameConditionManager.ConditionIsActive(GameConditionDefOf.Aurora))
        {
            return "Placing Dirt";
        }

        var num = GenCelestial.CurCelestialSunGlow(Map);
        if (num < 0.1f)
        {
            return "Placing Dirt";
        }

        if (num >= 0.65f)
        {
            return "Placing Dirt";
        }

        if (GenLocalDate.DayPercent(pawn) < 0.5f)
        {
            return "Placing Dirt";
        }

        return "Placing Dirt";
    }
}

public class JoyGiver_EatSomeDirt : JoyGiver
{
    public override Job TryGiveJob(Pawn pawn)
    {
        if (PawnUtility.WillSoonHaveBasicNeed(pawn))
        {
            return null;
        }

        bool CellValidator(IntVec3 x)
        {
            return !PawnUtility.KnownDangerAt(x, pawn.Map, pawn) && !x.GetTerrain(pawn.Map).avoidWander &&
                   x.Standable(pawn.Map) && x.SupportsStructureType(pawn.Map, TerrainAffordanceDefOf.Diggable);
        }

        bool Validator(Region x)
        {
            return !x.IsForbiddenEntirely(pawn) && x.TryFindRandomCellInRegionUnforbidden(pawn, CellValidator, out _);
        }

        if (!CellFinder.TryFindClosestRegionWith(pawn.GetRegion(), TraverseParms.For(pawn), Validator, 500,
                out var reg))
        {
            return null;
        }

        if (!reg.TryFindRandomCellInRegionUnforbidden(pawn, CellValidator, out var root))
        {
            return null;
        }

        if (!WalkPathFinder.TryFindWalkPath(pawn, root, out var list))
        {
            return null;
        }

        var job = new Job(def.jobDef, list[0])
        {
            targetQueueA = new List<LocalTargetInfo> { list[0] },
            locomotionUrgency = LocomotionUrgency.Walk
        };
        return job;
    }
}

public class JobDriver_EatDirt : JobDriver_Skygaze
{
    public override string GetReport()
    {
        if (Map.gameConditionManager.ConditionIsActive(GameConditionDefOf.Eclipse))
        {
            return "Eating Dirt";
        }

        if (Map.gameConditionManager.ConditionIsActive(GameConditionDefOf.Aurora))
        {
            return "Eating Dirt";
        }

        var num = GenCelestial.CurCelestialSunGlow(Map);
        if (num < 0.1f)
        {
            return "Eating Dirt";
        }

        if (num >= 0.65f)
        {
            return "Eating Dirt";
        }

        if (GenLocalDate.DayPercent(pawn) < 0.5f)
        {
            return "Eating Dirt";
        }

        return "Eating Dirt";
    }
}

public class JoyGiver_WanderMapEdge : JoyGiver
{
    public override Job TryGiveJob(Pawn pawn)
    {
        if (PawnUtility.WillSoonHaveBasicNeed(pawn))
        {
            return null;
        }

        bool CellValidator(IntVec3 x)
        {
            return !PawnUtility.KnownDangerAt(x, pawn.Map, pawn) && !x.GetTerrain(pawn.Map).avoidWander &&
                   x.Standable(pawn.Map) && x.CloseToEdge(pawn.Map, 5);
        }

        bool Validator(Region x)
        {
            return x.Room.PsychologicallyOutdoors && x.TryFindRandomCellInRegionUnforbidden(pawn, CellValidator, out _);
        }

        if (!CellFinder.TryFindClosestRegionWith(pawn.GetRegion(), TraverseParms.For(pawn), Validator, 500,
                out var reg))
        {
            return null;
        }

        if (!reg.TryFindRandomCellInRegionUnforbidden(pawn, CellValidator, out var root))
        {
            return null;
        }

        if (!WalkPathFinder.TryFindWalkPath(pawn, root, out var list))
        {
            return null;
        }

        if (!reg.TryFindRandomCellInRegionUnforbidden(pawn, CellValidator, out var root2))
        {
            return null;
        }

        if (!WalkPathFinder.TryFindWalkPath(pawn, root2, out var list2))
        {
            return null;
        }

        if (!reg.TryFindRandomCellInRegionUnforbidden(pawn, CellValidator, out var root4))
        {
            return null;
        }

        if (!WalkPathFinder.TryFindWalkPath(pawn, root4, out var list4))
        {
            return null;
        }

        if (!reg.TryFindRandomCellInRegionUnforbidden(pawn, CellValidator, out _))
        {
            return null;
        }

        if (!WalkPathFinder.TryFindWalkPath(pawn, root4, out var list5))
        {
            return null;
        }

        var job = new Job(def.jobDef, list[0])
        {
            targetQueueA = new List<LocalTargetInfo>()
        };
        //can't figure out how to get hte pawn to wait so this has them sit for an extra tick. sadly not more than that.
        list[1] = list[0];
        list[2] = list2[0];
        list[3] = list2[0];
        list[4] = list4[0];
        list[5] = list4[0];
        list[6] = list5[0];
        list[7] = list5[0];
        job.targetQueueA.Add(list[0]);
        job.targetQueueA.Add(list[1]);
        job.targetQueueA.Add(list[2]);
        job.targetQueueA.Add(list[3]);
        job.targetQueueA.Add(list[4]);
        job.targetQueueA.Add(list[5]);
        job.targetQueueA.Add(list[6]);
        job.targetQueueA.Add(list[7]);
        /* adding this made htem go back to the start again. its intersting. still nots rue how this translates to wandering only in water
        for (int i = 1; i < list.Count; i++)
        {
            job.targetQueueA.Add(list[i]);
        }
        */
        job.locomotionUrgency = LocomotionUrgency.Sprint;
        return job;
    }
}

public class JobDriver_StareAtCeiling : JobDriver
{
    public override bool TryMakePreToilReservations(bool errorOnFailed)
    {
        return true;
    }

    protected override IEnumerable<Toil> MakeNewToils()
    {
        yield return Toils_Goto.GotoCell(TargetIndex.A, PathEndMode.OnCell);
        var gaze = new Toil
        {
            initAction = delegate { pawn.jobs.posture = PawnPosture.LayingOnGroundFaceUp; },
            defaultCompleteMode = ToilCompleteMode.Delay,
            defaultDuration = job.def.joyDuration
        };
        yield return gaze;
    }
}

public class JobDriver_GoForWalk_Test : JobDriver
{
    public override bool TryMakePreToilReservations(bool errorOnFailed)
    {
        return true;
    }

    protected override IEnumerable<Toil> MakeNewToils()
    {
        var goToil = Toils_Goto.GotoCell(TargetIndex.A, PathEndMode.OnCell);
        yield return goToil;
    }
}

public class JoyGiver_GoFindSun : JoyGiver
{
    public override Job TryGiveJob(Pawn pawn)
    {
        var IsCurrentWeatherClear = "Null";
        if (pawn.Map.weatherManager.curWeather != null)
        {
            IsCurrentWeatherClear = pawn.Map.weatherManager.curWeather.ToString();
        }

        if (IsCurrentWeatherClear != "Clear")
        {
            return null;
        }

        var CurrentCelestialGlow = GenCelestial.CurCelestialSunGlow(pawn.Map);
        if (CurrentCelestialGlow <= 0.4f)
        {
            return null;
        }

        /*
        if (GenLocalDate.DayPercent(pawn.Map) < 0.5f)
        {
            return null;
        }
        */
        if (!JoyUtility.EnjoyableOutsideNow(pawn))
        {
            return null;
        }

        if (PawnUtility.WillSoonHaveBasicNeed(pawn))
        {
            return null;
        }

        bool CellValidator(IntVec3 x)
        {
            return !PawnUtility.KnownDangerAt(x, pawn.Map, pawn) && !x.GetTerrain(pawn.Map).avoidWander &&
                   x.Standable(pawn.Map) && !x.Roofed(pawn.Map);
        }

        bool Validator(Region x)
        {
            return x.Room.PsychologicallyOutdoors && !x.IsForbiddenEntirely(pawn) &&
                   x.TryFindRandomCellInRegionUnforbidden(pawn, CellValidator, out _);
        }

        if (!CellFinder.TryFindClosestRegionWith(pawn.GetRegion(), TraverseParms.For(pawn), Validator, 100,
                out var reg))
        {
            return null;
        }

        if (!reg.TryFindRandomCellInRegionUnforbidden(pawn, CellValidator, out var root))
        {
            return null;
        }

        if (!WalkPathFinder.TryFindWalkPath(pawn, root, out var list))
        {
            return null;
        }

        var job = new Job(def.jobDef, list[0])
        {
            targetQueueA = new List<LocalTargetInfo>()
        };
        for (var i = 1; i < list.Count; i++)
        {
            job.targetQueueA.Add(list[i]);
        }

        job.locomotionUrgency = LocomotionUrgency.Walk;
        return job;
    }
}

public class JobDriver_SunBathe : JobDriver_Skygaze
{
    public override string GetReport()
    {
        if (Map.gameConditionManager.ConditionIsActive(GameConditionDefOf.Eclipse))
        {
            return "Sunbathing";
        }

        if (Map.gameConditionManager.ConditionIsActive(GameConditionDefOf.Aurora))
        {
            return "Sunbathing";
        }

        var num = GenCelestial.CurCelestialSunGlow(Map);
        if (num < 0.1f)
        {
            return "Sunbathing";
        }

        if (num >= 0.65f)
        {
            return "Sunbathing";
        }

        if (GenLocalDate.DayPercent(pawn) < 0.5f)
        {
            return "Sunbathing";
        }

        return "Sunbathing";
    }
}