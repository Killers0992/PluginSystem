using PluginSystem.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginSystem
{
    public class SLServer : Server
    {
        public override string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override int Port => (int)ServerStatic.ServerPort;

        public override string IpAddress => ServerConsole.Ip;

        public override Round Round => throw new NotImplementedException();

        public override Map Map => throw new NotImplementedException();

        public override int NumPlayers => ServerConsole.PlayersAmount;

        public override int MaxPlayers { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override string PlayerListTitle { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override bool BanIpAddress(string username, string ipAddress, int duration, string reason = "", string issuer = "Server")
        {
            throw new NotImplementedException();
        }

        public override bool BanUserId(string username, string userId, int duration, string reason = "", string issuer = "Server")
        {
            throw new NotImplementedException();
        }

        public override string GetAppFolder(bool addSeparator = true, bool serverConfig = false, string centralConfig = "")
        {
            throw new NotImplementedException();
        }

        public override List<Connection> GetConnections(string filter = "")
        {
            throw new NotImplementedException();
        }

        public override Player GetPlayer(int playerId)
        {
            throw new NotImplementedException();
        }

        public override List<Player> GetPlayers()
        {
            throw new NotImplementedException();
        }

        public override List<Player> GetPlayers(string filter)
        {
            throw new NotImplementedException();
        }

        public override List<Player> GetPlayers(IRole role)
        {
            throw new NotImplementedException();
        }

        public override List<Player> GetPlayers(TeamType team)
        {
            throw new NotImplementedException();
        }

        public override List<Player> GetPlayers(Predicate<Player> predicate)
        {
            throw new NotImplementedException();
        }

        public override List<TeamType> GetRoles(string filter = "")
        {
            throw new NotImplementedException();
        }
    }
}
