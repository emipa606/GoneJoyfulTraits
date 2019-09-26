using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;
using RimWorld.Planet;
using Verse.AI;
using UnityEngine;

namespace GBKT_Defs
{
    public class GBT_TraitChecker : WorldComponent
    {
        private List<BodyPartDef> GBKT_BodyPartDef = new List<BodyPartDef>();
        private TraitDef[] GBKT_TraitDef = new TraitDef[5];
        List<Map> maps;

        public GBT_TraitChecker(World world) : base(world)
        {
            GBKT_BodyPartDef.Add(BodyPartDefOf.Brain);
            GBKT_BodyPartDef.Add(BodyPartDefOf.Body);
            GBKT_TraitDef[0] = GBTK_DefinitionTypes.GBTK_DefinitionTypes_Traits.GBKT_Aquaphile;
            GBKT_TraitDef[1] = GBTK_DefinitionTypes.GBTK_DefinitionTypes_Traits.GBKT_CareGiver;
            GBKT_TraitDef[2] = GBTK_DefinitionTypes.GBTK_DefinitionTypes_Traits.GBKT_SkyGazer;
            GBKT_TraitDef[3] = GBTK_DefinitionTypes.GBTK_DefinitionTypes_Traits.GBKT_Energetic;
            GBKT_TraitDef[4] = GBTK_DefinitionTypes.GBTK_DefinitionTypes_Traits.GBKT_Explorer;
            GBKT_TraitDef[5] = GBTK_DefinitionTypes.GBTK_DefinitionTypes_Traits.GBKT_Homebody;
        }

        //if (pawn.Map.windManager.WindSpeed > 0.1f)
        //if (pawn.Map.weatherManager.curWeather == WeatherDef.Named("SnowHard"))
        //if (pawn.Map.weatherManager.RainRate > 0.10f)
        //if (pawn.Position.IsInPrisonCell(pawn.Map))
        //if (pawn.Position.CloseToEdge(pawn.Map, 20))
        //if (pawn.Position.Roofed(pawn.Map))
        //if (pawn.Downed)
        //if (pawn.Drafted)
        //pawn.IsPrisonerOfColony
        //pawn.IsPrisonerInPrisonCell
        //if (pawn.InMentalState)
        //if (pawn.InAggroMentalState)
        //if (pawn.Map.IsPlayerHome)
        //bool GBKT_MovineWAterTileCheck = pawn.Position.SupportsStructureType(pawn.Map, TerrainAffordanceDefOf.MovingFluid);
        // if (pawn.stances.Staggered) not sure hwo to test for
        //if (pawn.IsFreeColonist) seesm to jsut trigger all the time?
        //if (pawn.HitPoints > 50)
        //if (pawn.Map.TileInfo.swampiness > 0) seems to grant the bonus if the map the pawn is on is a swamp map
        //if (pawn.Map.TileInfo.rainfall > 0) seems to grant the bonus if the map the pawn is on is a swamp map
        // if (ticksUntillNextInspirationCheck < 1)
        //{
        //  bool tryThisHediff = HediffGiverUtility.TryApply(pawn, GBKT_TraitDefOf.GBKT_TestHediff, GBKT_BodyPartDef);
        //        float test = pawn.Map.Biome.plantDensity;
        //}SupportsStructureType(pawn.Map, TerrainAffordanceDefOf.MovingFluid) == true)
        //string test = pawn.Map.Biome.ToString();
        //if (test == "Desert")
        //bool test = pawn.Position.SupportsStructureType(pawn.Map, TerrainAffordanceDefOf.MovingFluid); GrowSoil Diggable SmoothableStone Heavy Medium Light
        //string test = pawn.Map.weatherManager.ToString(); check if clear.
        //string test = Faction.OfPlayer.def.techLevel.ToString(); Undefined Neolithic Animal Medieval Industrial Spacer Ultra Archotech
        // pawn.Destroy(DestroyMode.Vanish); use this to make a geat one go away after 15 days
        //bool test = pawn.Faction.IsPlayer;
        //pawn.Map.weatherManager.curWeather.accuracyMultiplier
        //  pawn.Map.weatherManager.curWeather.exposedThought
        //pawn.Map.weatherManager.curWeather.moveSpeedMultiplier
        //float what = pawn.Map.TileInfo.temperature;
        // bool test = pawn.IsCaravanMember();
        // bool tester = pawn.InCaravanBed();
        //string test = pawn.CurJobDef.ToString();
        //pawn.health.DropBloodFilth();
        //bool esterer = pawn.InBed();
        // string PawnsCurrentJobSpeed = pawn.CurJob.locomotionUrgency.ToString();
        //string PawnsCurrentJob = pawn.CurJobDef.joyKind.ToString();

