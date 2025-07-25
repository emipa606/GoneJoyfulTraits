using GBTK_DefinitionTypes;
using RimWorld;
using Verse;

namespace GBKT_ThoughtWorkers;

public class ThoughtWorker_GBKTSkyGazer : ThoughtWorker
{
    protected override ThoughtState CurrentStateInternal(Pawn pawn)
    {
        var IsPawnRoofed = pawn.Position.Roofed(pawn.Map);
        var whatIsTheWeather = pawn.Map.weatherManager.curWeather.ToString();
        if (!pawn.Spawned || !pawn.RaceProps.Humanlike ||
            !pawn.story.traits.HasTrait(GBTK_DefinitionTypes_Traits.GBKT_SkyGazer) || IsPawnRoofed)
        {
            return ThoughtState.Inactive;
        }

        return whatIsTheWeather != "Clear" ? ThoughtState.Inactive : ThoughtState.ActiveAtStage(0);
    }
}