using System;
using System.Collections.Generic;
using System.Reflection;
using GBTK_DefinitionTypes;
using RimWorld;
using RimWorld.Planet;
using Verse;

namespace GBKT_Main;

public class GBT_TraitChecker : WorldComponent
{
    private readonly List<BodyPartDef> GBKT_BodyPartDef = new List<BodyPartDef>();

    private readonly TraitDef[]
        GBKT_TraitDef =
            new TraitDef[16]; /*IF the code has aw eird map error it is because this nubmer is too fuckign low*/

    private List<Map> maps;

    public GBT_TraitChecker(World world) : base(world)
    {
        GBKT_BodyPartDef.Add(BodyPartDefOf.Brain);
        GBKT_BodyPartDef.Add(BodyPartDefOf.Body);
        GBKT_TraitDef[0] = GBTK_DefinitionTypes_Traits.GBKT_Aquaphile;
        GBKT_TraitDef[1] = GBTK_DefinitionTypes_Traits.GBKT_CareGiver;
        GBKT_TraitDef[2] = GBTK_DefinitionTypes_Traits.GBKT_SkyGazer;
        GBKT_TraitDef[3] = GBTK_DefinitionTypes_Traits.GBKT_Energetic;
        GBKT_TraitDef[4] = GBTK_DefinitionTypes_Traits.GBKT_Explorer;
        GBKT_TraitDef[5] = GBTK_DefinitionTypes_Traits.GBKT_Homebody;
        GBKT_TraitDef[6] = GBTK_DefinitionTypes_Traits.GBKT_DirtLover;
        GBKT_TraitDef[7] = GBTK_DefinitionTypes_Traits.GBKT_Dreamer;
        GBKT_TraitDef[8] = GBTK_DefinitionTypes_Traits.GBKT_Gamer;
        GBKT_TraitDef[9] = GBTK_DefinitionTypes_Traits.GBKT_Meditative;
        GBKT_TraitDef[10] = GBTK_DefinitionTypes_Traits.GBKT_Socialite;
        GBKT_TraitDef[11] = GBTK_DefinitionTypes_Traits.GBKT_CouchPotato;
        GBKT_TraitDef[12] = GBTK_DefinitionTypes_Traits.GBKT_Foodie;
        GBKT_TraitDef[13] = GBTK_DefinitionTypes_Traits.GBKT_BattleThrill;
        GBKT_TraitDef[14] = GBTK_DefinitionTypes_Traits.GBKT_SunBather;
        GBKT_TraitDef[15] = GBTK_DefinitionTypes_Traits.GBKT_SnowBunny;
    }

