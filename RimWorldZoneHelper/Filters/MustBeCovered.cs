namespace RimWorldZoneHelper
{
    using RimWorld;
    using Verse;

    public class MustBeCovered : SpecialThingFilterWorker
    {
        internal static bool MatchesInternal(Thing thing)
        {
            ThingDef thingDef = thing?.def;

            if (thingDef == null)
            {
                return false;
            }

            if (MustBeFrozen.MatchesInternal(thing))
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
            return MatchesInternal(t);
        }
    }
}
