using RimWorld;
using Verse;
using Verse.AI;

namespace GBTK_JobTypes;

public class JoyGiver_GoForWalk_InRain : JoyGiver
{
    public override Job TryGiveJob(Pawn pawn)
    {
        var DoesCurrentWeatherMakePawnWet = "Null";
        if (pawn.Map.weatherManager.curWeather.weatherThought != null)
        {
            DoesCurrentWeatherMakePawnWet = pawn.Map.weatherManager.curWeather.weatherThought.ToString();
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
            targetQueueA = []
        };
        for (var i = 1; i < list.Count; i++)
        {
            job.targetQueueA.Add(list[i]);
        }

        job.locomotionUrgency = LocomotionUrgency.Walk;
        return job;

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
    }
}