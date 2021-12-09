using System;
using System.Reflection;

namespace PluginSystem.Logging
{
    public class BaseLogger : ILogger
    {
        public void Debug(string message) => RawMessage(Assembly.GetCallingAssembly(), LogType.Debug, message, ConsoleColor.Yellow);
        public void Error(string message) => RawMessage(Assembly.GetCallingAssembly(), LogType.Error, message, ConsoleColor.Red);
        public void Info(string message) => RawMessage(Assembly.GetCallingAssembly(), LogType.Info, message, ConsoleColor.Cyan);
        public void Warn(string message) => RawMessage(Assembly.GetCallingAssembly(), LogType.Warning, message, ConsoleColor.DarkYellow);

        public void RawMessage(Assembly assembly, LogType type, string message, ConsoleColor color)
        {
            PluginManager.PluginsAssemblies.TryGetValue(assembly, out string pluginId);

            switch (type)
            {
                case LogType.Error:
                    PluginManager.Manager.LogHandler.RawMessage($"[ERROR] [{pluginId}] {message}", color);
                    break;
                case LogType.Warning:
                    PluginManager.Manager.LogHandler.RawMessage($"[WARN] [{pluginId}] {message}", color);
                    break;
                case LogType.Info:
                    PluginManager.Manager.LogHandler.RawMessage($"[INFO] [{pluginId}] {message}", color);
                    break;
                case LogType.Debug:
                    PluginManager.Manager.LogHandler.RawMessage($"[DEBUG] [{pluginId}] {message}", color);
                    break;
            }
        }
    }
}
