namespace RimWorldZoneHelper
{
    using RimWorld;
    using Verse;

    public class MustCoverDoesntRot : SpecialThingFilterWorker
    {
        internal static bool Matches(ThingDef thingDef)
        {
            if (thingDef == null)
            {
                return false;
            }

            if (RottableFilter.Matches(thingDef))
            {
                return false;
            }

            if (thingDef.CanEverDeteriorate)
            {
                float rate = thingDef.GetStatValueAbstract(StatDefOf.DeteriorationRate);

                if (rate > 0.0)
                {
                    return true;
                }
                else
                {
                    // This is stuff like chunks (granit, marble, etc.)
                    return false;
                }
            }

            return false;
        }

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
            return Matches(def);
        }

        public override bool CanEverMatch(ThingDef def)
        {
            return Matches(def);
        }
    }
}
