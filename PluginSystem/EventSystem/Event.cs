using PluginSystem.EventHandlers;

namespace PluginSystem.Events
{
    public abstract class Event
    {
        public abstract void ExecuteHandler(IEventHandler handler);
    }
}
