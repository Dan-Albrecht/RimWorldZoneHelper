namespace RimWorldZoneHelper
{
    using RimWorld;
    using Verse;

    public class RottableFilter : SpecialThingFilterWorker
    {
        public override bool Matches(Thing t)
        {
            if (t == null)
            {
                return false;
            }

            return Matches(t.def);
        }

        public override bool AlwaysMatches(ThingDef def)
        {
            Helpers.LogOnce($"{nameof(RottableFilter)}.{nameof(AlwaysMatches)}");
            return Matches(def);
        }

        public override bool CanEverMatch(ThingDef def)
        {
            Helpers.LogOnce($"{nameof(RottableFilter)}.{nameof(CanEverMatch)}");
            return Matches(def);
        }

        internal static bool Matches(ThingDef def)
        {
            if (def == null)
            {
                return false;
            }

            return def.GetCompProperties<CompProperties_Rottable>() != null;
        }
    }
}
