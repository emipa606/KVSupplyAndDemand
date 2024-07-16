using System;
using System.Reflection;
using HarmonyLib;
using Verse;

namespace SupplyAndDemand;

[StaticConstructorOnStartup]
internal class Main
{
    public static Pawn ChoicesForPawn;

    static Main()
    {
        new Harmony("com.supplyanddemand.rimworld.mod").PatchAll(Assembly.GetExecutingAssembly());
        Log.Message(
            $"SupplyAndDemand Harmony Patches:{Environment.NewLine}  Postfix:{Environment.NewLine}    StockGenerator.RandomCountOf");
    }
}