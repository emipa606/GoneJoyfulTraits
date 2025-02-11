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
        if (!pawn.Spawned)
        {
            return ThoughtState.Inactive;
        }

        if (!pawn.RaceProps.Humanlike)
        {
            return ThoughtState.Inactive;
        }

        if (!pawn.story.traits.HasTrait(GBTK_DefinitionTypes_Traits.GBKT_Homebody))
        {
            return ThoughtState.Inactive;
        }

        if (IsThePawnInThePlayerFaction && IsThePawnInThePlayerHome == false)
        {
            return ThoughtState.Inactive;
        }

        if (IsThePawnInThePlayerFaction == false && IsThePawnInThePlayerHome)
        {
            return ThoughtState.Inactive;
        }

        return ThoughtState.ActiveAtStage(0);
    }
}