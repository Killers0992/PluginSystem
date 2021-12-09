using PluginSystem.API;

namespace PluginSystem.Permissions
{
    public interface IPermissionsHandler
    {
        short CheckPermission(Player player, string permissionName);
    }
}
