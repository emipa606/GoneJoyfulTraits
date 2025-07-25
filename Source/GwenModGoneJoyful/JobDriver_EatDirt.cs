using RimWorld;
using Verse;

namespace GBTK_JobTypes;

public class JobDriver_EatDirt : JobDriver_Skygaze
{
    public override string GetReport()
    {
        return "GJT.EatingDirt".Translate();
    }
}