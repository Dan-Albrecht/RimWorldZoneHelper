namespace RimWorldZoneHelper
{
    using RimWorld;
    using Verse;

    public class RottableFilter : SpecialThingFilterWorker
    {
        public static bool Matches(ThingDef thingDef)
        {
            if (thingDef == null)
            {
                return false;
            }

            return thingDef.GetCompProperties<CompProperties_Rottable>() != null;
        }

        public override bool Matches(Thing thing)
        {
            if (thing == null)
            {
                return false;
            }

            return Matches(thing.def);
        }
    }
}
