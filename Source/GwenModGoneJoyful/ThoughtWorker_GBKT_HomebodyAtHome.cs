using GBTK_DefinitionTypes;
using RimWorld;
using Verse;

namespace GBKT_ThoughtWorkers;

public class ThoughtWorker_GBKT_HomebodyAtHome : ThoughtWorker
{
    protected override ThoughtState CurrentStateInternal(Pawn pawn)
    {
        var IsThePawnInThePlayerFaction = pawn.Faction.IsPlayer;
        var IsThePawnInThePlayerHome = pawn.Map.IsPlayerHome;
        if (!pawn.Spawned || !pawn.RaceProps.Humanlike ||
            !pawn.story.traits.HasTrait(GBTK_DefinitionTypes_Traits.GBKT_Homebody) ||
            IsThePawnInThePlayerFaction && !IsThePawnInThePlayerHome ||
            !IsThePawnInThePlayerFaction && IsThePawnInThePlayerHome)
        {
            return ThoughtState.Inactive;
        }

        return ThoughtState.ActiveAtStage(0);
    }
}