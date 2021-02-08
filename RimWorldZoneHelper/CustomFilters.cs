namespace RimWorldZoneHelper
{
    using System;
    using HarmonyLib;
    using RimWorld;
    using Verse;

    [HarmonyPatch(typeof(ThingCategoryNodeDatabase), "FinalizeInit")]
    public static class CustomFilters
    {
        public static bool HideBuiltin { get; set; }

        public static void Prefix()
        {
            foreach (SpecialThingFilterDef thingFilter in DefDatabase<SpecialThingFilterDef>.AllDefs)
            {
                // We just want our filters to show to minimzed cluster. We cannot delete the builtin
                // ones so just hide instead.
                Log.Message($"{thingFilter} Default: {thingFilter.allowedByDefault} Config: {thingFilter.configurable} def: {thingFilter.defName} Desc: {thingFilter.description} label: {thingFilter.label} Parent {thingFilter.parentCategory}  Generated: {thingFilter.generated} SaveKey: {thingFilter.saveKey} Worker {thingFilter.workerClass}");

                //thingFilter.configurable = false;
            }


            ThingCategoryDef rootCategory = ThingCategoryDefOf.Root;

            AddFilter(rootCategory, "AllowFrozen", "Things that must be frozen to last indefinitely", "must be frozen & covered", "allowFrozen", typeof(RottableFilter));
            AddFilter(rootCategory, "AllowCovered", "Things that must be covered to last indefinitely", "must be convered", "allowCovered", typeof(MustCoverDoesntRot));
            AddFilter(rootCategory, "AllowAnywhere", "Things that can be placed anywhere and will last indefinitely", "no storage restrictions", "allowAnywhere", typeof(DoesntRotDoesntNeedCover));

            DefDatabase<SpecialThingFilterDef>.ErrorCheckAllDefs();

            //var x = new Zone_Stockpile();
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
