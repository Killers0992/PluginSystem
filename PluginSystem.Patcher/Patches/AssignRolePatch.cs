using MonoMod;

namespace PluginSystem.Patcher.Patches
{
    [MonoModPatch("global::PlayerStatsSystem.PlayerStats")]
    public class AssignRolePatch
    {
        public extern void orig_OnClassChanged(ReferenceHub userHub, RoleType prevClass, RoleType newClass);

        private void OnClassChanged(ReferenceHub userHub, RoleType prevClass, RoleType newClass)
        {
            var plr = SLPlayer.GetOrAdd(userHub);

            if (plr.Role != null)
                plr.Role.OnPlayerStopUsingRole(plr);

            if (RoleManager.Manager.RegisteredRoles.TryGetValue((int)newClass, out IRole role))
            {
                plr.Role = role;
                plr.Role.OnPlayerStartUsingRole(plr);
            }

            orig_OnClassChanged(userHub, prevClass, newClass);
        }
    }
}
