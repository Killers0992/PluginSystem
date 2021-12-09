using System;

namespace PluginSystem.Logging
{
    public abstract class LogHandler
    {
        public abstract void RawMessage(string message, ConsoleColor color);
    }
}
