using HarmonyLib;
using RimWorld;
using System;
using System.Reflection;
using Verse;

namespace SupplyAndDemand
{
    [StaticConstructorOnStartup]
    class Main
    {
        public static Pawn ChoicesForPawn = null;

        static Main()
        {
            var harmony = new Harmony("com.supplyanddemand.rimworld.mod");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }

    [HarmonyPatch(typeof(StockGenerator), "RandomCountOf")]
    static class Patch_FloatMenuMakerMap_ChoicesAtFor
    {
        static void Postfix(ref int __result, StockGenerator __instance)
        {
            float adjustment = Settings.ScaleAdjustment * 0.0008f;
            float totalWealth = Find.World.PlayerWealthForStoryteller;
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
    }
}