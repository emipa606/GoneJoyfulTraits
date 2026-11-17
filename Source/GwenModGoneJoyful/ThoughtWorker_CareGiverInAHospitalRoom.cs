using GBTK_DefinitionTypes;
using RimWorld;
using Verse;

namespace GBKT_ThoughtWorkers;

public class ThoughtWorker_CareGiverInAHospitalRoom : ThoughtWorker
{
    protected override ThoughtState CurrentStateInternal(Pawn p)
    {
        var room = p.GetRoom(RegionType.Set_Passable);
        if (!p.Spawned || !p.RaceProps.Humanlike ||
            !p.story.traits.HasTrait(GBTK_DefinitionTypes_Traits.GBKT_CareGiver))
        {
            return ThoughtState.Inactive;
        }

        return room.Role != RoomRoleDefOf.Hospital ? ThoughtState.Inactive : ThoughtState.ActiveAtStage(0);
    }
}