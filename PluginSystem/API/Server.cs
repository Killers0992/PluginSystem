using System;
using System.Collections.Generic;

namespace PluginSystem.API
{
	public abstract class Server
	{
		public abstract string Name { get; set; }
		public abstract int Port { get; }
		public abstract string IpAddress { get; }
		public abstract Round Round { get; }
		public abstract Map Map { get; }
		public abstract int NumPlayers { get; }
		public abstract int MaxPlayers { get; set; }
		public abstract string PlayerListTitle { get; set; }

		public abstract List<Player> GetPlayers();
		public abstract List<Player> GetPlayers(string filter);
		public abstract List<Player> GetPlayers(IRole role);
		public abstract List<Player> GetPlayers(TeamType team);
		public abstract List<Player> GetPlayers(Predicate<Player> predicate);

		public abstract Player GetPlayerById(int playerId);
		public abstract Player GetPlayerByUserId(string userId);
		public abstract Player GetPlayerByUsername(string username);

		public abstract List<Connection> GetConnections(string filter = "");
		public abstract List<TeamType> GetRoles(string filter = "");
		public abstract string GetAppFolder(bool addSeparator = true, bool serverConfig = false, string centralConfig = "");
		public abstract bool BanUserId(string username, string userId, int duration, string reason = "", string issuer = "Server");
		public abstract bool BanIpAddress(string username, string ipAddress, int duration, string reason = "", string issuer = "Server");
	}
}
