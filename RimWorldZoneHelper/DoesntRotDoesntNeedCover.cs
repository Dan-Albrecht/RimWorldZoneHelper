namespace RimWorldZoneHelper
{
    using Verse;

    public class DoesntRotDoesntNeedCover : SpecialThingFilterWorker
    {
        public override bool Matches(Thing t)
        {
            if (t == null)
            {
                return false;
            }

            if (RottableFilter.Matches(t.def) || MustCoverDoesntRot.Matches(t.def))
            {
                return false;
            }

            return true;
        }
    }
}
