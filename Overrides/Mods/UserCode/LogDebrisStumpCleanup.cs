using Eco.Core.Plugins.Interfaces;
using Eco.Core.Plugins;
using Eco.Core.Utils;
using Eco.Shared.Localization;
using Eco.Shared.Utils;

namespace Eco.Mods.TechTree
{
    [Localized]
    public class LogDebrisStumpCleanupConfig : Singleton<LogDebrisStumpCleanupConfig>
    {
        [LocDescription("Should debris be cleaned up upon felling trees?")]
        public bool CleanDebrisEnabled { get; set; }

        [LocDescription("Which logging level should be required for debris to be cleaned up? (0 for everyone)")]
        public int CleanDebrisLogging { get; set; }

        [LocDescription("Should stump be cleaned up upon felling trees?")]
        public bool RemoveStumpEnabled { get; set; }

        [LocDescription("Which logging level should be required for stump to be cleaned up? (0 for everyone)")]
        public int RemoveStumpLogging { get; set; }

        [LocDescription("Should stump be cleaned up upon felling trees?")]
        public bool PickupTrunkEnabled { get; set; }

        [LocDescription("Which logging level should be required for trunk to be picked up? (0 for everyone)")]
        public int PickupTrunkLogging { get; set; }
    }

    public class LogDebrisStumpCleanup : IModKitPlugin, IInitializablePlugin, IConfigurablePlugin, IDisplayablePlugin
    {
        public static PluginConfig<LogDebrisStumpCleanupConfig>? config;

        private string status = "";

        public IPluginConfig? PluginConfig => config;

        public ThreadSafeAction<object, string> ParamChanged { get; set; } = new ThreadSafeAction<object, string>();

        public void Initialize(TimedTask timer)
        {
            this.status = "Reading config";
            config = new PluginConfig<LogDebrisStumpCleanupConfig>("LogDebrisStumpCleanup");
            this.status = "";
        }

        public override string ToString() => Localizer.DoStr("Log Debris Stump Cleanup");
        public string GetDisplayText() => this.status;
        public string GetStatus() => this.status;

        public string GetCategory() => "Log Debris Stump";

        public object? GetEditObject() => config?.Config;

        public void OnEditObjectChanged(object o, string param)
        {
        }
    }
}