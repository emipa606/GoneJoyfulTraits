using GBTK_DefinitionTypes;
using RimWorld;
using Verse;

namespace GBKT_ThoughtWorkers;

public class ThoughtWorker_GBKT_HomebodyAtHome : ThoughtWorker
{
    protected override ThoughtState CurrentStateInternal(Pawn p)
    {
        var IsThePawnInThePlayerFaction = p.Faction.IsPlayer;
        var IsThePawnInThePlayerHome = p.Map.IsPlayerHome;
        if (!p.Spawned || !p.RaceProps.Humanlike ||
            !p.story.traits.HasTrait(GBTK_DefinitionTypes_Traits.GBKT_Homebody) ||
            IsThePawnInThePlayerFaction && !IsThePawnInThePlayerHome ||
            !IsThePawnInThePlayerFaction && IsThePawnInThePlayerHome)
        {
            return ThoughtState.Inactive;
        }

        return ThoughtState.ActiveAtStage(0);
    }
}