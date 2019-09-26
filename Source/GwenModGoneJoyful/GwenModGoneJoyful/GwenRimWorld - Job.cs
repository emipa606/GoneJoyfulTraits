using System;
using System.Collections.Generic;
using RimWorld;
using Verse;
using Verse.AI;

namespace GBTK_JobTypes
{
    //pawn likes to take non watery path. not sure how to change it. at least with this method it seeks out new water cells
    public class JoyGiver_EnjoyWaterTiles : JoyGiver
    {

        // Token: 0x06000568 RID: 1384 RVA: 0x00035040 File Offset: 0x00033440
        public override Job TryGiveJob(Pawn pawn)
        {
            
            if (PawnUtility.WillSoonHaveBasicNeed(pawn))
            {
                return null;
            }
            if (!JoyUtility.EnjoyableOutsideNow(pawn, null))
            {
                return null;
            }
            Predicate<IntVec3> cellValidator = (IntVec3 x) => x.GetTerrain(pawn.Map).extinguishesFire;
            Predicate<Region> validator = delegate (Region x)
            {
                IntVec3 intVec;
                return x.TryFindRandomCellInRegionUnforbidden(pawn, cellValidator, out intVec);
            };
            Region reg;
            if (!CellFinder.TryFindClosestRegionWith(pawn.GetRegion(RegionType.Set_Passable), TraverseParms.For(pawn, Danger.Deadly, TraverseMode.ByPawn, false), validator, 500, out reg, RegionType.Set_Passable))
            {
                return null;
            }
            IntVec3 root;
            if (!reg.TryFindRandomCellInRegionUnforbidden(pawn, cellValidator, out root))
            {
                return null;
            }
           
            List<IntVec3> list;
            if (!WalkPathFinder.TryFindWalkPath(pawn, root, out list))
            {
                return null;
            }
            IntVec3 root2;
            if (!reg.TryFindRandomCellInRegionUnforbidden(pawn, cellValidator, out root2))
            {
                return null;
            }
            List<IntVec3> list2;
            if (!WalkPathFinder.TryFindWalkPath(pawn, root2, out list2))
            {
                return null;
            }
            IntVec3 root4;
            if (!reg.TryFindRandomCellInRegionUnforbidden(pawn, cellValidator, out root4))
            {
                return null;
            }
            List<IntVec3> list4;
            if (!WalkPathFinder.TryFindWalkPath(pawn, root4, out list4))
            {
                return null;
            }
            IntVec3 root5;
            if (!reg.TryFindRandomCellInRegionUnforbidden(pawn, cellValidator, out root5))
            {
                return null;
            }
            List<IntVec3> list5;
            if (!WalkPathFinder.TryFindWalkPath(pawn, root4, out list5))
            {
                return null;
            }
            Job job = new Job(this.def.jobDef, list[0]);
            job.targetQueueA = new List<LocalTargetInfo>();
            //can't figure out how to get hte pawn to wait so this has them sit for an extra tick. sadly not more than that.
            list[1] = list[0];
            list[2] = list2[0];
            list[3] = list2[0];
            list[4] = list4[0];
            list[5] = list4[0];
            list[6] = list5[0];
            list[7] = list5[0];
            job.targetQueueA.Add(list[0]);
            job.targetQueueA.Add(list[1]);
            job.targetQueueA.Add(list[2]);
            job.targetQueueA.Add(list[3]);
            job.targetQueueA.Add(list[4]);
            job.targetQueueA.Add(list[5]);
            job.targetQueueA.Add(list[6]);
            job.targetQueueA.Add(list[7]);
            /* adding this made htem go back to the start again. its intersting. still nots rue how this translates to wandering only in water
            for (int i = 1; i < list.Count; i++)
            {
                job.targetQueueA.Add(list[i]);
            }
            */
            job.locomotionUrgency = LocomotionUrgency.Amble;
            return job;
        }
    }
    public class JoyGiver_GoForWalk_InRain : JoyGiver
    {
        // Token: 0x06000568 RID: 1384 RVA: 0x00035040 File Offset: 0x00033440
        public override Job TryGiveJob(Pawn pawn)
        {
            string DoesCurrentWeatherMakePawnWet = "Null";
            if (pawn.Map.weatherManager.curWeather.exposedThought != null)
            {
                DoesCurrentWeatherMakePawnWet = pawn.Map.weatherManager.curWeather.exposedThought.ToString();
            }
            if (DoesCurrentWeatherMakePawnWet != "SoakingWet")
            {
                return null;
            }
            if (!JoyUtility.EnjoyableOutsideNow(pawn, null))
            {
                return null;
            }
            if (PawnUtility.WillSoonHaveBasicNeed(pawn))
            {
                return null;
            }
            Predicate<IntVec3> cellValidator = (IntVec3 x) => !PawnUtility.KnownDangerAt(x, pawn.Map, pawn) && !x.GetTerrain(pawn.Map).avoidWander && x.Standable(pawn.Map) && !x.Roofed(pawn.Map);
            Predicate<Region> validator = delegate (Region x)
            {
                IntVec3 intVec;
                return x.Room.PsychologicallyOutdoors && !x.IsForbiddenEntirely(pawn) && x.TryFindRandomCellInRegionUnforbidden(pawn, cellValidator, out intVec);
            };
            Region reg;
            if (!CellFinder.TryFindClosestRegionWith(pawn.GetRegion(RegionType.Set_Passable), TraverseParms.For(pawn, Danger.Deadly, TraverseMode.ByPawn, false), validator, 100, out reg, RegionType.Set_Passable))
            {
                return null;
            }
            IntVec3 root;
            if (!reg.TryFindRandomCellInRegionUnforbidden(pawn, cellValidator, out root))
            {
                return null;
            }
            List<IntVec3> list;
            if (!WalkPathFinder.TryFindWalkPath(pawn, root, out list))
            {
                return null;
            }
            Job job = new Job(this.def.jobDef, list[0]);
            job.targetQueueA = new List<LocalTargetInfo>();
            for (int i = 1; i < list.Count; i++)
            {
                job.targetQueueA.Add(list[i]);
            }
            job.locomotionUrgency = LocomotionUrgency.Walk;
            return job;
        }
    }
    public class JoyGiver_GoForWalk_InSnow : JoyGiver
    {
        // Token: 0x06000568 RID: 1384 RVA: 0x00035040 File Offset: 0x00033440
        public override Job TryGiveJob(Pawn pawn)
        { 
            float HowMuchSnowIsThere = 0;
            if (pawn.Map.weatherManager.curWeather.snowRate > 0.0f)
            {
                HowMuchSnowIsThere = pawn.Map.weatherManager.curWeather.snowRate;
            }
            if (HowMuchSnowIsThere < 0.1f)
            {
                return null;
            }
            if (!JoyUtility.EnjoyableOutsideNow(pawn, null))
            {
                return null;
            }
            if (PawnUtility.WillSoonHaveBasicNeed(pawn))
            {
                return null;
            }
            Predicate<IntVec3> cellValidator = (IntVec3 x) => !PawnUtility.KnownDangerAt(x, pawn.Map, pawn) && !x.GetTerrain(pawn.Map).avoidWander && x.Standable(pawn.Map) && !x.Roofed(pawn.Map);
            Predicate<Region> validator = delegate (Region x)
            {
                IntVec3 intVec;
                return x.Room.PsychologicallyOutdoors && !x.IsForbiddenEntirely(pawn) && x.TryFindRandomCellInRegionUnforbidden(pawn, cellValidator, out intVec);
            };
            Region reg;
            if (!CellFinder.TryFindClosestRegionWith(pawn.GetRegion(RegionType.Set_Passable), TraverseParms.For(pawn, Danger.Deadly, TraverseMode.ByPawn, false), validator, 100, out reg, RegionType.Set_Passable))
            {
                return null;
            }
            IntVec3 root;
            if (!reg.TryFindRandomCellInRegionUnforbidden(pawn, cellValidator, out root))
            {
                return null;
            }
            List<IntVec3> list;
            if (!WalkPathFinder.TryFindWalkPath(pawn, root, out list))
            {
                return null;
            }
            Job job = new Job(this.def.jobDef, list[0]);
            job.targetQueueA = new List<LocalTargetInfo>();
            for (int i = 1; i < list.Count; i++)
            {
                job.targetQueueA.Add(list[i]);
            }
            job.locomotionUrgency = LocomotionUrgency.Walk;
            return job;
        }
    }
    public class JoyGiver_GBKT_RunBackAndForth : JoyGiver
    {

