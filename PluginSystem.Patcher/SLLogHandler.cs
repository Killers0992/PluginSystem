using PluginSystem.Logging;
using System;

namespace PluginSystem
{
    public class SLLogHandler : LogHandler
    {
        public override void RawMessage(string message, ConsoleColor color)
        {
            ServerConsole.AddLog(message, color);
        }
    }
}
