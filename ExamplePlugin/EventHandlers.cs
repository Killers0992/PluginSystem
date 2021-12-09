using PluginSystem.EventHandlers;
using PluginSystem.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamplePlugin
{
    public class EventHandlers : IEventHandlerPlayerJoin, IEventHandlerWaitingForPlayers
    {
        private MainClass Plugin;
        public EventHandlers(MainClass plugin)
        {
            this.Plugin = plugin;
        }

        public void OnPlayerJoin(PlayerJoinEvent ev)
        {
            ev.Player.SendConsoleMessage("Join", "red");
            Plugin.Logger.Info($"Player joined {ev.Player.Name} ({ev.Player.UserId})");
        }

        public void OnWaitingForPlayers(WaitingForPlayersEvent ev)
        {
            Plugin.Logger.Info($"Waiting for players.");
        }
    }
}