        // Token: 0x06000568 RID: 1384 RVA: 0x00035040 File Offset: 0x00033440
        public override Job TryGiveJob(Pawn pawn)
        {

            if (PawnUtility.WillSoonHaveBasicNeed(pawn))
            {
                return null;
            }
            Predicate<IntVec3> cellValidator = (IntVec3 x) => !PawnUtility.KnownDangerAt(x, pawn.Map, pawn) && !x.GetTerrain(pawn.Map).avoidWander && x.Standable(pawn.Map) && !x.Roofed(pawn.Map);
            Predicate<Region> validator = delegate (Region x)
            {
                IntVec3 intVec;
                return x.TryFindRandomCellInRegionUnforbidden(pawn, cellValidator, out intVec);
            };
            Region reg;
            if (!CellFinder.TryFindClosestRegionWith(pawn.GetRegion(RegionType.Set_Passable), TraverseParms.For(pawn, Danger.Deadly, TraverseMode.ByPawn, false), validator, 500, out reg, RegionType.Set_Passable))
            {
                return null;
            }
            IntVec3 root;
            if (!reg.TryFindRandomCellInRegionUnforbidden(pawn, cellValidator, out root))
            {
                return null;
            }
            List<IntVec3> list;
            if (!WalkPathFinder.TryFindWalkPath(pawn, root, out list))
            {
                return null;
            }
            Job job = new Job(this.def.jobDef, list[0]);
            job.targetQueueA = new List<LocalTargetInfo>();
            //can't figure out how to get hte pawn to wait so this has them sit for an extra tick. sadly not more than that.
            //list[1] = list[1];
            list[1] = list[2];
            //list[2] = list[0];
            list[3] = list[2];
            list[4] = list[0];
            list[5] = list[2];
            list[6] = list[0];
            list[7] = list[2];
            list[8] = list[0];
            list[2] = list[0];
            job.targetQueueA.Add(list[0]);
            job.targetQueueA.Add(list[1]);
            job.targetQueueA.Add(list[2]);
            job.targetQueueA.Add(list[3]);
            job.targetQueueA.Add(list[4]);
            job.targetQueueA.Add(list[5]);
            job.targetQueueA.Add(list[6]);
            job.targetQueueA.Add(list[7]);
            job.targetQueueA.Add(list[8]);
            /* adding this made htem go back to the start again. its intersting. still nots rue how this translates to wandering only in water
            for (int i = 1; i < list.Count; i++)
            {
                job.targetQueueA.Add(list[i]);
            }
            */
            job.locomotionUrgency = LocomotionUrgency.Sprint;
            return job;
        }
    }
    public class JoyGiver_PlaceSomeDirt : JoyGiver
    {
        public override Job TryGiveJob(Pawn pawn)
        {

            if (PawnUtility.WillSoonHaveBasicNeed(pawn))
            {
                return null;
            }
            Predicate<IntVec3> cellValidator = (IntVec3 x) => !PawnUtility.KnownDangerAt(x, pawn.Map, pawn) && !x.GetTerrain(pawn.Map).avoidWander && x.Standable(pawn.Map) && x.Roofed(pawn.Map);
            Predicate<Region> validator = delegate (Region x)
            {
                IntVec3 intVec;
                return !x.IsForbiddenEntirely(pawn) && x.TryFindRandomCellInRegionUnforbidden(pawn, cellValidator, out intVec);
            };
            Region reg;
            if (!CellFinder.TryFindClosestRegionWith(pawn.GetRegion(RegionType.Set_Passable), TraverseParms.For(pawn, Danger.Deadly, TraverseMode.ByPawn, false), validator, 500, out reg, RegionType.Set_Passable))
            {
                return null;
            }
            IntVec3 root;
            if (!reg.TryFindRandomCellInRegionUnforbidden(pawn, cellValidator, out root))
            {
                return null;
            }
            List<IntVec3> list;
            if (!WalkPathFinder.TryFindWalkPath(pawn, root, out list))
            {
                return null;
            }
            Job job = new Job(this.def.jobDef, list[0]);
            job.targetQueueA = new List<LocalTargetInfo>();
            job.targetQueueA.Add(list[0]);
            job.locomotionUrgency = LocomotionUrgency.Walk;
            return job;
        }
    }
    public class JobDriver_PlaceDirt : JobDriver_Skygaze
    {
        public override string GetReport()
        {
            if (base.Map.gameConditionManager.ConditionIsActive(GameConditionDefOf.Eclipse))
            {
                return "Placing Dirt";
            }
            if (base.Map.gameConditionManager.ConditionIsActive(GameConditionDefOf.Aurora))
            {
                return "Placing Dirt";
            }
            float num = GenCelestial.CurCelestialSunGlow(base.Map);
            if (num < 0.1f)
            {
                return "Placing Dirt";
            }
            if (num >= 0.65f)
            {
                return "Placing Dirt";
            }
            if (GenLocalDate.DayPercent(this.pawn) < 0.5f)
            {
                return "Placing Dirt";
            }
            return "Placing Dirt";
        }
    }
    public class JoyGiver_EatSomeDirt : JoyGiver
    {
        public override Job TryGiveJob(Pawn pawn)
        {

            if (PawnUtility.WillSoonHaveBasicNeed(pawn))
            {
                return null;
            }
            Predicate<IntVec3> cellValidator = (IntVec3 x) => !PawnUtility.KnownDangerAt(x, pawn.Map, pawn) && !x.GetTerrain(pawn.Map).avoidWander && x.Standable(pawn.Map) && x.SupportsStructureType(pawn.Map, TerrainAffordanceDefOf.Diggable);
            Predicate<Region> validator = delegate (Region x)
            {
                IntVec3 intVec;
                return !x.IsForbiddenEntirely(pawn) && x.TryFindRandomCellInRegionUnforbidden(pawn, cellValidator, out intVec);
            };
            Region reg;
            if (!CellFinder.TryFindClosestRegionWith(pawn.GetRegion(RegionType.Set_Passable), TraverseParms.For(pawn, Danger.Deadly, TraverseMode.ByPawn, false), validator, 500, out reg, RegionType.Set_Passable))
            {
                return null;
            }
            IntVec3 root;
            if (!reg.TryFindRandomCellInRegionUnforbidden(pawn, cellValidator, out root))
            {
                return null;
            }
            List<IntVec3> list;
            if (!WalkPathFinder.TryFindWalkPath(pawn, root, out list))
            {
                return null;
            }
            Job job = new Job(this.def.jobDef, list[0]);
            job.targetQueueA = new List<LocalTargetInfo>();
            job.targetQueueA.Add(list[0]);
            job.locomotionUrgency = LocomotionUrgency.Walk;
            return job;
        }
    }
    public class JobDriver_EatDirt : JobDriver_Skygaze
    {
        public override string GetReport()
        {
            if (base.Map.gameConditionManager.ConditionIsActive(GameConditionDefOf.Eclipse))
            {
                return "Eating Dirt";
            }
            if (base.Map.gameConditionManager.ConditionIsActive(GameConditionDefOf.Aurora))
            {
                return "Eating Dirt";
            }
            float num = GenCelestial.CurCelestialSunGlow(base.Map);
            if (num < 0.1f)
            {
                return "Eating Dirt";
            }
            if (num >= 0.65f)
            {
                return "Eating Dirt";
            }
            if (GenLocalDate.DayPercent(this.pawn) < 0.5f)
            {
                return "Eating Dirt";
            }
            return "Eating Dirt";
        }
    }
    public class JoyGiver_WanderMapEdge : JoyGiver
    {
        public override Job TryGiveJob(Pawn pawn)
        {
            if (PawnUtility.WillSoonHaveBasicNeed(pawn))
            {
                return null;
            }
            Predicate<IntVec3> cellValidator = (IntVec3 x) => !PawnUtility.KnownDangerAt(x, pawn.Map, pawn) && !x.GetTerrain(pawn.Map).avoidWander && x.Standable(pawn.Map) && x.CloseToEdge(pawn.Map, 5);
            Predicate<Region> validator = delegate (Region x)
            {
                IntVec3 intVec;
                return x.Room.PsychologicallyOutdoors && x.TryFindRandomCellInRegionUnforbidden(pawn, cellValidator, out intVec);
            };
            Region reg;
            if (!CellFinder.TryFindClosestRegionWith(pawn.GetRegion(RegionType.Set_Passable), TraverseParms.For(pawn, Danger.Deadly, TraverseMode.ByPawn, false), validator, 500, out reg, RegionType.Set_Passable))
            {
                return null;
            }
            IntVec3 root;
            if (!reg.TryFindRandomCellInRegionUnforbidden(pawn, cellValidator, out root))
            {
                return null;
            }
            List<IntVec3> list;
            if (!WalkPathFinder.TryFindWalkPath(pawn, root, out list))
            {
                return null;
            }
            IntVec3 root2;
            if (!reg.TryFindRandomCellInRegionUnforbidden(pawn, cellValidator, out root2))
            {
                return null;
            }
            List<IntVec3> list2;
            if (!WalkPathFinder.TryFindWalkPath(pawn, root2, out list2))
            {
                return null;
            }
            IntVec3 root4;
            if (!reg.TryFindRandomCellInRegionUnforbidden(pawn, cellValidator, out root4))
            {
                return null;
            }
            List<IntVec3> list4;
            if (!WalkPathFinder.TryFindWalkPath(pawn, root4, out list4))
            {
                return null;
            }
            IntVec3 root5;
            if (!reg.TryFindRandomCellInRegionUnforbidden(pawn, cellValidator, out root5))
            {
                return null;
            }
            List<IntVec3> list5;
            if (!WalkPathFinder.TryFindWalkPath(pawn, root4, out list5))
            {
                return null;
            }
            Job job = new Job(this.def.jobDef, list[0]);
            job.targetQueueA = new List<LocalTargetInfo>();
            //can't figure out how to get hte pawn to wait so this has them sit for an extra tick. sadly not more than that.
            list[1] = list[0];
            list[2] = list2[0];
            list[3] = list2[0];
            list[4] = list4[0];
            list[5] = list4[0];
            list[6] = list5[0];
            list[7] = list5[0];
            job.targetQueueA.Add(list[0]);
            job.targetQueueA.Add(list[1]);
            job.targetQueueA.Add(list[2]);
            job.targetQueueA.Add(list[3]);
            job.targetQueueA.Add(list[4]);
            job.targetQueueA.Add(list[5]);
            job.targetQueueA.Add(list[6]);
            job.targetQueueA.Add(list[7]);
            /* adding this made htem go back to the start again. its intersting. still nots rue how this translates to wandering only in water
            for (int i = 1; i < list.Count; i++)
            {
                job.targetQueueA.Add(list[i]);
            }
            */
            job.locomotionUrgency = LocomotionUrgency.Sprint;
            return job;
        }
    }
    public class JobDriver_StareAtCeiling : JobDriver
    {
        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {
            return true;
        }
        
