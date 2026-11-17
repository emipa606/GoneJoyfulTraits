using GBTK_DefinitionTypes;
using RimWorld;
using Verse;

namespace GBKT_ThoughtWorkers;

public class ThoughtWorker_GamerInARecRoom : ThoughtWorker
{
    private static readonly RoomRoleDef recRoom = DefDatabase<RoomRoleDef>.GetNamedSilentFail("RecRoom");

    protected override ThoughtState CurrentStateInternal(Pawn p)
    {
        var room = p.GetRoom(RegionType.Set_Passable);
        if (!p.Spawned || !p.RaceProps.Humanlike ||
            !p.story.traits.HasTrait(GBTK_DefinitionTypes_Traits.GBKT_CouchPotato))
        {
            return ThoughtState.Inactive;
        }

        return room.Role != recRoom ? ThoughtState.Inactive : ThoughtState.ActiveAtStage(0);
    }
}