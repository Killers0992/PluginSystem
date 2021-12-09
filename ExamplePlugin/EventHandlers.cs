using PluginSystem.EventHandlers;
using PluginSystem.Events;

namespace ExamplePlugin
{
    public class EventHandlers : IEventHandlerPlayerJoin, IEventHandlerPlayerLeave, IEventHandlerWaitingForPlayers, IEventHandlerBan, IEventHandlerLure, IEventHandlerWarheadStartCountdown, IEventHandlerWarheadStopCountdown
    {
        private MainClass Plugin;
        public EventHandlers(MainClass plugin)
        {
            this.Plugin = plugin;
        }

        public void OnBan(BanEvent ev)
        {
            Plugin.Logger.Info($"Player {ev.Issuer} tried to ban {ev.Player} with reason {ev.Reason}, canceled");
            ev.AllowBan = false;
        }

        public void OnLure(PlayerLureEvent ev)
        {
            Plugin.Logger.Info($"{ev.Player} tried to lure in scp106, canceled");
            ev.AllowContain = false;
        }

        public void OnPlayerJoin(PlayerJoinEvent ev)
        {
            Plugin.Logger.Info($"Player joined {ev.Player}");
        }

        public void OnPlayerLeave(PlayerLeaveEvent ev)
        {
            Plugin.Logger.Info($"Player left {ev.Player}");
        }

        public void OnStartCountdown(WarheadStartEvent ev)
        {
            Plugin.Logger.Info($"{(ev.Activator == null ? "Server" : ev.Activator.ToString())} tried to start warhead countdown, canceled.");
            ev.Cancel = true;
        }

        public void OnStopCountdown(WarheadStopEvent ev)
        {
            Plugin.Logger.Info($"{(ev.Activator == null ? "Server" : ev.Activator.ToString())} tried to stop warhead countdown, canceled.");
            ev.Cancel = true;
        }

        public void OnWaitingForPlayers(WaitingForPlayersEvent ev)
        {
            Plugin.Logger.Info($"Waiting for players.");
        }
    }
}
