using RimWorld;
using Verse;
using RimWorld.Planet;
using System.Collections.Generic;

namespace GBKT_ThoughtWorkers
{
    public class ThoughtWorker_GBKTSkyGazer : ThoughtWorker
    {
        protected override ThoughtState CurrentStateInternal(Pawn pawn)
        {
            bool IsPawnRoofed = pawn.Position.Roofed(pawn.Map);
            string whatIsTheWeather = pawn.Map.weatherManager.curWeather.ToString();
            if (!pawn.Spawned)
                return ThoughtState.Inactive;
            if (!pawn.RaceProps.Humanlike)
                return ThoughtState.Inactive;
            if (!pawn.story.traits.HasTrait(GBTK_DefinitionTypes.GBTK_DefinitionTypes_Traits.GBKT_SkyGazer))
                return ThoughtState.Inactive;
            if (IsPawnRoofed == true)
                return ThoughtState.Inactive;
            if (whatIsTheWeather != "Clear")
                return ThoughtState.Inactive;
            return ThoughtState.ActiveAtStage(0);
        }
    }
    public class ThoughtWorker_CareGiverInAHospitalRoom : ThoughtWorker
    {
        protected override ThoughtState CurrentStateInternal(Pawn pawn)
        {
            Room room = pawn.GetRoom(RegionType.Set_Passable);
            if (!pawn.Spawned)
                return ThoughtState.Inactive;
            if (!pawn.RaceProps.Humanlike)
                return ThoughtState.Inactive;
            if (!pawn.story.traits.HasTrait(GBTK_DefinitionTypes.GBTK_DefinitionTypes_Traits.GBKT_CareGiver))
                return ThoughtState.Inactive;
            if (room.Role != RoomRoleDefOf.Hospital)
                return ThoughtState.Inactive;
            if (room == null)
                return ThoughtState.Inactive;
            return ThoughtState.ActiveAtStage(0);
        }
    }
    public class ThoughtWorker_GBKT_ExplorerAtHome : ThoughtWorker
    {
        protected override ThoughtState CurrentStateInternal(Pawn pawn)
        {
            bool IsThePawnInThePlayerFaction = pawn.Faction.IsPlayer;
            bool IsThePawnInThePlayerHome = pawn.Map.IsPlayerHome;
            bool IsThePawnInACaravan = pawn.IsPlayerControlledCaravanMember(); /*For some reason this doesn't detect so no bonuses can eb applied while traveling in a caravan.*/

            if (!pawn.Spawned)
                return ThoughtState.Inactive;
            if (!pawn.RaceProps.Humanlike)
                return ThoughtState.Inactive;
            if (!pawn.story.traits.HasTrait(GBTK_DefinitionTypes.GBTK_DefinitionTypes_Traits.GBKT_Explorer))
                return ThoughtState.Inactive;
            if (IsThePawnInThePlayerFaction == true && IsThePawnInThePlayerHome == true)
                return ThoughtState.ActiveAtStage(0);
            if (IsThePawnInThePlayerFaction == true && IsThePawnInThePlayerHome == false)
                return ThoughtState.ActiveAtStage(1);
            if (IsThePawnInThePlayerFaction == false && IsThePawnInThePlayerHome == true)
                return ThoughtState.ActiveAtStage(1);
            if (IsThePawnInThePlayerFaction == false && IsThePawnInThePlayerHome == false)
                return ThoughtState.ActiveAtStage(0);
            return ThoughtState.ActiveAtStage(0);
        }
    }
    //    */
    public class ThoughtWorker_GBKT_HomebodyAtHome : ThoughtWorker
    {
        protected override ThoughtState CurrentStateInternal(Pawn pawn)
        {
            bool IsThePawnInThePlayerFaction = pawn.Faction.IsPlayer;
            bool IsThePawnInThePlayerHome = pawn.Map.IsPlayerHome;
            if (!pawn.Spawned)
                return ThoughtState.Inactive;
            if (!pawn.RaceProps.Humanlike)
                return ThoughtState.Inactive;
            if (!pawn.story.traits.HasTrait(GBTK_DefinitionTypes.GBTK_DefinitionTypes_Traits.GBKT_Homebody))
                return ThoughtState.Inactive;
            if (IsThePawnInThePlayerFaction == true && IsThePawnInThePlayerHome == false)
                return ThoughtState.Inactive;
            if (IsThePawnInThePlayerFaction == false && IsThePawnInThePlayerHome == true)
                return ThoughtState.Inactive;
            if (IsThePawnInThePlayerFaction == true && IsThePawnInThePlayerHome == true)
                return ThoughtState.ActiveAtStage(0);
            if (IsThePawnInThePlayerFaction == false && IsThePawnInThePlayerHome == false)
                return ThoughtState.ActiveAtStage(0);
            return ThoughtState.ActiveAtStage(0);
        }
    }
    public class ThoughtWorker_GBKT_DirtLover_OnDirt : ThoughtWorker
    {
        protected override ThoughtState CurrentStateInternal(Pawn pawn)
        {
            string WhatFilthDoesTileMake = pawn.Position.GetTerrain(pawn.Map).generatedFilth.ToString();
            if (!pawn.Spawned)
                return ThoughtState.Inactive;
            if (!pawn.RaceProps.Humanlike)
                return ThoughtState.Inactive;
            if (!pawn.story.traits.HasTrait(GBTK_DefinitionTypes.GBTK_DefinitionTypes_Traits.GBKT_DirtLover))
                return ThoughtState.Inactive;
            if (WhatFilthDoesTileMake != "Filth_Dirt" || WhatFilthDoesTileMake != "Filth_Sand")
                return ThoughtState.Inactive;
            return ThoughtState.ActiveAtStage(0);
        }
    }
    public class ThoughtWorker_GamerInARecRoom : ThoughtWorker
    {
        protected override ThoughtState CurrentStateInternal(Pawn pawn)
        {
            Room room = pawn.GetRoom(RegionType.Set_Passable);
            if (!pawn.Spawned)
                return ThoughtState.Inactive;
            if (!pawn.RaceProps.Humanlike)
                return ThoughtState.Inactive;
            if (!pawn.story.traits.HasTrait(GBTK_DefinitionTypes.GBTK_DefinitionTypes_Traits.GBKT_CouchPotato))
                return ThoughtState.Inactive;
            if (room.Role != RoomRoleDefOf.RecRoom)
                return ThoughtState.Inactive;
            if (room == null)
                return ThoughtState.Inactive;
            return ThoughtState.ActiveAtStage(0);
        }
    }
    public class ThoughtWorker_DirtLoverInDirt : ThoughtWorker
    {
        protected override ThoughtState CurrentStateInternal(Pawn pawn)
        {
            string WhatFilthDoesTileMake = "Null";
            if (pawn.Position.GetTerrain(pawn.Map).generatedFilth != null)
            {
                WhatFilthDoesTileMake = pawn.Position.GetTerrain(pawn.Map).generatedFilth.ToString();
            }
            if (!pawn.Spawned)
                return ThoughtState.Inactive;
            if (!pawn.RaceProps.Humanlike)
                return ThoughtState.Inactive;
            if (!pawn.story.traits.HasTrait(GBTK_DefinitionTypes.GBTK_DefinitionTypes_Traits.GBKT_DirtLover))
                return ThoughtState.Inactive;
            if (WhatFilthDoesTileMake == "Filth_Dirt" || WhatFilthDoesTileMake == "Filth_Sand")
                return ThoughtState.ActiveAtStage(0);
            return ThoughtState.Inactive;
        }
    }
    public class ThoughtWorker_Foodie_InADinningRoom : ThoughtWorker
    {
        protected override ThoughtState CurrentStateInternal(Pawn pawn)
        {
            Room room = pawn.GetRoom(RegionType.Set_Passable);
            if (!pawn.Spawned)
                return ThoughtState.Inactive;
            if (!pawn.RaceProps.Humanlike)
                return ThoughtState.Inactive;
            if (!pawn.story.traits.HasTrait(GBTK_DefinitionTypes.GBTK_DefinitionTypes_Traits.GBKT_Foodie))
                return ThoughtState.Inactive;
            if (room.Role != RoomRoleDefOf.DiningRoom)
                return ThoughtState.Inactive;
            if (room == null)
                return ThoughtState.Inactive;
            return ThoughtState.ActiveAtStage(0);
        }
    }
    public class ThoughtWorker_BattleThrill_Fighting : ThoughtWorker
    {
        protected override ThoughtState CurrentStateInternal(Pawn pawn)
        {
            string PawnsCurrentJob = pawn.CurJobDef.ToString();
            Room room = pawn.GetRoom(RegionType.Set_Passable);
            if (!pawn.Spawned)
                return ThoughtState.Inactive;
            if (!pawn.RaceProps.Humanlike)
                return ThoughtState.Inactive;
            if (!pawn.story.traits.HasTrait(GBTK_DefinitionTypes.GBTK_DefinitionTypes_Traits.GBKT_BattleThrill))
                return ThoughtState.Inactive;
            if (PawnsCurrentJob != "AttackStatic" || PawnsCurrentJob != "AttackMelee" || PawnsCurrentJob != "SocialFight")
                return ThoughtState.Inactive;
            if (room == null)
                return ThoughtState.Inactive;
            return ThoughtState.ActiveAtStage(0);
        }
    }
}
