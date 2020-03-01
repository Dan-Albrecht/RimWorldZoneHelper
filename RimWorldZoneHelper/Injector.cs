namespace RimWorldZoneHelper
{
    using System.Reflection;
    using HarmonyLib;
    using Verse;

    public class Injector : Mod
    {
        public Injector(ModContentPack content) : base(content)
        {
            Log.Message($"Injecting {Assembly.GetExecutingAssembly().FullName}...");

            var harmony = new Harmony("albrecht.dan.zonehelper");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }
}
