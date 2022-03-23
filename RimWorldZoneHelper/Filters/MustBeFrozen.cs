namespace RimWorldZoneHelper
{
    using RimWorld;
    using Verse;

    public class MustBeFrozen : SpecialThingFilterWorker
    {
        public override bool Matches(Thing t)
        {
            if (t == null)
            {
                Helpers.LogOnceError($"{nameof(MustBeFrozen)} called with null");
                return false;
            }

            return MatchesInternal(t);
        }

        internal static bool MatchesInternal(Thing thing)
        {
            ThingDef def = thing?.def;

            if (def == null)
            {
                return false;
            }

            CompProperties_Rottable prop = def.GetCompProperties<CompProperties_Rottable>();

            if (prop == null)
            {
                return false;
            }

            string mes = $"DefName: {def.defName} DTD: {prop.daysToDessicated} DTRS: {prop.daysToRotStart} DDPD: {prop.dessicatedDamagePerDay} DIH: {prop.disableIfHatcher} RDPD: {prop.rotDamagePerDay} RD: {prop.rotDestroys} TTD: {prop.TicksToDessicated} TTRS: {prop.TicksToRotStart}";

            if (thing.IsDessicated())
            {
                // No point of freezing this anymore
                Helpers.LogOnce($"Dessicated: {mes}");
                return false;
            }

            if (thing.MaxHitPoints >= 0 && thing.HitPoints == 0)
            {
                // Thing is broken now; what does this even mean
                Helpers.LogOnce($"Destroyed: {mes}");
                return false;
            }

            if(thing.MarketValue == 0 && thing.RoyalFavorValue == 0)
            {
                // No one will pay anything for this
                Helpers.LogOnce($"Worthless: {mes}");
                return false;
            }

            return true;
        }
    }
}
