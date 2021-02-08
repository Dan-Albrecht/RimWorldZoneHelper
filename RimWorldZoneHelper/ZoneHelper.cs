namespace RimWorldZoneHelper
{
    using System;
    using System.Reflection;
    using HarmonyLib;
    using UnityEngine;
    using Verse;

    // %LOCALAPPDATA%Low\Ludeon Studios\RimWorld by Ludeon Studios\Player.log
    public class ZoneHelper : Mod
    {
        public ZoneHelperSettings ModSettings { get; }
        private bool currentHide;

        public ZoneHelper(ModContentPack content) : base(content)
        {
            if (ZoneHelper.Instnace == null)
            {
                ZoneHelper.Instnace = this;
            }
            else
            {
                Log.Error("There's already an instance of this mod present. Not patching.");
                return;
            }

            this.ModSettings = this.GetSettings<ZoneHelperSettings>();
            this.currentHide = this.ModSettings.HideBuiltinFilters;

            Log.Message($"Injecting {Assembly.GetExecutingAssembly().FullName}...");
            var harmony = new Harmony("Albrecht.Dan.ZoneHelper");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

        public static ZoneHelper Instnace { get; private set; }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listingStandard = new Listing_Standard();
            listingStandard.Begin(inRect);

            listingStandard.Label("This is a label");

            listingStandard.CheckboxLabeled(
                "Hide builtin filters",
                ref this.ModSettings.HideBuiltinFilters,
                "A value indicating if we should hide or show the builting filter values. If hidden, the filters will all be set to selected to prevent having XXX.");

            listingStandard.End();
            base.DoSettingsWindowContents(inRect);
        }

        public override string SettingsCategory()
        {
            return "Zone Helper";
        }

        public override void WriteSettings()
        {
            if (this.currentHide != this.ModSettings.HideBuiltinFilters)
            {
                Log.Message($"Changed to {this.ModSettings.HideBuiltinFilters}");
                this.currentHide = this.ModSettings.HideBuiltinFilters;
                CustomFilters.SetHidden(this.currentHide);
            }

            base.WriteSettings();
        }
    }
}
