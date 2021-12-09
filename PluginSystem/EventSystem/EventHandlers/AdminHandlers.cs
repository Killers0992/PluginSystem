using PluginSystem.Events;

namespace PluginSystem.EventHandlers
{
	public interface IEventHandlerBan : IEventHandler
	{
		void OnBan(BanEvent ev);
	}
}
