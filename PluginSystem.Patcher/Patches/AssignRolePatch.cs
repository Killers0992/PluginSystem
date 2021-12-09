using MonoMod;

namespace PluginSystem.Patcher.Patches
{
    [MonoModPatch("global::PlayerStatsSystem.PlayerStats")]
    public class AssignRolePatch
    {
        public extern void orig_OnClassChanged(ReferenceHub userHub, RoleType prevClass, RoleType newClass);

        private void OnClassChanged(ReferenceHub userHub, RoleType prevClass, RoleType newClass)
        {
            if (RoleManager.Manager.RegisteredRoles.TryGetValue((int)newClass, out IRole role))
            {
                var plr = SLPlayer.GetOrAdd(userHub);
                plr.Role = role;
            }

            orig_OnClassChanged(userHub, prevClass, newClass);
        }
    }
}
