using GBTK_DefinitionTypes;
using RimWorld;
using RimWorld.Planet;
using Verse;

namespace GBKT_ThoughtWorkers;

public class ThoughtWorker_GBKT_ExplorerAtHome : ThoughtWorker
{
    protected override ThoughtState CurrentStateInternal(Pawn pawn)
    {
        var IsThePawnInThePlayerFaction = pawn.Faction.IsPlayer;
        var IsThePawnInThePlayerHome = pawn.Map.IsPlayerHome;
        _ = pawn.IsPlayerControlledCaravanMember();

        if (!pawn.Spawned || !pawn.RaceProps.Humanlike ||
            !pawn.story.traits.HasTrait(GBTK_DefinitionTypes_Traits.GBKT_Explorer))
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