using GBTK_DefinitionTypes;
using RimWorld;
using Verse;

namespace GBKT_ThoughtWorkers;

public class ThoughtWorker_DirtLoverInDirt : ThoughtWorker
{
    protected override ThoughtState CurrentStateInternal(Pawn p)
    {
        var WhatFilthDoesTileMake = "Null";
        if (p.Position.GetTerrain(p.Map).generatedFilth != null)
        {
            WhatFilthDoesTileMake = p.Position.GetTerrain(p.Map).generatedFilth.ToString();
        }

        if (!p.Spawned || !p.RaceProps.Humanlike ||
            !p.story.traits.HasTrait(GBTK_DefinitionTypes_Traits.GBKT_DirtLover))
        {
            return ThoughtState.Inactive;
        }

        return WhatFilthDoesTileMake is "Filth_Dirt" or "Filth_Sand"
            ? ThoughtState.ActiveAtStage(0)
            : ThoughtState.Inactive;
    }
}