using MonoMod;
using PluginSystem.EventHandlers;
using PluginSystem.Events;
using UnityEngine;

namespace PluginSystem.Patcher.Patches
{
    [MonoModPatch("global::ReferenceHub")]
    public class PlayerLeavePatch : MonoBehaviour
	{
		public extern void orig_OnDestroy();

		private void OnDestroy()
		{
			var plr = SLPlayer.GetOrAdd(base.gameObject);

			PlayerLeaveEvent ev = new PlayerLeaveEvent(plr);
			EventManager.Manager.HandleEvent<IEventHandlerPlayerLeave>(ev);

			plr.OnDestroy();
			orig_OnDestroy();
		}
	}
}