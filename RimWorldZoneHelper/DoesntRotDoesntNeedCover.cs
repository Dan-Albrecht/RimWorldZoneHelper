namespace RimWorldZoneHelper
{
    using System.Diagnostics;
    using Verse;

    public class DoesntRotDoesntNeedCover : SpecialThingFilterWorker
    {
        public override bool Matches(Thing t)
        {
            if (t == null)
            {
                return false;
            }

            if (!CanEverMatch(t.def))
            {
                return false;
            }

            if (RottableFilter.Matches(t.def))
            {
                // See if this is fully rotted at which point it cannot get any worse and we can put it in this zone
                Debugger.Break();
                return false;
            }

            if (MustCoverDoesntRot.Matches(t.def))
            {
                return false;
            }

            return true;
        }

        public override bool CanEverMatch(ThingDef def)
        {
            if (MustCoverDoesntRot.Matches(def))
            {
                return false;
            }

            return true;
        }
    }
}
