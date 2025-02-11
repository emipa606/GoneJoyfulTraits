using RimWorld;
using Verse;
using Verse.AI;

namespace GBTK_JobTypes;

//pawn likes to take non-watery path. not sure how to change it. at least with this method it seeks out new water cells
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
            targetQueueA = []
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

        bool CellValidator(IntVec3 x)
        {
            return x.GetTerrain(pawn.Map).extinguishesFire;
        }

        bool Validator(Region x)
        {
            return x.TryFindRandomCellInRegionUnforbidden(pawn, CellValidator, out _);
        }
    }
}