using GBTK_DefinitionTypes;
using RimWorld;
using Verse;

namespace GBKT_ThoughtWorkers;

public class ThoughtWorker_GBKTSkyGazer : ThoughtWorker
{
    protected override ThoughtState CurrentStateInternal(Pawn p)
    {
        var IsPawnRoofed = p.Position.Roofed(p.Map);
        var whatIsTheWeather = p.Map.weatherManager.curWeather.ToString();
        if (!p.Spawned || !p.RaceProps.Humanlike ||
            !p.story.traits.HasTrait(GBTK_DefinitionTypes_Traits.GBKT_SkyGazer) || IsPawnRoofed)
        {
            return ThoughtState.Inactive;
        }

        return whatIsTheWeather != "Clear" ? ThoughtState.Inactive : ThoughtState.ActiveAtStage(0);
    }
}