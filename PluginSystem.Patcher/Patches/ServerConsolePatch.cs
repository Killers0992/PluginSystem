using MonoMod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginSystem.Patcher.Patches
{
    [MonoModPatch("global::ServerConsole")]
    public class ServerConsolePatch : ServerConsole
    {
        public string RefreshServerName()
        {
            ServerConsole._serverName += $"<color=#00000000><size=1>PluginSystem {PluginManager.ApiVersion.ToString(3)}</size></color>";
            return this.NameFormatter.ProcessExpression(ServerConsole._serverName);
        }
    }
}
