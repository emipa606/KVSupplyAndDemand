using Harmony;
using RimWorld;
using System;
using System.Reflection;
using UnityEngine;
using Verse;

namespace SupplyAndDemand
{
    [StaticConstructorOnStartup]
    class Main
    {
        public static Pawn ChoicesForPawn = null;

        static Main()
        {
            var harmony = HarmonyInstance.Create("com.supplyanddemand.rimworld.mod");
            harmony.PatchAll(Assembly.GetExecutingAssembly());

            Log.Message(
                "SupplyAndDemand Harmony Patches:" + Environment.NewLine +
                "  Postfix:" + Environment.NewLine +
                "    StockGenerator.RandomCountOf");
        }
    }

    [HarmonyPatch(typeof(StockGenerator), "RandomCountOf")]
    static class Patch_FloatMenuMakerMap_ChoicesAtFor
    {
        static void Postfix(ref int __result, StockGenerator __instance)
        {
            float adjustment = Settings.ScaleAdjustment * 0.001f;
            float totalWealth = Find.CurrentMap.wealthWatcher.WealthTotal;
            float storyTellerPriceLossFactor = 1f - Find.Storyteller.difficulty.tradePriceFactorLoss;

            if (Settings.ScaleVisitors || __instance.trader.orbital || __instance.trader.defName.Contains("Base_"))
            {
                double log = Math.Log(2.7182818284590451 + totalWealth * storyTellerPriceLossFactor);
                if (Settings.IncludeLogging)
                {
                    Log.Message("    __result pre: " + __result);
                }
                __result = (int)(adjustment * log * __result);
                if (Settings.IncludeLogging)
                {
                    Log.Message("    __result post: " + __result);
                    Log.Message("");
                }
            }
        }

        public class SettingsController : Mod
        {
            public SettingsController(ModContentPack content) : base(content)
            {
                base.GetSettings<Settings>();
            }

            public override string SettingsCategory()
            {
                return "Supply And Demand";
            }

            public override void DoSettingsWindowContents(Rect inRect)
            {
                Settings.DoSettingsWindowContents(inRect);
            }

            public override void WriteSettings()
            {
                base.WriteSettings();
            }
        }

        public class Settings : ModSettings
        {
            private const float MAX_SCALE = 400f;
            public static bool ScaleVisitors;

            public static float ScaleAdjustment = 1;

            public static bool IncludeLogging = false;

            public override void ExposeData()
            {
                base.ExposeData();

                Scribe_Values.Look<bool>(ref ScaleVisitors, "ScaleVisitors", false, false);
                Scribe_Values.Look<float>(ref ScaleAdjustment, "ScaleAdjustment", 100, false);

                if (Scribe.mode == LoadSaveMode.LoadingVars)
                {
                    if (ScaleAdjustment < 1)
                        ScaleAdjustment = 1;
                    else if (ScaleAdjustment > MAX_SCALE)
                        ScaleAdjustment = MAX_SCALE;
                }
            }

            public static void DoSettingsWindowContents(Rect rect)
            {
                Listing_Standard l = new Listing_Standard(GameFont.Small)
                {
                    ColumnWidth = System.Math.Min(400, rect.width / 2)
                };
                
                l.Begin(rect);
                l.CheckboxLabeled("Scale Visitors?", ref ScaleVisitors);
                l.Label("Scalar: " + ScaleAdjustment.ToString("n2") + "%");
                ScaleAdjustment = l.Slider(ScaleAdjustment, 0.1f, MAX_SCALE);
                l.End();
                if (Widgets.ButtonText(new Rect(20, 110, 100, 30), "Reset"))
                    ScaleAdjustment = 100;

                Widgets.CheckboxLabeled(new Rect(0, 300, 150, 30), "Debug Logging", ref IncludeLogging);
            }
        }
    }
}