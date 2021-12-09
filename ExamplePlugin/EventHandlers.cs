using PluginSystem.EventHandlers;
using PluginSystem.Events;

namespace ExamplePlugin
{
    public class EventHandlers : IEventHandlerPlayerJoin, IEventHandlerPlayerLeave, IEventHandlerWaitingForPlayers
    {
        private MainClass Plugin;
        public EventHandlers(MainClass plugin)
        {
            this.Plugin = plugin;
        }

        public void OnPlayerJoin(PlayerJoinEvent ev)
        {
            Plugin.Logger.Info($"Player joined {ev.Player.Name} ({ev.Player.UserId})");
        }

        public void OnPlayerLeave(PlayerLeaveEvent ev)
        {
            Plugin.Logger.Info($"Player left {ev.Player.Name} ({ev.Player.UserId})");
        }

        public void OnWaitingForPlayers(WaitingForPlayersEvent ev)
        {
            Plugin.Logger.Info($"Waiting for players.");
        }
    }
}
