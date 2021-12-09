using MonoMod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginSystem.Patcher.Patches
{
    [MonoModPatch("global::PlayerStatsSystem.PlayerStats")]
    public class PlayerStatsPatch
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
