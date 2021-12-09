using PluginSystem.API;
using PluginSystem.Attributes;
using PluginSystem.EventHandlers;
using PluginSystem.Events;
using PluginSystem.Logging;
using System;

namespace PluginSystem
{
    public abstract class Plugin<TConfig> : IPlugin<TConfig> where TConfig : IConfig, new()
    {
        public PluginDetails Details { get; set; }

        public TConfig Config { get; } = new TConfig();

        public ILogger Logger { get; } = new BaseLogger();

        public EventManager EventManager => EventManager.Manager;
        public PluginManager PluginManager => PluginManager.Manager;
        public RoleManager RoleManager => RoleManager.Manager;
        public Server Server => PluginManager.Manager.Server;

        public virtual void Register() => Logger.Info($"{Details.Name} ({Details.Version}) has been registered!");

        public virtual void OnEnable() => Logger.Info($"{Details.Name} ({Details.Version}) has been enabled!");

        public virtual void OnDisable() => Logger.Info($"{Details.Name} ({Details.Version}) has been disabled!");

        public void AddEventHandlers(IEventHandler handler, Priority priority = Priority.Normal)
        {
            if (PluginManager.Plugins.TryGetValue(Details.Id, out IPlugin<IConfig> plugin))
                EventManager.AddEventHandlers(plugin, handler, priority);
        }

        public void AddEventHandler(Type eventType, IEventHandler handler, Priority priority = Priority.Normal)
        {
            if (PluginManager.Plugins.TryGetValue(Details.Id, out IPlugin<IConfig> plugin))
                EventManager.AddEventHandler(plugin, eventType, handler, priority);
        }
    }
}
