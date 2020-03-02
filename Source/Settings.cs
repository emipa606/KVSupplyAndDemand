using UnityEngine;
using Verse;

namespace SupplyAndDemand
{
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
        private const float MIN_SCALE = 0.1f;
        private const float MAX_SCALE = 1000f;
        private const float SCALE_ADJUSTMENT_DEFAULT = 100f;

        public static bool ScaleVisitors = false;

        public static float ScaleAdjustment = SCALE_ADJUSTMENT_DEFAULT;
        private static string textBuffer = SCALE_ADJUSTMENT_DEFAULT.ToString();

        public static bool IncludeLogging = false;

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Values.Look<bool>(ref ScaleVisitors, "ScaleVisitors", false, false);
            Scribe_Values.Look<float>(ref ScaleAdjustment, "ScaleAdjustmentV2", 100, false);

            if (Scribe.mode == LoadSaveMode.LoadingVars)
            {
                if (ScaleAdjustment < MIN_SCALE)
                    ScaleAdjustment = MIN_SCALE;
                else if (ScaleAdjustment > MAX_SCALE)
                    ScaleAdjustment = MAX_SCALE;
            }

            textBuffer = ScaleAdjustment.ToString("n2");
        }

        public static void DoSettingsWindowContents(Rect rect)
        {
            string originalText = textBuffer;
            float originalScaler = ScaleAdjustment;

            float x = rect.x + 20;
            float y = rect.y;
            Widgets.CheckboxLabeled(new Rect(x, y, 150, 32), "Scale Visitors?", ref ScaleVisitors);
            y += 60;

            Widgets.Label(new Rect(x, y, 75, 32), "Scalar:");
            textBuffer = GUI.TextField(new Rect(x + 80, y, 60, 32), textBuffer);
            Widgets.Label(new Rect(x + 160, y, 40, 32), "%");
            y += 40;

            if (originalText != textBuffer)
            {
                float v;
                if (float.TryParse(textBuffer, out v))
                {
                    if (v >= MIN_SCALE && v <= MAX_SCALE)
                    {
                        ScaleAdjustment = v;
                    }
                }
            }
            else
            {
                ScaleAdjustment = Widgets.HorizontalSlider(new Rect(x, y, 400, 32), ScaleAdjustment, MIN_SCALE, MAX_SCALE);
                y += 40;
                if (Widgets.ButtonText(new Rect(20, y, 100, 30), "Reset"))
                    ScaleAdjustment = 100;

                if (originalScaler != ScaleAdjustment)
                    textBuffer = ScaleAdjustment.ToString("n2");
            }

            //Widgets.CheckboxLabeled(new Rect(0, 300, 150, 30), "Debug Logging", ref IncludeLogging);
        }
    }
}