        public override void WorldComponentTick()
        {
            maps = Find.Maps;
            foreach (Map map in maps)
            {
                List<Pawn> pawns = map.mapPawns.AllPawnsSpawned;
                foreach (Pawn pawn in pawns)
                {
                    //Log.Error("this runs");
                    for (int i = 0; i < GBKT_TraitDef.Length; i++)
                    {
                        //SKY GAZER
                        bool? hasTrait = pawn.story?.traits?.HasTrait(GBKT_TraitDef[i]);
                        if (hasTrait == true)
                        {
                            if (GBKT_TraitDef[i] == GBTK_DefinitionTypes.GBTK_DefinitionTypes_Traits.GBKT_SkyGazer)
                            {
                                bool IsPawnRoofed = pawn.Position.Roofed(pawn.Map);
                                string whatIsTheWeather = pawn.Map.weatherManager.curWeather.ToString();
                                string PawnsCurrentJob = pawn.CurJobDef.ToString();
                                if (PawnsCurrentJob == "Skygaze")
                                {
                                    bool tryThisHediff = HediffGiverUtility.TryApply(pawn, GBTK_DefinitionTypes.GBTK_DefinitionTypes_Hediff.GBKT_SkyGazerSeesSky, GBKT_BodyPartDef);
                                }
                            }
                            //CAREGIVER
                            if (GBKT_TraitDef[i] == GBTK_DefinitionTypes.GBTK_DefinitionTypes_Traits.GBKT_CareGiver)
                            {
                                string PawnsCurrentJob = pawn.CurJobDef.ToString();
                                Room room = pawn.GetRoom(RegionType.Set_Passable);
                                if (room.Role == RoomRoleDefOf.Hospital)
                                {
                                    bool tryThisHediff = HediffGiverUtility.TryApply(pawn, GBTK_DefinitionTypes.GBTK_DefinitionTypes_Hediff.GBKT_CareGiverInHospitalRoom, GBKT_BodyPartDef);
                                }
                                if (PawnsCurrentJob == "TendPatient")
                                {
                                    bool tryThisHediff = HediffGiverUtility.TryApply(pawn, GBTK_DefinitionTypes.GBTK_DefinitionTypes_Hediff.GBKT_CareGiverTendedAPatient, GBKT_BodyPartDef);
                                }
                            }
                            //AQUAPHILE
                            if (GBKT_TraitDef[i] == GBTK_DefinitionTypes.GBTK_DefinitionTypes_Traits.GBKT_Aquaphile)
                            {
                                bool DoesTileExtinguishFire = pawn.Position.GetTerrain(pawn.Map).extinguishesFire;
                                if (DoesTileExtinguishFire == true)
                                {
                                    bool tryThisHediff = HediffGiverUtility.TryApply(pawn, GBTK_DefinitionTypes.GBTK_DefinitionTypes_Hediff.GBKT_Aquaphile_On_Water_Tile, GBKT_BodyPartDef);
                                }
                                if (pawn.Map.weatherManager.curWeather.exposedThought!= null)
                                {
                                    string DoesCurrentWeatherMakePawnWet = pawn.Map.weatherManager.curWeather.exposedThought.ToString();
                                    if (DoesCurrentWeatherMakePawnWet == "SoakingWet")
                                    {
                                        bool tryThisHediff = HediffGiverUtility.TryApply(pawn, GBTK_DefinitionTypes.GBTK_DefinitionTypes_Hediff.GBKT_Aquaphile_In_The_Rain, GBKT_BodyPartDef);
                                    }
                                }
                            }
                            //ENERGETIC
                            if (GBKT_TraitDef[i] == GBTK_DefinitionTypes.GBTK_DefinitionTypes_Traits.GBKT_Energetic)
                            {
                                string PawnsCurrentJob = pawn.CurJobDef.ToString();
                                if (GBKT_TraitDef[i] == GBTK_DefinitionTypes.GBTK_DefinitionTypes_Traits.GBKT_Energetic)
                                {
                                    bool tryThisHediff = HediffGiverUtility.TryApply(pawn, GBTK_DefinitionTypes.GBTK_DefinitionTypes_Hediff.GBKT_BaseEnergetic, GBKT_BodyPartDef);
                                }
                                if (PawnsCurrentJob == "GBKT_RunBackAndForth")
                                {
                                    bool tryThisHediff = HediffGiverUtility.TryApply(pawn, GBTK_DefinitionTypes.GBTK_DefinitionTypes_Hediff.GBKT_EnergizedEnergetic, GBKT_BodyPartDef);
                                }
                            }
                        }
                    }
                }
            }
        }
    }

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
    public class ThoughtWorker_GBKT_DryAquaLover : ThoughtWorker
    {
        protected override ThoughtState CurrentStateInternal(Pawn pawn)
        {
            string DoesCurrentWeatherMakePawnWet = "Null";
            if (pawn.Map.weatherManager.curWeather.exposedThought != null)
            {
                DoesCurrentWeatherMakePawnWet = pawn.Map.weatherManager.curWeather.exposedThought.ToString();
            }
            bool DoesTileExtinguishFire = pawn.Position.GetTerrain(pawn.Map).extinguishesFire;
            if (!pawn.Spawned)
                return ThoughtState.Inactive;
            if (!pawn.RaceProps.Humanlike)
                return ThoughtState.Inactive;
            if (!pawn.story.traits.HasTrait(GBTK_DefinitionTypes.GBTK_DefinitionTypes_Traits.GBKT_Aquaphile))
                return ThoughtState.Inactive;
            if (DoesCurrentWeatherMakePawnWet == "SoakingWet")
                return ThoughtState.Inactive;
            if (DoesTileExtinguishFire == true)
                return ThoughtState.Inactive;
            return ThoughtState.ActiveAtStage(0);
        }
    }
  //  /*
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
            if (IsThePawnInACaravan == true)
                return ThoughtState.ActiveAtStage(1);
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
}