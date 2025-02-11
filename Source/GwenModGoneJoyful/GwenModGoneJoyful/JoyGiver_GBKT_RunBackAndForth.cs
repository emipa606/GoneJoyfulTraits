using RimWorld;
using Verse;
using Verse.AI;

namespace GBTK_JobTypes;

public class JoyGiver_GBKT_RunBackAndForth : JoyGiver
{
    public override Job TryGiveJob(Pawn pawn)
    {
        if (PawnUtility.WillSoonHaveBasicNeed(pawn))
        {
            return null;
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
            targetQueueA = []
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

        bool CellValidator(IntVec3 x)
        {
            return !PawnUtility.KnownDangerAt(x, pawn.Map, pawn) && !x.GetTerrain(pawn.Map).avoidWander &&
                   x.Standable(pawn.Map) && !x.Roofed(pawn.Map);
        }

        bool Validator(Region x)
        {
            return x.TryFindRandomCellInRegionUnforbidden(pawn, CellValidator, out _);
        }
    }
}