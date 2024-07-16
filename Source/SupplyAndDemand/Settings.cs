using System;
using UnityEngine;
using Verse;

namespace SupplyAndDemand;

public class Settings : ModSettings
{
    private const float MIN_SCALE = 0.1f;

    private const float SCALE_ADJUSTMENT_DEFAULT = 100f;

    public static bool ScaleVisitors;

    public static float ScaleAdjustment = SCALE_ADJUSTMENT_DEFAULT;

    public static readonly bool IncludeLogging = false;

    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_Values.Look(ref ScaleVisitors, "ScaleVisitors");
        Scribe_Values.Look(ref ScaleAdjustment, "ScaleAdjustmentV2", SCALE_ADJUSTMENT_DEFAULT);
        if (Scribe.mode == LoadSaveMode.LoadingVars)
        {
            ScaleAdjustment = Math.Max(ScaleAdjustment, MIN_SCALE);
        }
    }

    public static void DoSettingsWindowContents(Rect rect)
    {
        var text = ScaleAdjustment.ToString();
        _ = ScaleAdjustment;
        var num = rect.x + 20f;
        var y = rect.y;
        Widgets.CheckboxLabeled(new Rect(num, y, 150f, 32f), "SAD.Scale".Translate(), ref ScaleVisitors);
        y += 60f;
        Widgets.Label(new Rect(num, y, 75f, 32f), "SAD.Scalar".Translate());
        var text2 = GUI.TextField(new Rect(num + 80f, y, 200f, 32f), text);
        Widgets.Label(new Rect(num + 300f, y, 40f, 32f), "%");
        y += 40f;
        if (text2 != text && float.TryParse(text2, out var result))
        {
            result = Math.Max(result, MIN_SCALE);

            ScaleAdjustment = result;
        }

        if (Widgets.ButtonText(new Rect(num, y, SCALE_ADJUSTMENT_DEFAULT, 30f), "Default".Translate()))
        {
            ScaleAdjustment = SCALE_ADJUSTMENT_DEFAULT;
        }

        if (SettingsController.CurrentVersion == null)
        {
            return;
        }

        y += 40f;
        GUI.contentColor = Color.gray;
        Widgets.Label(new Rect(num, y, 150f, 32f), "SAD.ModVersion".Translate(SettingsController.CurrentVersion));
        GUI.contentColor = Color.white;
    }
}