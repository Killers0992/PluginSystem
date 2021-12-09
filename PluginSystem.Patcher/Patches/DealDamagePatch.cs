using MonoMod;
using PlayerStatsSystem;
using PluginSystem.EventHandlers;
using PluginSystem.Events;

namespace PluginSystem.Patcher.Patches
{
    [MonoModPatch("global::PlayerStatsSystem.PlayerStats")]
    public class DealDamagePatch : PlayerStats
    {
        public extern bool orig_DealDamage(DamageHandlerBase handler);

        public bool DealDamage(DamageHandlerBase handler)
        {
            switch (handler)
            {
                case UniversalDamageHandler uniHandler:
                    if (uniHandler.TranslationId == DeathTranslations.UsedAs106Bait.Id)
                    {
                        var plr = SLPlayer.GetOrAdd(_hub);

                        PlayerLureEvent playerLureEvent = new PlayerLureEvent(plr, true);
                        EventManager.Manager.HandleEvent<IEventHandlerLure>(playerLureEvent);

                        PluginSystem.LureCanceled = !playerLureEvent.AllowContain;

                        if (!playerLureEvent.AllowContain)
                            return false;
                    }
                    break;
            }

            return orig_DealDamage(handler);
        }
    }
}
