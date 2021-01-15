using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace RimWorldZoneHelper
{
    public class ZoneHelperSettings : ModSettings
    {
#pragma warning disable CA1051 // Do not declare visible instance fields
        public string Value = "Default Value";
#pragma warning restore CA1051 // Do not declare visible instance fields

        public static ZoneHelperSettings Instance { get; set; }

        public override void ExposeData()
        {
            Scribe_Values.Look(ref this.Value, "SomeLabel");
            base.ExposeData();
        }
    }
}
