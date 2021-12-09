using PluginSystem.API;
using PluginSystem.EventHandlers;

namespace PluginSystem.Events
{
	public class BanEvent : Event
	{
		public Player Player { get; set; }
		public string Issuer { get; set; }
		public int Duration { get; set; }
		public string Reason { get; set; }
		public string Result { get; set; }
		public bool AllowBan { get; set; }

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerBan)handler).OnBan(this);
		}
	}
}
