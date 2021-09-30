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
                Log.Warning("Called to match something we'll never match");
                return false;
            }

            if (RottableFilter.Matches(t.def))
            {
                Log.Message($"Check it: {t}");
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
            Helpers.LogOnce($"{nameof(DoesntRotDoesntNeedCover)}.{nameof(CanEverMatch)}");

            if (MustCoverDoesntRot.Matches(def))
            {
                return false;
            }

            // Since we only define 3 filters, this is either a truly doesn't rot, doesn't need cover
            // Or it is rottable in which case it'll match if it is already fully destoryed
            return true;
        }
    }
}
