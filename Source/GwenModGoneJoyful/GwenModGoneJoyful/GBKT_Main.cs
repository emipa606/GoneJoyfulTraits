using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using RimWorld;
using Verse;
using RimWorld.Planet;

namespace GBKT_Main
{
    public class GBT_TraitChecker : WorldComponent
    {
        private List<BodyPartDef> GBKT_BodyPartDef = new List<BodyPartDef>();
        private TraitDef[] GBKT_TraitDef = new TraitDef[16]; /*IF the code has aw eird map error it is because this nubmer is too fuckign low*/
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
            GBKT_TraitDef[6] = GBTK_DefinitionTypes.GBTK_DefinitionTypes_Traits.GBKT_DirtLover;
            GBKT_TraitDef[7] = GBTK_DefinitionTypes.GBTK_DefinitionTypes_Traits.GBKT_Dreamer;
            GBKT_TraitDef[8] = GBTK_DefinitionTypes.GBTK_DefinitionTypes_Traits.GBKT_Gamer;
            GBKT_TraitDef[9] = GBTK_DefinitionTypes.GBTK_DefinitionTypes_Traits.GBKT_Meditative;
            GBKT_TraitDef[10] = GBTK_DefinitionTypes.GBTK_DefinitionTypes_Traits.GBKT_Socialite;
            GBKT_TraitDef[11] = GBTK_DefinitionTypes.GBTK_DefinitionTypes_Traits.GBKT_CouchPotato;
            GBKT_TraitDef[12] = GBTK_DefinitionTypes.GBTK_DefinitionTypes_Traits.GBKT_Foodie;
            GBKT_TraitDef[13] = GBTK_DefinitionTypes.GBTK_DefinitionTypes_Traits.GBKT_BattleThrill;
            GBKT_TraitDef[14] = GBTK_DefinitionTypes.GBTK_DefinitionTypes_Traits.GBKT_SunBather;
            GBKT_TraitDef[15] = GBTK_DefinitionTypes.GBTK_DefinitionTypes_Traits.GBKT_SnowBunny;
        }

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
                        bool? hasTrait = pawn.story?.traits?.HasTrait(GBKT_TraitDef[i]);
                        if (hasTrait == true)
                        {
                            //SKY GAZER
                            if (GBKT_TraitDef[i] == GBTK_DefinitionTypes.GBTK_DefinitionTypes_Traits.GBKT_SkyGazer)
                            {
                                bool IsPawnRoofed = pawn.Position.Roofed(pawn.Map);
                                string whatIsTheWeather = pawn.Map.weatherManager.curWeather.ToString();
                                string PawnsCurrentJob = pawn.CurJobDef.ToString();
                                if (PawnsCurrentJob == "Skygaze" || PawnsCurrentJob == "UseTelescope")
                                {
                                    bool tryThisHediff = HediffGiverUtility.TryApply(pawn, GBTK_DefinitionTypes.GBTK_DefinitionTypes_Hediff.GBKT_SkyGazerSeesSky, GBKT_BodyPartDef);
                                }
                            }
                            //CARE GIVER
                            foreach (Pawn VisitedPAwn in pawns)
                            {
                                string PawnsCurrentJob = pawn.CurJobDef.ToString();
                                if (GBKT_TraitDef[i] == GBTK_DefinitionTypes.GBTK_DefinitionTypes_Traits.GBKT_CareGiver)
                                {
                                    if (PawnsCurrentJob == "VisitSickPawn" || PawnsCurrentJob == "TendPatient" || PawnsCurrentJob == "FeedPatient")
                                    {
                                        if(pawn.CurJob.targetA == VisitedPAwn)
                                        {
                                            bool tryThisHediff = HediffGiverUtility.TryApply(VisitedPAwn, GBTK_DefinitionTypes.GBTK_DefinitionTypes_Hediff.GBKT_CareGiverVisited, GBKT_BodyPartDef);
                                            VisitedPAwn.needs.joy.GainJoy(0.00001f, GBTK_DefinitionTypes.GBTK_DefinitionTypes_JoyDeff.Social);
                                        }
                                    }
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
                                if (DoesTileExtinguishFire == true)
                                {
                                    bool tryThisHediff = HediffGiverUtility.TryApply(pawn, GBTK_DefinitionTypes.GBTK_DefinitionTypes_Hediff.GBKT_Aquaphile_Is_Wet, GBKT_BodyPartDef);
                                }
                                if (pawn.Map.weatherManager.curWeather.exposedThought != null)
                                {
                                    string DoesCurrentWeatherMakePawnWet = pawn.Map.weatherManager.curWeather.exposedThought.ToString();
                                    if (DoesCurrentWeatherMakePawnWet == "SoakingWet")
                                    {
                                        bool tryThisHediff = HediffGiverUtility.TryApply(pawn, GBTK_DefinitionTypes.GBTK_DefinitionTypes_Hediff.GBKT_Aquaphile_In_The_Rain, GBKT_BodyPartDef);
                                    }
                                    
                                }
                                if (pawn.Map.weatherManager.curWeather.exposedThought != null)
                                {
                                    string DoesCurrentWeatherMakePawnWet = pawn.Map.weatherManager.curWeather.exposedThought.ToString();
                                    if (DoesCurrentWeatherMakePawnWet == "SoakingWet")
                                    {
                                        bool tryThisHediff = HediffGiverUtility.TryApply(pawn, GBTK_DefinitionTypes.GBTK_DefinitionTypes_Hediff.GBKT_Aquaphile_Is_Wet, GBKT_BodyPartDef);
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
                            //EXPLORER
                            if (GBKT_TraitDef[i] == GBTK_DefinitionTypes.GBTK_DefinitionTypes_Traits.GBKT_Explorer)
                            {
                                bool IsThePawnInThePlayerFaction = pawn.Faction.IsPlayer;
                                bool IsThePawnInThePlayerHome = pawn.Map.IsPlayerHome;
                                if (IsThePawnInThePlayerFaction == true && IsThePawnInThePlayerHome == true)
                                {
                                    bool tryThisHediff = HediffGiverUtility.TryApply(pawn, GBTK_DefinitionTypes.GBTK_DefinitionTypes_Hediff.GBKT_ExplorerAtHome, GBKT_BodyPartDef);
                                }
                                if (IsThePawnInThePlayerFaction == false && IsThePawnInThePlayerHome == false)
                                {
                                    bool tryThisHediff = HediffGiverUtility.TryApply(pawn, GBTK_DefinitionTypes.GBTK_DefinitionTypes_Hediff.GBKT_ExplorerAtHome, GBKT_BodyPartDef);
                                }
                                if (IsThePawnInThePlayerFaction == true && IsThePawnInThePlayerHome == false)
                                {
                                    bool tryThisHediff = HediffGiverUtility.TryApply(pawn, GBTK_DefinitionTypes.GBTK_DefinitionTypes_Hediff.GBKT_ExplorerNotAtHome, GBKT_BodyPartDef);
                                }
                                if (IsThePawnInThePlayerFaction == false && IsThePawnInThePlayerHome == true)
                                {
                                    bool tryThisHediff = HediffGiverUtility.TryApply(pawn, GBTK_DefinitionTypes.GBTK_DefinitionTypes_Hediff.GBKT_ExplorerNotAtHome, GBKT_BodyPartDef);
                                }
                            }
                            //HOMEBODY
                            if (GBKT_TraitDef[i] == GBTK_DefinitionTypes.GBTK_DefinitionTypes_Traits.GBKT_Homebody)
                            {
                                bool IsThePawnInThePlayerFaction = pawn.Faction.IsPlayer;
                                bool IsThePawnInThePlayerHome = pawn.Map.IsPlayerHome;
                                string PawnsCurrentJob = pawn.CurJobDef.ToString();
                                float GBKT_JoyLevel = pawn.needs.joy.CurLevelPercentage;
                                if (IsThePawnInThePlayerHome == true && IsThePawnInThePlayerFaction == true)
                                {
                                    bool tryThisHediff = HediffGiverUtility.TryApply(pawn, GBTK_DefinitionTypes.GBTK_DefinitionTypes_Hediff.GBKT_Homebody_AtHome, GBKT_BodyPartDef);
                                }
                                if (IsThePawnInThePlayerFaction == false && IsThePawnInThePlayerHome == false)
                                {
                                    bool tryThisHediff = HediffGiverUtility.TryApply(pawn, GBTK_DefinitionTypes.GBTK_DefinitionTypes_Hediff.GBKT_Homebody_AtHome, GBKT_BodyPartDef);
                                }
                                if (PawnsCurrentJob == "Clean" && GBKT_JoyLevel < 74.00f)
                                {
                                    pawn.needs.joy.GainJoy(0.00001f, GBTK_DefinitionTypes.GBTK_DefinitionTypes_JoyDeff.Meditative);
                                }
                            }
                            //DIRT LOVER
                            if (GBKT_TraitDef[i] == GBTK_DefinitionTypes.GBTK_DefinitionTypes_Traits.GBKT_DirtLover)
                            {
                                bool IsPawnRoofed = pawn.Position.Roofed(pawn.Map);
                                string WhatFilthDoesTileMake = "Null";
                                if(pawn.Position.GetTerrain(pawn.Map).generatedFilth != null)
                                {
                                    WhatFilthDoesTileMake = pawn.Position.GetTerrain(pawn.Map).generatedFilth.ToString();
                                }
                                string PawnsCurrentJob = pawn.CurJobDef.ToString();
                                Random random = new Random();
                                int randomNumber = random.Next(0, 5);
                                if (randomNumber == 5)
                                {
                                    if (WhatFilthDoesTileMake == "Filth_Dirt")
                                    {
                                        pawn.filth.GainFilth(GBTK_DefinitionTypes.GBTK_DefinitionTypes_ThingDeff.Filth_Dirt);
                                    }
                                    if (WhatFilthDoesTileMake == "Filth_Sand")
                                    {
                                        pawn.filth.GainFilth(GBTK_DefinitionTypes.GBTK_DefinitionTypes_ThingDeff.Filth_Sand);
                                    }
                                }
                                if (PawnsCurrentJob == "GBKT_EatDirt")
                                {
                                    bool tryThisHediff = HediffGiverUtility.TryApply(pawn, GBTK_DefinitionTypes.GBTK_DefinitionTypes_Hediff.GBKT_AteSomeDirt, GBKT_BodyPartDef);
                                }
                                if (PawnsCurrentJob == "GBKT_PlaceDirt" && IsPawnRoofed == true)
                                {
                                    pawn.needs.joy.GainJoy(10f, GBTK_DefinitionTypes.GBTK_DefinitionTypes_JoyDeff.Meditative);
                                    pawn.ClearAllReservations(true);
                                    pawn.filth.GainFilth(GBTK_DefinitionTypes.GBTK_DefinitionTypes_ThingDeff.Filth_Dirt);
                                    pawn.filth.GainFilth(GBTK_DefinitionTypes.GBTK_DefinitionTypes_ThingDeff.Filth_Sand);
                                    pawn.filth.GainFilth(GBTK_DefinitionTypes.GBTK_DefinitionTypes_ThingDeff.Filth_Dirt);
                                    pawn.filth.GainFilth(GBTK_DefinitionTypes.GBTK_DefinitionTypes_ThingDeff.Filth_Sand);
                                    pawn.filth.GetType().GetMethod("TryDropFilth", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(pawn.filth, null);
                                }
                            }
                            
                            //DREAMER
                            if (GBKT_TraitDef[i] == GBTK_DefinitionTypes.GBTK_DefinitionTypes_Traits.GBKT_Dreamer)
                            {
                                string PawnsCurrentJob = pawn.CurJobDef.ToString();
                                float PawnsCurrentJoyLevel = pawn.needs.joy.CurLevelPercentage;
                                bool IsPawnInBed = pawn.InBed();
                                //LayDown
                                if (IsPawnInBed == true || PawnsCurrentJob == "LayDown")
                                {
                                    pawn.needs.joy.GainJoy(0.00001f, GBTK_DefinitionTypes.GBTK_DefinitionTypes_JoyDeff.Meditative);
                                }
                                if (PawnsCurrentJoyLevel <= 0.80f && PawnsCurrentJoyLevel > 0.60f)
                                {
                                    bool tryThisHediff = HediffGiverUtility.TryApply(pawn, GBTK_DefinitionTypes.GBTK_DefinitionTypes_Hediff.GBKT_DreamerUnhappyI, GBKT_BodyPartDef);
                                }
                                if (PawnsCurrentJoyLevel <= 0.60f && PawnsCurrentJoyLevel > 0.40f)
                                {
                                    bool tryThisHediff = HediffGiverUtility.TryApply(pawn, GBTK_DefinitionTypes.GBTK_DefinitionTypes_Hediff.GBKT_DreamerUnhappyII, GBKT_BodyPartDef);
                                }
                                if (PawnsCurrentJoyLevel <= 0.40f && PawnsCurrentJoyLevel > 0.20f)
                                {
                                    bool tryThisHediff = HediffGiverUtility.TryApply(pawn, GBTK_DefinitionTypes.GBTK_DefinitionTypes_Hediff.GBKT_DreamerUnhappyIII, GBKT_BodyPartDef);
                                }
                                if (PawnsCurrentJoyLevel <= 0.20f && PawnsCurrentJoyLevel >= 0.0f)
                                {
                                    bool tryThisHediff = HediffGiverUtility.TryApply(pawn, GBTK_DefinitionTypes.GBTK_DefinitionTypes_Hediff.GBKT_DreamerUnhappyIV, GBKT_BodyPartDef);
                                }
                            }
                            //GAMER
                            if (GBKT_TraitDef[i] == GBTK_DefinitionTypes.GBTK_DefinitionTypes_Traits.GBKT_Gamer)
                            {
                                string PawnsCurrentJoyKind = "Null";
                                if (pawn.CurJobDef.joyKind != null)
                                {
                                    PawnsCurrentJoyKind = pawn.CurJobDef.joyKind.ToString();
                                }
                                if (PawnsCurrentJoyKind != "Null")
                                {
                                    bool tryThisHediff = HediffGiverUtility.TryApply(pawn, GBTK_DefinitionTypes.GBTK_DefinitionTypes_Hediff.GBKT_GamerPlayingGames, GBKT_BodyPartDef);
                                }
                            }
                            //MEDITATIVE
                            if (GBKT_TraitDef[i] == GBTK_DefinitionTypes.GBTK_DefinitionTypes_Traits.GBKT_Meditative)
                            {
                                string PawnsCurrentJoyKind = "Null";
                                if (pawn.CurJobDef.joyKind != null)
                                {
                                    PawnsCurrentJoyKind = pawn.CurJobDef.joyKind.ToString();
                                }
                                if (PawnsCurrentJoyKind == "Meditative")
                                {
                                    bool tryThisHediff = HediffGiverUtility.TryApply(pawn, GBTK_DefinitionTypes.GBTK_DefinitionTypes_Hediff.GBKT_RecentlyMeditatied, GBKT_BodyPartDef);
                                }
                            }
                            //Socialite
                            if (GBKT_TraitDef[i] == GBTK_DefinitionTypes.GBTK_DefinitionTypes_Traits.GBKT_Socialite)
                            {
                                string PawnsCurrentJoyKind = "Null";
                                if (pawn.CurJobDef.joyKind != null)
                                {
                                    PawnsCurrentJoyKind = pawn.CurJobDef.joyKind.ToString();
                                }
                                if (PawnsCurrentJoyKind == "Social")
                                {
                                    bool tryThisHediff = HediffGiverUtility.TryApply(pawn, GBTK_DefinitionTypes.GBTK_DefinitionTypes_Hediff.GBKT_RecentlySocialed, GBKT_BodyPartDef);
                                }
                                if (GBKT_TraitDef[i] == GBTK_DefinitionTypes.GBTK_DefinitionTypes_Traits.GBKT_Socialite)
                                {
                                    bool tryThisHediff = HediffGiverUtility.TryApply(pawn, GBTK_DefinitionTypes.GBTK_DefinitionTypes_Hediff.GBKT_SocailiteBase, GBKT_BodyPartDef);
                                }
                            }
                            //COUCH POTATO
                            if (GBKT_TraitDef[i] == GBTK_DefinitionTypes.GBTK_DefinitionTypes_Traits.GBKT_CouchPotato)
                            {
                                string PawnsCurrentJob = pawn.CurJobDef.ToString();
                                if (pawn.CurJobDef.joyKind != null)
                                {
                                    PawnsCurrentJob = pawn.CurJobDef.ToString();
                                }
                                if (PawnsCurrentJob == "ViewArt" || PawnsCurrentJob == "WatchTelevision")
                                {
                                    bool tryThisHediff = HediffGiverUtility.TryApply(pawn, GBTK_DefinitionTypes.GBTK_DefinitionTypes_Hediff.GBKT_CouchPotatoViewing, GBKT_BodyPartDef);
                                }
                            }
                            //FOODIE
                            if (GBKT_TraitDef[i] == GBTK_DefinitionTypes.GBTK_DefinitionTypes_Traits.GBKT_Foodie)
                            {
                                string PawnsCurrentJob = pawn.CurJobDef.ToString();
                                string PawnsCurrentJoyKind = "Null";
                                if (pawn.CurJobDef.joyKind != null)
                                {
                                    PawnsCurrentJoyKind = pawn.CurJobDef.joyKind.ToString();
                                }
                                if (PawnsCurrentJoyKind == "Gluttonous")
                                {
                                    bool tryThisHediff = HediffGiverUtility.TryApply(pawn, GBTK_DefinitionTypes.GBTK_DefinitionTypes_Hediff.GBKT_FoodieAteFood, GBKT_BodyPartDef);
                                }
                                if (PawnsCurrentJob == "Ingest")
                                {
                                    pawn.needs.joy.GainJoy(0.00001f, GBTK_DefinitionTypes.GBTK_DefinitionTypes_JoyDeff.Gluttonous);
                                }
                            }
                            // GBKT_BattleThrill
                            if (GBKT_TraitDef[i] == GBTK_DefinitionTypes.GBTK_DefinitionTypes_Traits.GBKT_BattleThrill)
                            {
                                string PawnsCurrentJob = pawn.CurJobDef.ToString();
                                if (PawnsCurrentJob == "AttackStatic" || PawnsCurrentJob == "AttackMelee" || PawnsCurrentJob == "SocialFight")
                                {
                                    pawn.needs.joy.GainJoy(0.00001f, GBTK_DefinitionTypes.GBTK_DefinitionTypes_JoyDeff.Meditative);
                                    bool tryThisHediff = HediffGiverUtility.TryApply(pawn, GBTK_DefinitionTypes.GBTK_DefinitionTypes_Hediff.GBKT_BattleThrillBattling, GBKT_BodyPartDef);
                                }
                                string PawnsCurrentJoyKind = "Null";
                                if (pawn.CurJobDef.joyKind != null)
                                {
                                    PawnsCurrentJoyKind = pawn.CurJobDef.joyKind.ToString();
                                }
                                if (PawnsCurrentJoyKind != "Null")
                                {
                                    bool tryThisHediff = HediffGiverUtility.TryApply(pawn, GBTK_DefinitionTypes.GBTK_DefinitionTypes_Hediff.GBKT_BattleThrillBattling, GBKT_BodyPartDef);
                                }
                            }
                            // SUN BATHER
                            if (GBKT_TraitDef[i] == GBTK_DefinitionTypes.GBTK_DefinitionTypes_Traits.GBKT_SunBather)
                            {
                                string PawnsCurrentJob = pawn.CurJobDef.ToString();
                                if (PawnsCurrentJob == "GBKT_SunBathe")
                                {
                                    bool tryThisHediff = HediffGiverUtility.TryApply(pawn, GBTK_DefinitionTypes.GBTK_DefinitionTypes_Hediff.GBKT_SunBather_SoakedUpRays, GBKT_BodyPartDef);
                                }
                            }//
                            //SNOW BUNNY
                            if (GBKT_TraitDef[i] == GBTK_DefinitionTypes.GBTK_DefinitionTypes_Traits.GBKT_SnowBunny)
                            {
                                float HowMuchSnowIsThere = 0;
                                if (pawn.Map.weatherManager.curWeather.snowRate > 0.0f)
                                {
                                    HowMuchSnowIsThere = pawn.Map.weatherManager.curWeather.snowRate;
                                    if (HowMuchSnowIsThere > 0.0f)
                                    {
                                        bool tryThisHediff = HediffGiverUtility.TryApply(pawn, GBTK_DefinitionTypes.GBTK_DefinitionTypes_Hediff.GBKT_SnowBunny_In_The_Snow, GBKT_BodyPartDef);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}