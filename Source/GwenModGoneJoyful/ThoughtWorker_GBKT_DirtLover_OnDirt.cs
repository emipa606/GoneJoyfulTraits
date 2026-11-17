using GBTK_DefinitionTypes;
using RimWorld;
using Verse;

namespace GBKT_ThoughtWorkers;

public class ThoughtWorker_GBKT_DirtLover_OnDirt : ThoughtWorker
{
    protected override ThoughtState CurrentStateInternal(Pawn p)
    {
        var WhatFilthDoesTileMake = p.Position.GetTerrain(p.Map).generatedFilth.ToString();
        if (!p.Spawned || !p.RaceProps.Humanlike ||
            !p.story.traits.HasTrait(GBTK_DefinitionTypes_Traits.GBKT_DirtLover))
        {
            return ThoughtState.Inactive;
        }

        return WhatFilthDoesTileMake is not ("Filth_Dirt" or "Filth_Sand")
            ? ThoughtState.Inactive
            : ThoughtState.ActiveAtStage(0);
    }
}