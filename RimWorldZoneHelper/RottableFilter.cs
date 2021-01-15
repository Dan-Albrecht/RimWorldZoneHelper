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

        public override bool Matches(Thing t)
        {
            if (t == null)
            {
                return false;
            }

            return Matches(t.def);
        }
    }
}