    public override void WorldComponentTick()
    {
        maps = Find.Maps;
        foreach (var map in maps)
        {
            var pawns = map.mapPawns.AllPawnsSpawned;
            foreach (var pawn in pawns)
            {
                //Log.Error("this runs");
                foreach (var traitDef in GBKT_TraitDef)
                {
                    var hasTrait = pawn.story?.traits?.HasTrait(traitDef);
                    if (hasTrait != true)
                    {
                        continue;
                    }

                    //SKY GAZER
                    if (traitDef == GBTK_DefinitionTypes_Traits.GBKT_SkyGazer)
                    {
                        var IsPawnRoofed = pawn.Position.Roofed(pawn.Map);
                        var whatIsTheWeather = pawn.Map.weatherManager.curWeather.ToString();
                        var PawnsCurrentJob = "Null";
                        if (pawn.CurJobDef.ToString() != null)
                        {
                            PawnsCurrentJob = pawn.CurJobDef.ToString();
                        }

                        if (PawnsCurrentJob == "Skygaze" || PawnsCurrentJob == "UseTelescope")
                        {
                            var unused = HediffGiverUtility.TryApply(pawn,
                                GBTK_DefinitionTypes_Hediff.GBKT_SkyGazerSeesSky, GBKT_BodyPartDef);
                        }
                    }

                    //CARE GIVER
                    foreach (var VisitedPAwn in pawns)
                    {
                        var PawnsCurrentJob = pawn.CurJobDef.ToString();
                        if (traitDef == GBTK_DefinitionTypes_Traits.GBKT_CareGiver)
                        {
                            if (PawnsCurrentJob == "VisitSickPawn" || PawnsCurrentJob == "TendPatient" ||
                                PawnsCurrentJob == "FeedPatient")
                            {
                                if (pawn.CurJob.targetA == VisitedPAwn)
                                {
                                    var unused = HediffGiverUtility.TryApply(VisitedPAwn,
                                        GBTK_DefinitionTypes_Hediff.GBKT_CareGiverVisited, GBKT_BodyPartDef);
                                    VisitedPAwn.needs.joy.GainJoy(0.00001f, GBTK_DefinitionTypes_JoyDeff.Social);
                                }
                            }
                        }

                        if (PawnsCurrentJob == "TendPatient")
                        {
                            var unused = HediffGiverUtility.TryApply(pawn,
                                GBTK_DefinitionTypes_Hediff.GBKT_CareGiverTendedAPatient, GBKT_BodyPartDef);
                        }
                    }

                    //AQUAPHILE
                    if (traitDef == GBTK_DefinitionTypes_Traits.GBKT_Aquaphile)
                    {
                        var DoesTileExtinguishFire = pawn.Position.GetTerrain(pawn.Map).extinguishesFire;
                        if (DoesTileExtinguishFire)
                        {
                            var unused = HediffGiverUtility.TryApply(pawn,
                                GBTK_DefinitionTypes_Hediff.GBKT_Aquaphile_On_Water_Tile, GBKT_BodyPartDef);
                        }

                        if (DoesTileExtinguishFire)
                        {
                            var unused = HediffGiverUtility.TryApply(pawn,
                                GBTK_DefinitionTypes_Hediff.GBKT_Aquaphile_Is_Wet, GBKT_BodyPartDef);
                        }

                        if (pawn.Map.weatherManager.curWeather.exposedThought != null)
                        {
                            var DoesCurrentWeatherMakePawnWet =
                                pawn.Map.weatherManager.curWeather.exposedThought.ToString();
                            if (DoesCurrentWeatherMakePawnWet == "SoakingWet")
                            {
                                var unused = HediffGiverUtility.TryApply(pawn,
                                    GBTK_DefinitionTypes_Hediff.GBKT_Aquaphile_In_The_Rain, GBKT_BodyPartDef);
                            }
                        }

                        if (pawn.Map.weatherManager.curWeather.exposedThought != null)
                        {
                            var DoesCurrentWeatherMakePawnWet =
                                pawn.Map.weatherManager.curWeather.exposedThought.ToString();
                            if (DoesCurrentWeatherMakePawnWet == "SoakingWet")
                            {
                                var unused = HediffGiverUtility.TryApply(pawn,
                                    GBTK_DefinitionTypes_Hediff.GBKT_Aquaphile_Is_Wet, GBKT_BodyPartDef);
                            }
                        }
                    }

                    //ENERGETIC
                    if (traitDef == GBTK_DefinitionTypes_Traits.GBKT_Energetic)
                    {
                        var PawnsCurrentJob = "Null";
                        if (pawn.CurJobDef.ToString() != null)
                        {
                            PawnsCurrentJob = pawn.CurJobDef.ToString();
                        }

                        if (traitDef == GBTK_DefinitionTypes_Traits.GBKT_Energetic)
                        {
                            var unused = HediffGiverUtility.TryApply(pawn,
                                GBTK_DefinitionTypes_Hediff.GBKT_BaseEnergetic, GBKT_BodyPartDef);
                        }

                        if (PawnsCurrentJob == "GBKT_RunBackAndForth")
                        {
                            var unused = HediffGiverUtility.TryApply(pawn,
                                GBTK_DefinitionTypes_Hediff.GBKT_EnergizedEnergetic, GBKT_BodyPartDef);
                        }
                    }

                    //EXPLORER
                    if (traitDef == GBTK_DefinitionTypes_Traits.GBKT_Explorer)
                    {
                        var IsThePawnInThePlayerFaction = pawn.Faction.IsPlayer;
                        var IsThePawnInThePlayerHome = pawn.Map.IsPlayerHome;
                        if (IsThePawnInThePlayerFaction && IsThePawnInThePlayerHome)
                        {
                            var unused = HediffGiverUtility.TryApply(pawn,
                                GBTK_DefinitionTypes_Hediff.GBKT_ExplorerAtHome, GBKT_BodyPartDef);
                        }

                        if (IsThePawnInThePlayerFaction == false && IsThePawnInThePlayerHome == false)
                        {
                            var unused = HediffGiverUtility.TryApply(pawn,
                                GBTK_DefinitionTypes_Hediff.GBKT_ExplorerAtHome, GBKT_BodyPartDef);
                        }

                        if (IsThePawnInThePlayerFaction && IsThePawnInThePlayerHome == false)
                        {
                            var unused = HediffGiverUtility.TryApply(pawn,
                                GBTK_DefinitionTypes_Hediff.GBKT_ExplorerNotAtHome, GBKT_BodyPartDef);
                        }

                        if (IsThePawnInThePlayerFaction == false && IsThePawnInThePlayerHome)
                        {
                            var unused = HediffGiverUtility.TryApply(pawn,
                                GBTK_DefinitionTypes_Hediff.GBKT_ExplorerNotAtHome, GBKT_BodyPartDef);
                        }
                    }

                    //HOMEBODY
                    if (traitDef == GBTK_DefinitionTypes_Traits.GBKT_Homebody)
                    {
                        var IsThePawnInThePlayerFaction = pawn.Faction.IsPlayer;
                        var IsThePawnInThePlayerHome = pawn.Map.IsPlayerHome;
                        var PawnsCurrentJob = "Null";
                        if (pawn.CurJobDef.ToString() != null)
                        {
                            PawnsCurrentJob = pawn.CurJobDef.ToString();
                        }

                        var GBKT_JoyLevel = pawn.needs.joy.CurLevelPercentage;
                        if (IsThePawnInThePlayerHome && IsThePawnInThePlayerFaction)
                        {
                            var unused = HediffGiverUtility.TryApply(pawn,
                                GBTK_DefinitionTypes_Hediff.GBKT_Homebody_AtHome, GBKT_BodyPartDef);
                        }

                        if (IsThePawnInThePlayerFaction == false && IsThePawnInThePlayerHome == false)
                        {
                            var unused = HediffGiverUtility.TryApply(pawn,
                                GBTK_DefinitionTypes_Hediff.GBKT_Homebody_AtHome, GBKT_BodyPartDef);
                        }

                        if (PawnsCurrentJob == "Clean" && GBKT_JoyLevel < 74.00f)
                        {
                            pawn.needs.joy.GainJoy(0.00001f, GBTK_DefinitionTypes_JoyDeff.Meditative);
                        }
                    }

                    //DIRT LOVER
                    if (traitDef == GBTK_DefinitionTypes_Traits.GBKT_DirtLover)
                    {
                        var IsPawnRoofed = pawn.Position.Roofed(pawn.Map);
                        var WhatFilthDoesTileMake = "Null";
                        if (pawn.Position.GetTerrain(pawn.Map).generatedFilth != null)
                        {
                            WhatFilthDoesTileMake = pawn.Position.GetTerrain(pawn.Map).generatedFilth.ToString();
                        }

                        var PawnsCurrentJob = "Null";
                        if (pawn.CurJobDef.ToString() != null)
                        {
                            PawnsCurrentJob = pawn.CurJobDef.ToString();
                        }

                        var random = new Random();
                        var randomNumber = random.Next(0, 5);
                        if (randomNumber == 5)
                        {
                            if (WhatFilthDoesTileMake == "Filth_Dirt")
                            {
                                pawn.filth.GainFilth(GBTK_DefinitionTypes_ThingDeff.Filth_Dirt);
                            }

                            if (WhatFilthDoesTileMake == "Filth_Sand")
                            {
                                pawn.filth.GainFilth(GBTK_DefinitionTypes_ThingDeff.Filth_Sand);
                            }
                        }

                        if (PawnsCurrentJob == "GBKT_EatDirt")
                        {
                            var unused = HediffGiverUtility.TryApply(pawn,
                                GBTK_DefinitionTypes_Hediff.GBKT_AteSomeDirt, GBKT_BodyPartDef);
                        }

                        if (PawnsCurrentJob == "GBKT_PlaceDirt" && IsPawnRoofed)
                        {
                            pawn.needs.joy.GainJoy(10f, GBTK_DefinitionTypes_JoyDeff.Meditative);
                            pawn.ClearAllReservations();
                            pawn.filth.GainFilth(GBTK_DefinitionTypes_ThingDeff.Filth_Dirt);
                            pawn.filth.GainFilth(GBTK_DefinitionTypes_ThingDeff.Filth_Sand);
                            pawn.filth.GainFilth(GBTK_DefinitionTypes_ThingDeff.Filth_Dirt);
                            pawn.filth.GainFilth(GBTK_DefinitionTypes_ThingDeff.Filth_Sand);
                            pawn.filth.GetType()
                                .GetMethod("TryDropFilth", BindingFlags.Instance | BindingFlags.NonPublic)
                                .Invoke(pawn.filth, null);
                        }
                    }

                    //DREAMER
                    if (traitDef == GBTK_DefinitionTypes_Traits.GBKT_Dreamer)
                    {
                        var PawnsCurrentJob = "Null";
                        if (pawn.CurJobDef.ToString() != null)
                        {
                            PawnsCurrentJob = pawn.CurJobDef.ToString();
                        }

                        var PawnsCurrentJoyLevel = pawn.needs.joy.CurLevelPercentage;
                        var IsPawnInBed = pawn.InBed();
                        //LayDown
                        if (IsPawnInBed || PawnsCurrentJob == "LayDown")
                        {
                            pawn.needs.joy.GainJoy(0.00001f, GBTK_DefinitionTypes_JoyDeff.Meditative);
                        }

                        if (PawnsCurrentJoyLevel is <= 0.80f and > 0.60f)
                        {
                            var unused = HediffGiverUtility.TryApply(pawn,
                                GBTK_DefinitionTypes_Hediff.GBKT_DreamerUnhappyI, GBKT_BodyPartDef);
                        }

                        if (PawnsCurrentJoyLevel is <= 0.60f and > 0.40f)
                        {
                            var unused = HediffGiverUtility.TryApply(pawn,
                                GBTK_DefinitionTypes_Hediff.GBKT_DreamerUnhappyII, GBKT_BodyPartDef);
                        }

                        if (PawnsCurrentJoyLevel is <= 0.40f and > 0.20f)
                        {
                            var unused = HediffGiverUtility.TryApply(pawn,
                                GBTK_DefinitionTypes_Hediff.GBKT_DreamerUnhappyIII, GBKT_BodyPartDef);
                        }

                        if (PawnsCurrentJoyLevel is <= 0.20f and >= 0.0f)
                        {
                            var unused = HediffGiverUtility.TryApply(pawn,
                                GBTK_DefinitionTypes_Hediff.GBKT_DreamerUnhappyIV, GBKT_BodyPartDef);
                        }
                    }

                    //GAMER
                    if (traitDef == GBTK_DefinitionTypes_Traits.GBKT_Gamer)
                    {
                        var PawnsCurrentJoyKind = "Null";
                        if (pawn.CurJobDef.joyKind != null)
                        {
                            PawnsCurrentJoyKind = pawn.CurJobDef.joyKind.ToString();
                        }

                        if (PawnsCurrentJoyKind != "Null")
                        {
                            var unused = HediffGiverUtility.TryApply(pawn,
                                GBTK_DefinitionTypes_Hediff.GBKT_GamerPlayingGames, GBKT_BodyPartDef);
                        }
                    }

                    //MEDITATIVE
                    if (traitDef == GBTK_DefinitionTypes_Traits.GBKT_Meditative)
                    {
                        var PawnsCurrentJoyKind = "Null";
                        if (pawn.CurJobDef.joyKind != null)
                        {
                            PawnsCurrentJoyKind = pawn.CurJobDef.joyKind.ToString();
                        }

                        if (PawnsCurrentJoyKind == "Meditative")
                        {
                            var unused = HediffGiverUtility.TryApply(pawn,
                                GBTK_DefinitionTypes_Hediff.GBKT_RecentlyMeditatied, GBKT_BodyPartDef);
                        }
                    }

                    //Socialite
                    if (traitDef == GBTK_DefinitionTypes_Traits.GBKT_Socialite)
                    {
                        var PawnsCurrentJoyKind = "Null";
                        if (pawn.CurJobDef.joyKind != null)
                        {
                            PawnsCurrentJoyKind = pawn.CurJobDef.joyKind.ToString();
                        }

                        if (PawnsCurrentJoyKind == "Social")
                        {
                            var unused = HediffGiverUtility.TryApply(pawn,
                                GBTK_DefinitionTypes_Hediff.GBKT_RecentlySocialed, GBKT_BodyPartDef);
                        }

                        if (traitDef == GBTK_DefinitionTypes_Traits.GBKT_Socialite)
                        {
                            var unused = HediffGiverUtility.TryApply(pawn,
                                GBTK_DefinitionTypes_Hediff.GBKT_SocailiteBase, GBKT_BodyPartDef);
                        }
                    }

                    //COUCH POTATO
                    if (traitDef == GBTK_DefinitionTypes_Traits.GBKT_CouchPotato)
                    {
                        var PawnsCurrentJob = "Null";
                        if (pawn.CurJobDef.ToString() != null)
                        {
                            PawnsCurrentJob = pawn.CurJobDef.ToString();
                        }

                        if (pawn.CurJobDef.joyKind != null)
                        {
                            PawnsCurrentJob = pawn.CurJobDef.ToString();
                        }

                        if (PawnsCurrentJob == "ViewArt" || PawnsCurrentJob == "WatchTelevision")
                        {
                            var unused = HediffGiverUtility.TryApply(pawn,
                                GBTK_DefinitionTypes_Hediff.GBKT_CouchPotatoViewing, GBKT_BodyPartDef);
                        }
                    }

                    //FOODIE
                    if (traitDef == GBTK_DefinitionTypes_Traits.GBKT_Foodie)
                    {
                        var PawnsCurrentJob = "Null";
                        if (pawn.CurJobDef.ToString() != null)
                        {
                            PawnsCurrentJob = pawn.CurJobDef.ToString();
                        }

                        var PawnsCurrentJoyKind = "Null";
                        if (pawn.CurJobDef.joyKind != null)
                        {
                            PawnsCurrentJoyKind = pawn.CurJobDef.joyKind.ToString();
                        }

                        if (PawnsCurrentJoyKind == "Gluttonous")
                        {
                            var unused = HediffGiverUtility.TryApply(pawn,
                                GBTK_DefinitionTypes_Hediff.GBKT_FoodieAteFood, GBKT_BodyPartDef);
                        }

                        if (PawnsCurrentJob == "Ingest")
                        {
                            pawn.needs.joy.GainJoy(0.00001f, GBTK_DefinitionTypes_JoyDeff.Gluttonous);
                        }
                    }

                    // GBKT_BattleThrill
                    if (traitDef == GBTK_DefinitionTypes_Traits.GBKT_BattleThrill)
                    {
                        var PawnsCurrentJob = "Null";
                        if (pawn.CurJobDef.ToString() != null)
                        {
                            PawnsCurrentJob = pawn.CurJobDef.ToString();
                        }

                        if (PawnsCurrentJob == "AttackStatic" || PawnsCurrentJob == "AttackMelee" ||
                            PawnsCurrentJob == "SocialFight")
                        {
                            pawn.needs.joy.GainJoy(0.00001f, GBTK_DefinitionTypes_JoyDeff.Meditative);
                            var unused = HediffGiverUtility.TryApply(pawn,
                                GBTK_DefinitionTypes_Hediff.GBKT_BattleThrillBattling, GBKT_BodyPartDef);
                        }

                        var PawnsCurrentJoyKind = "Null";
                        if (pawn.CurJobDef.joyKind != null)
                        {
                            PawnsCurrentJoyKind = pawn.CurJobDef.joyKind.ToString();
                        }

                        if (PawnsCurrentJoyKind != "Null")
                        {
                            var unused = HediffGiverUtility.TryApply(pawn,
                                GBTK_DefinitionTypes_Hediff.GBKT_BattleThrillBattling, GBKT_BodyPartDef);
                        }
                    }

                    // SUN BATHER
                    if (traitDef == GBTK_DefinitionTypes_Traits.GBKT_SunBather)
                    {
                        var PawnsCurrentJob = "Null";
                        if (pawn.CurJobDef.ToString() != null)
                        {
                            PawnsCurrentJob = pawn.CurJobDef.ToString();
                        }

                        if (PawnsCurrentJob == "GBKT_SunBathe")
                        {
                            var unused = HediffGiverUtility.TryApply(pawn,
                                GBTK_DefinitionTypes_Hediff.GBKT_SunBather_SoakedUpRays, GBKT_BodyPartDef);
                        }
                    } //

                    //SNOW BUNNY
                    if (traitDef != GBTK_DefinitionTypes_Traits.GBKT_SnowBunny)
                    {
                        continue;
                    }

                    if (!(pawn.Map.weatherManager.curWeather.snowRate > 0.0f))
                    {
                        continue;
                    }

                    var HowMuchSnowIsThere = pawn.Map.weatherManager.curWeather.snowRate;
                    if (HowMuchSnowIsThere > 0.0f)
                    {
                        var unused = HediffGiverUtility.TryApply(pawn,
                            GBTK_DefinitionTypes_Hediff.GBKT_SnowBunny_In_The_Snow, GBKT_BodyPartDef);
                    }
                }
            }
        }
    }
}