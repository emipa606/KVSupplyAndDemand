using System;
using HarmonyLib;
using RimWorld;
using Verse;

namespace SupplyAndDemand;

[HarmonyPatch(typeof(StockGenerator), "RandomCountOf")]
internal static class Patch_FloatMenuMakerMap_ChoicesAtFor
{
    private static void Postfix(ref int __result, StockGenerator __instance)
    {
        var num = Settings.ScaleAdjustment * 0.0008f;
        var playerWealthForStoryteller = Find.World.PlayerWealthForStoryteller;
        var num2 = 1f - Find.Storyteller.difficulty.tradePriceFactorLoss;
        if (!Settings.ScaleVisitors && !__instance.trader.orbital && !__instance.trader.defName.Contains("Base_"))
        {
            return;
        }

        var num3 = Math.Log(Math.E + (playerWealthForStoryteller * num2));
        if (Settings.IncludeLogging)
        {
            Log.Message($"    __result pre: {__result}");
        }

        __result = (int)(num * num3 * __result);
        if (!Settings.IncludeLogging)
        {
            return;
        }

        Log.Message($"    __result post: {__result}");
        Log.Message("");
    }
}