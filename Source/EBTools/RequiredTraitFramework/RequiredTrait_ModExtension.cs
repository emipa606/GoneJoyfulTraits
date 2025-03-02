using System.Collections.Generic;
using RimWorld;
using Verse;

namespace EBTools.RequiredTraitFramework;

internal class RequiredTrait_ModExtension : DefModExtension
{
    public List<TraitDef> decreaseChanceTraits;
    public List<TraitDef> forbiddenTraits;
    public List<TraitDef> increaseChanceTraits;
    public List<TraitDef> requiredTraitsAll;
    public List<TraitDef> requiredTraitsOne;
}