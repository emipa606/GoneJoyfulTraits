using GBTK_DefinitionTypes;
using RimWorld;
using Verse;

namespace GBKT_ThoughtWorkers;

public class ThoughtWorker_CareGiverInAHospitalRoom : ThoughtWorker
{
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

        if (!pawn.story.traits.HasTrait(GBTK_DefinitionTypes_Traits.GBKT_CareGiver))
        {
            return ThoughtState.Inactive;
        }

        return room.Role != RoomRoleDefOf.Hospital ? ThoughtState.Inactive : ThoughtState.ActiveAtStage(0);
    }
}