        protected override IEnumerable<Toil> MakeNewToils()
        {
            yield return Toils_Goto.GotoCell(TargetIndex.A, PathEndMode.OnCell);
            Toil gaze = new Toil();
            gaze.initAction = delegate ()
            {
                this.pawn.jobs.posture = PawnPosture.LayingOnGroundFaceUp;
            };
            gaze.defaultCompleteMode = ToilCompleteMode.Delay;
            gaze.defaultDuration = this.job.def.joyDuration;
            yield return gaze;
        }
    }
    public class JobDriver_GoForWalk_Test : JobDriver
    {
        // Token: 0x06000298 RID: 664 RVA: 0x00019488 File Offset: 0x00017888
        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {
            return true;
        }

        // Token: 0x06000299 RID: 665 RVA: 0x0001948C File Offset: 0x0001788C
        protected override IEnumerable<Toil> MakeNewToils()
        {
            Toil goToil = Toils_Goto.GotoCell(TargetIndex.A, PathEndMode.OnCell);
            yield return goToil;
            yield break;
        }
    }
    public class JoyGiver_GoFindSun : JoyGiver
    {
        // Token: 0x06000568 RID: 1384 RVA: 0x00035040 File Offset: 0x00033440
        public override Job TryGiveJob(Pawn pawn)
        {
            string IsCurrentWeatherClear = "Null";
            if (pawn.Map.weatherManager.curWeather != null)
            {
                IsCurrentWeatherClear = pawn.Map.weatherManager.curWeather.ToString();
            }
            if (IsCurrentWeatherClear != "Clear")
            {
                return null;
            }
            float CurrentCelestialGlow = GenCelestial.CurCelestialSunGlow(pawn.Map);
            if (CurrentCelestialGlow <= 0.4f)
            {
                return null;
            }
            /*
            if (GenLocalDate.DayPercent(pawn.Map) < 0.5f)
            {
                return null;
            }
            */
            if (!JoyUtility.EnjoyableOutsideNow(pawn, null))
            {
                return null;
            }
            if (PawnUtility.WillSoonHaveBasicNeed(pawn))
            {
                return null;
            }
            Predicate<IntVec3> cellValidator = (IntVec3 x) => !PawnUtility.KnownDangerAt(x, pawn.Map, pawn) && !x.GetTerrain(pawn.Map).avoidWander && x.Standable(pawn.Map) && !x.Roofed(pawn.Map);
            Predicate<Region> validator = delegate (Region x)
            {
                IntVec3 intVec;
                return x.Room.PsychologicallyOutdoors && !x.IsForbiddenEntirely(pawn) && x.TryFindRandomCellInRegionUnforbidden(pawn, cellValidator, out intVec);
            };
            Region reg;
            if (!CellFinder.TryFindClosestRegionWith(pawn.GetRegion(RegionType.Set_Passable), TraverseParms.For(pawn, Danger.Deadly, TraverseMode.ByPawn, false), validator, 100, out reg, RegionType.Set_Passable))
            {
                return null;
            }
            IntVec3 root;
            if (!reg.TryFindRandomCellInRegionUnforbidden(pawn, cellValidator, out root))
            {
                return null;
            }
            List<IntVec3> list;
            if (!WalkPathFinder.TryFindWalkPath(pawn, root, out list))
            {
                return null;
            }
            Job job = new Job(this.def.jobDef, list[0]);
            job.targetQueueA = new List<LocalTargetInfo>();
            for (int i = 1; i < list.Count; i++)
            {
                job.targetQueueA.Add(list[i]);
            }
            job.locomotionUrgency = LocomotionUrgency.Walk;
            return job;
        }
    }
    public class JobDriver_SunBathe : JobDriver_Skygaze
    {
        public override string GetReport()
        {
            if (base.Map.gameConditionManager.ConditionIsActive(GameConditionDefOf.Eclipse))
            {
                return "Sunbathing";
            }
            if (base.Map.gameConditionManager.ConditionIsActive(GameConditionDefOf.Aurora))
            {
                return "Sunbathing";
            }
            float num = GenCelestial.CurCelestialSunGlow(base.Map);
            if (num < 0.1f)
            {
                return "Sunbathing";
            }
            if (num >= 0.65f)
            {
                return "Sunbathing";
            }
            if (GenLocalDate.DayPercent(this.pawn) < 0.5f)
            {
                return "Sunbathing";
            }
            return "Sunbathing";
        }
    }
}
