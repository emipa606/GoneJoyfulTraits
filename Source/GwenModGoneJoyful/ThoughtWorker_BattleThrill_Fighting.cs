using GBTK_DefinitionTypes;
using RimWorld;
using Verse;

namespace GBKT_ThoughtWorkers;

public class ThoughtWorker_BattleThrill_Fighting : ThoughtWorker
{
    protected override ThoughtState CurrentStateInternal(Pawn pawn)
    {
        var PawnsCurrentJob = pawn.CurJobDef.ToString();
        var room = pawn.GetRoom(RegionType.Set_Passable);
        if (!pawn.Spawned)
        {
            return ThoughtState.Inactive;
        }

        if (!pawn.RaceProps.Humanlike)
        {
            return ThoughtState.Inactive;
        }

        if (!pawn.story.traits.HasTrait(GBTK_DefinitionTypes_Traits.GBKT_BattleThrill))
        {
            return ThoughtState.Inactive;
        }

        if (PawnsCurrentJob is not ("AttackStatic" or "AttackMelee" or "SocialFight"))
        {
            return ThoughtState.Inactive;
        }

        return room == null ? ThoughtState.Inactive : ThoughtState.ActiveAtStage(0);
    }
}