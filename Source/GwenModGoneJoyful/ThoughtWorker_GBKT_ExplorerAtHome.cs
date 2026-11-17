using GBTK_DefinitionTypes;
using RimWorld;
using RimWorld.Planet;
using Verse;

namespace GBKT_ThoughtWorkers;

public class ThoughtWorker_GBKT_ExplorerAtHome : ThoughtWorker
{
    protected override ThoughtState CurrentStateInternal(Pawn p)
    {
        var IsThePawnInThePlayerFaction = p.Faction.IsPlayer;
        var IsThePawnInThePlayerHome = p.Map.IsPlayerHome;
        _ = p.IsPlayerControlledCaravanMember();

        if (!p.Spawned || !p.RaceProps.Humanlike ||
            !p.story.traits.HasTrait(GBTK_DefinitionTypes_Traits.GBKT_Explorer))
        {
            return ThoughtState.Inactive;
        }

        if (IsThePawnInThePlayerFaction && IsThePawnInThePlayerHome)
        {
            return ThoughtState.ActiveAtStage(0);
        }

        return IsThePawnInThePlayerFaction
            ? ThoughtState.ActiveAtStage(1)
            : ThoughtState.ActiveAtStage(IsThePawnInThePlayerHome ? 1 : 0);
    }
}