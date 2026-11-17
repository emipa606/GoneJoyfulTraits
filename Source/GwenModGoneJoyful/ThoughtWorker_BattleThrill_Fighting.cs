using GBTK_DefinitionTypes;
using RimWorld;
using Verse;

namespace GBKT_ThoughtWorkers;

public class ThoughtWorker_BattleThrill_Fighting : ThoughtWorker
{
    protected override ThoughtState CurrentStateInternal(Pawn p)
    {
        var PawnsCurrentJob = p.CurJobDef.ToString();
        var room = p.GetRoom(RegionType.Set_Passable);
        if (!p.Spawned || !p.RaceProps.Humanlike ||
            !p.story.traits.HasTrait(GBTK_DefinitionTypes_Traits.GBKT_BattleThrill) ||
            PawnsCurrentJob is not ("AttackStatic" or "AttackMelee" or "SocialFight"))
        {
            return ThoughtState.Inactive;
        }

        return room == null ? ThoughtState.Inactive : ThoughtState.ActiveAtStage(0);
    }
}