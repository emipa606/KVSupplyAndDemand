using Mlie;
using UnityEngine;
using Verse;

namespace SupplyAndDemand;

public class SettingsController : Mod
{
    public static string CurrentVersion;

    public SettingsController(ModContentPack content)
        : base(content)
    {
        GetSettings<Settings>();
        CurrentVersion = VersionFromManifest.GetVersionFromModMetaData(content.ModMetaData);
    }

    public override string SettingsCategory()
    {
        return "Supply And Demand";
    }

    public override void DoSettingsWindowContents(Rect inRect)
    {
        Settings.DoSettingsWindowContents(inRect);
    }
}