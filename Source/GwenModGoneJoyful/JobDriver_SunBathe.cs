using RimWorld;
using Verse;

namespace GBTK_JobTypes;

public class JobDriver_SunBathe : JobDriver_Skygaze
{
    public override string GetReport()
    {
        return "GJT.Sunbathing".Translate();
    }
}