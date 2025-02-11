using RimWorld;
using Verse;
using Verse.AI;

namespace GBTK_JobTypes;

public class JoyGiver_EatSomeDirt : JoyGiver
{
    private static readonly TerrainAffordanceDef diggable =
        DefDatabase<TerrainAffordanceDef>.GetNamedSilentFail("Diggable");

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
            targetQueueA = [list[0]],
            locomotionUrgency = LocomotionUrgency.Walk
        };
        return job;

        bool CellValidator(IntVec3 x)
        {
            return !PawnUtility.KnownDangerAt(x, pawn.Map, pawn) && !x.GetTerrain(pawn.Map).avoidWander &&
                   x.Standable(pawn.Map) && x.SupportsStructureType(pawn.Map, diggable);
        }

        bool Validator(Region x)
        {
            return !x.IsForbiddenEntirely(pawn) && x.TryFindRandomCellInRegionUnforbidden(pawn, CellValidator, out _);
        }
    }
}