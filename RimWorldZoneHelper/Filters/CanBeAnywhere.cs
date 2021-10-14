namespace RimWorldZoneHelper
{
    using System.Diagnostics;
    using Verse;

    public class CanBeAnywhere : SpecialThingFilterWorker
    {
        public override bool Matches(Thing t)
        {
            if (t == null)
            {
                return false;
            }

            if (MustBeFrozen.MatchesInternal(t))
            {
                return false;
            }

            if (MustBeCovered.MatchesInternal(t))
            {
                return false;
            }

            return true;
        }
    }
}
