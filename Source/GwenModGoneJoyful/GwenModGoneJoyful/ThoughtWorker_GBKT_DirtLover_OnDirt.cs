using GBTK_DefinitionTypes;
using RimWorld;
using Verse;

namespace GBKT_ThoughtWorkers;

public class ThoughtWorker_GBKT_DirtLover_OnDirt : ThoughtWorker
{
    protected override ThoughtState CurrentStateInternal(Pawn pawn)
    {
        var WhatFilthDoesTileMake = pawn.Position.GetTerrain(pawn.Map).generatedFilth.ToString();
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

        if (WhatFilthDoesTileMake != "Filth_Dirt" || WhatFilthDoesTileMake != "Filth_Sand")
        {
            return ThoughtState.Inactive;
        }

        return ThoughtState.ActiveAtStage(0);
    }
}