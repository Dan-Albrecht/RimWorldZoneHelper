namespace RimWorldZoneHelper
{
    using System;
    using System.Reflection;
    using HarmonyLib;
    using Verse;

    public class Injector : Mod
    {
        public Injector(ModContentPack content) : base(content)
        {
            Log.Message($"Injecting {Assembly.GetExecutingAssembly().FullName}...");

            var harmony = new Harmony("Albrecht.Dan.ZoneHelper");
            harmony.PatchAll(Assembly.GetExecutingAssembly());

            try
            {
                Log.Message("Loading settings...");
                ZoneHelperSettings.Instance = this.GetSettings<ZoneHelperSettings>();
                Log.Message($"Settings loaded with value {ZoneHelperSettings.Instance.Value}");
            }
            catch(Exception e)
            {
                Log.Error(e.ToString());
            }
        }
    }
}
