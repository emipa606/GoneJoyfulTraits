using RimWorld;
using Verse;

namespace GBTK_JobTypes;

public class JobDriver_PlaceDirt : JobDriver_Skygaze
{
    public override string GetReport()
    {
        return "GJT.PlacingDirt".Translate();
    }
}