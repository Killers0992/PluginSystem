using MonoMod;
using PlayerStatsSystem;

namespace PluginSystem.Patcher.Patches
{
    [MonoModPatch("global::PlayerStatsSystem.HealthStat")]
    public class HealthStatPatch : HealthStat
    {
        public override float MaxValue
        {
            get
            {
                return SLPlayer.GetOrAdd(base.Hub).Role.MaxHealth;
            }
        }
    }
}
