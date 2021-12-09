using PluginSystem.API;
using PluginSystem.Attributes;
using PluginSystem.EventHandlers;
using PluginSystem.Events;
using System;

namespace PluginSystem
{
    public interface IPlugin<out TConfig> where TConfig : IConfig
    {
        PluginDetails Details { get; set; }
        TConfig Config { get; }
        ILogger Logger { get; }
        EventManager EventManager { get; }
        PluginManager PluginManager { get; }
        RoleManager RoleManager { get; }
        Server Server { get; }
        void Register();
        void OnEnable();
        void OnDisable();
        void AddEventHandlers(IEventHandler handler, Priority priority = Priority.Normal);
        void AddEventHandler(Type eventType, IEventHandler handler, Priority priority = Priority.Normal);
    }
}
