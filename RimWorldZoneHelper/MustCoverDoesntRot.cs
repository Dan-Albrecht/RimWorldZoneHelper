namespace RimWorldZoneHelper
{
    using RimWorld;
    using Verse;

    public class MustCoverDoesntRot : SpecialThingFilterWorker
    {
        public static bool Matches(ThingDef thingDef)
        {
            if (thingDef == null)
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

            if (RottableFilter.Matches(t.def))
            {
                return false;
            }

            return Matches(t.def);
        }
    }
}
