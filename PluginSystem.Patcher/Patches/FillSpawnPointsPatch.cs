using MonoMod;
using PluginSystem.EventHandlers;
using PluginSystem.Events;

namespace PluginSystem.Patcher.Patches
{
	[MonoModPatch("global::SpawnpointManager")]
	public class FillSpawnPointsPatch
    {
		public extern static void orig_FillSpawnPoints();

		public static void FillSpawnPoints()
		{
			WaitingForPlayersEvent ev = new WaitingForPlayersEvent(PluginManager.Manager.Server);
			EventManager.Manager.HandleEvent<IEventHandlerWaitingForPlayers>(ev);
			orig_FillSpawnPoints();
		}
	}
}
