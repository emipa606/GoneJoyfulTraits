using GBTK_DefinitionTypes;
using RimWorld;
using Verse;

namespace GBKT_ThoughtWorkers;

public class ThoughtWorker_DirtLoverInDirt : ThoughtWorker
{
    protected override ThoughtState CurrentStateInternal(Pawn pawn)
    {
        var WhatFilthDoesTileMake = "Null";
        if (pawn.Position.GetTerrain(pawn.Map).generatedFilth != null)
        {
            WhatFilthDoesTileMake = pawn.Position.GetTerrain(pawn.Map).generatedFilth.ToString();
        }

        if (!pawn.Spawned)
        {
            return ThoughtState.Inactive;
        }

        if (!pawn.RaceProps.Humanlike)
        {
            return ThoughtState.Inactive;
        }

        if (!pawn.story.traits.HasTrait(GBTK_DefinitionTypes_Traits.GBKT_DirtLover))
        {
            return ThoughtState.Inactive;
        }

        return WhatFilthDoesTileMake is "Filth_Dirt" or "Filth_Sand"
            ? ThoughtState.ActiveAtStage(0)
            : ThoughtState.Inactive;
    }
}