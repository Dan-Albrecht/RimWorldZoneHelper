namespace RimWorldZoneHelper
{
    using System;
    using HarmonyLib;
    using RimWorld;
    using Verse;

    [HarmonyPatch(typeof(ThingCategoryNodeDatabase), "FinalizeInit")]
    public static class CustomFilters
    {
        private static SpecialThingFilterDef rottenFilter;
        private static SpecialThingFilterDef freshFilter;

        public static void Prefix()
        {
            foreach (SpecialThingFilterDef thingFilter in DefDatabase<SpecialThingFilterDef>.AllDefs)
            {
                // Cannot delete the builtin as the core game expect them to always exist
                if (thingFilter.saveKey == "allowRotten")
                {
                    rottenFilter = thingFilter;
                }
                else if (thingFilter.saveKey == "allowFresh")
                {
                    freshFilter = thingFilter;
                }
            }

            ThingCategoryDef rootCategory = ThingCategoryDefOf.Root;

            AddFilter(rootCategory, "AllowFrozen", "Things that must be frozen to last indefinitely", "must be frozen & covered", "allowFrozen", typeof(RottableFilter));
            AddFilter(rootCategory, "AllowCovered", "Things that must be covered to last indefinitely", "must be convered", "allowCovered", typeof(MustCoverDoesntRot));
            AddFilter(rootCategory, "AllowAnywhere", "Things that can be placed anywhere and will last indefinitely or are already ruined (rotten / desicated)", "no storage restrictions", "allowAnywhere", typeof(DoesntRotDoesntNeedCover));

            SetHidden(ZoneHelper.Instnace.ModSettings.HideBuiltinFilters);
        }

        internal static void SetHidden(bool currentHide)
        {
            Log.Message($"Hidden is now {currentHide}");
            rottenFilter.configurable = !currentHide;
            freshFilter.configurable = !currentHide;
            DefDatabase<SpecialThingFilterDef>.ErrorCheckAllDefs();
        }

        public static void Postfix()
        {
            // Close the tree by default
            ThingCategoryDefOf.Root.treeNode.catDef.childCategories[0].treeNode.SetOpen(-1, val: false);
        }

        private static void AddFilter(ThingCategoryDef rootCategory, string defName, string description, string label, string saveKey, Type type)
        {
            SpecialThingFilterDef existing = DefDatabase<SpecialThingFilterDef>.GetNamedSilentFail(defName);

            if (existing != null)
            {
                Log.Error($"Clashed with an existing filter named '{defName}' from '{existing.fileName}' with description '{existing.description}'");
                return;
            }

            SpecialThingFilterDef freezeFilter = new SpecialThingFilterDef
            {
                allowedByDefault = true,
                configurable = true,
                defName = defName,
                description = description,
                label = label,
                generated = true, /// ???
                parentCategory = rootCategory,
                saveKey = saveKey, /// ???
                workerClass = type,
            };

            DefDatabase<SpecialThingFilterDef>.Add(freezeFilter);
        }
    }
}
