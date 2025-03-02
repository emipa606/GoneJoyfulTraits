using System.Collections.Generic;
using Verse.AI;

namespace GBTK_JobTypes;

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