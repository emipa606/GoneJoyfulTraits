using System;
using System.Collections.Generic;
using RimWorld;
using Verse;
using Verse.AI;

namespace GBTK_JobTypes
{
    //pawn likes to take non watery path. not sure how to change it. at least with this method it seeks out new water cells
    public class JoyGiver_GoForWalk_789789 : JoyGiver
    {

        public override Job TryGiveJob(Pawn pawn)
        {
            
            if (PawnUtility.WillSoonHaveBasicNeed(pawn))
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
    //The below is unalted of hte base code and doesn't work
    public class JobDriver_GoForWalk_Test : JobDriver
    {
        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {
            return true;
        }

        protected override IEnumerable<Toil> MakeNewToils()
        {
            this.FailOn(() => !JoyUtility.EnjoyableOutsideNow(this.pawn, null));
            Toil goToil = Toils_Goto.GotoCell(TargetIndex.A, PathEndMode.OnCell);
            goToil.tickAction = delegate ()
            {
                if (Find.TickManager.TicksGame > this.startTick + this.job.def.joyDuration)
                {
                    this.EndJobWith(JobCondition.Succeeded);
                    return;
                }
                JoyUtility.JoyTickCheckEnd(this.pawn, JoyTickFullJoyAction.EndJob, 1f, null);
            };
            yield return goToil;
            yield return new Toil
            {
                initAction = delegate ()
                {
                    if (this.job.targetQueueA.Count > 0)
                    {
                        LocalTargetInfo targetA = this.job.targetQueueA[0];
                        this.job.targetQueueA.RemoveAt(0);
                        this.job.targetA = targetA;
                        this.JumpToToil(goToil);
                        return;
                    }
                }
            };
            yield break;
        }
    }
    public class JoyGiver_GoForWalk_InRain : JoyGiver
    {
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
    public class JoyGiver_GBKT_RunBackAndForth : JoyGiver
    {

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
}
