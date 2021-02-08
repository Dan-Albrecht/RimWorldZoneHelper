using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace RimWorldZoneHelper
{
    public class ZoneHelperSettings : ModSettings
    {
        [SuppressMessage("Design", "CA1051:Do not declare visible instance fields", Justification = "Everything wants refs to this, so just using a field")]
        public bool HideBuiltinFilters = true;

        public override void ExposeData()
        {
            Scribe_Values.Look(ref this.HideBuiltinFilters, nameof(HideBuiltinFilters));
            base.ExposeData();
        }
    }
}
