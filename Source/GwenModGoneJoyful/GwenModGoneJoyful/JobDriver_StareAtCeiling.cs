using System.Collections.Generic;
using RimWorld;
using Verse.AI;

namespace GBTK_JobTypes;

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