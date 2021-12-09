using PluginSystem;
using PluginSystem.Attributes;

namespace ExamplePlugin
{
    [PluginDetails(
        Name = "ExamplePlugin",
        Description = "Example plugin for plugin api.",
        Author = "Killers0992",
        Version = "1.0.0",
        ApiVersion = "1.0.0")]
    public class MainClass : Plugin<PluginConfig>
    {
        public override void OnDisable()
        {
            Logger.Info($"Disabled.");
        }

        public override void OnEnable()
        {
            Logger.Info($"Enabled.");
        }

        public override void Register()
        {
            RoleManager.RegisterRole(new CustomClassD());
            AddEventHandlers(new EventHandlers(this));
            Logger.Info($"Registered.");
        }
    }
}
