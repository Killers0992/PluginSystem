using MonoMod;

namespace PluginSystem.Patcher.Patches
{
    [MonoModPatch("global::ServerConsole")]
    public class ServerNamePatch : ServerConsole
    {
        public string RefreshServerName()
        {
            ServerConsole._serverName += $"<color=#00000000><size=1>PluginSystem {PluginManager.ApiVersion.ToString(3)}</size></color>";
            return this.NameFormatter.ProcessExpression(ServerConsole._serverName);
        }
    }
}
