using CommandSystem.Commands.Shared;
using PluginSystem.API;
using PluginSystem.Events;
using System.IO;

namespace PluginSystem
{
    public static class PluginSystem
    {
        public static void Initialize()
        {
            CustomNetworkManager.Modded = true;
            BuildInfoCommand.ModDescription = $"PluginSystem ({PluginManager.ApiVersion.ToString(3)})";

            if (!Directory.Exists("PluginSystem"))
                Directory.CreateDirectory("PluginSystem");

            ServerPluginsPath = Path.Combine("PluginSystem", $"plugins-{ServerStatic.ServerPort}");

            if (!Directory.Exists(ServerPluginsPath))
                Directory.CreateDirectory(ServerPluginsPath);

            if (!Directory.Exists(Path.Combine(ServerPluginsPath, "dependencies")))
                Directory.CreateDirectory(Path.Combine(ServerPluginsPath, "dependencies"));

            PluginManager.Manager.LogHandler = new SLLogHandler();
            PluginManager.Manager.Server = new SLServer(new SLMap(), new SLRound());

            RoleManager.Manager.LoadDefaultRoles();
            PluginManager.Manager.LoadPlugins(ServerPluginsPath);
            PluginManager.Manager.EnablePlugins();

        }

        public static ReferenceHub LastStartedDetonationBy;

        public static bool LureCanceled = false;

        public static string ServerPluginsPath;
    }
}
