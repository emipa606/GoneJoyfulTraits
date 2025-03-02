using GBTK_DefinitionTypes;
using RimWorld;
using Verse;

namespace GBKT_ThoughtWorkers;

public class ThoughtWorker_GamerInARecRoom : ThoughtWorker
{
    private static readonly RoomRoleDef recRoom = DefDatabase<RoomRoleDef>.GetNamedSilentFail("RecRoom");

    protected override ThoughtState CurrentStateInternal(Pawn pawn)
    {
        var room = pawn.GetRoom(RegionType.Set_Passable);
        if (!pawn.Spawned)
        {
            return ThoughtState.Inactive;
        }

        if (!pawn.RaceProps.Humanlike)
        {
            return ThoughtState.Inactive;
        }

        if (!pawn.story.traits.HasTrait(GBTK_DefinitionTypes_Traits.GBKT_CouchPotato))
        {
            return ThoughtState.Inactive;
        }

        return room.Role != recRoom ? ThoughtState.Inactive : ThoughtState.ActiveAtStage(0);
    }
}