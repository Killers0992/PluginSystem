using MonoMod;
using PluginSystem.EventHandlers;
using PluginSystem.Events;

namespace PluginSystem.Patcher.Patches
{
    [MonoModPatch("global::ServerRoles")]
    public class PlayerJoinedPatch : ServerRoles
	{
		public extern void orig_RefreshPermissions(bool disp = false);

		public void RefreshPermissions(bool disp = false)
		{
			orig_RefreshPermissions(disp);

			PlayerJoinEvent ev = new PlayerJoinEvent(SLPlayer.GetOrAdd(this._hub));
			EventManager.Manager.HandleEvent<IEventHandlerPlayerJoin>(ev);
		}
	}
}
