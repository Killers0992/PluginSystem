using MonoMod;
using PluginSystem.EventHandlers;
using PluginSystem.Events;
using UnityEngine;

namespace PluginSystem.Patcher.Patches
{
    [MonoModPatch("global::BanPlayer")]
    public class BanUserPatch : BanPlayer
    {
        public extern bool orig_BanUser(GameObject user, long duration, string reason, string issuer, bool isGlobalBan);

        public bool BanUser(GameObject user, long duration, string reason, string issuer, bool isGlobalBan)
        {
            var plr = SLPlayer.GetOrAdd(user);

            BanEvent ev = new BanEvent()
            {
                Player = plr,
                Duration = duration,
                AllowBan = true,
                Issuer = issuer,
                Reason = reason,
                Result = ""
            };
            EventManager.Manager.HandleEvent<IEventHandlerBan>(ev);

            if (!ev.AllowBan)
                return false;

            return orig_BanUser(user, duration, reason, issuer, isGlobalBan); 
        }
    }
}
