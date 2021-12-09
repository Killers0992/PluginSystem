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
        private Map _map;
        private Round _round;

        public SLServer(Map map, Round round)
        {
            _map = map;
            _round = Round;
        }

        public override string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override int Port => (int)ServerStatic.ServerPort;

        public override string IpAddress => ServerConsole.Ip;

        public override Round Round => _round;

        public override Map Map => _map;

        public override int NumPlayers => ServerConsole.PlayersAmount;

        public override int MaxPlayers
        {
            get
            {
                return CustomNetworkManager.slots;
            }
            set
            {
                CustomNetworkManager.slots = value;
            }
        }
        
        public override string PlayerListTitle
        {
            get
            {
                return PlayerList.Title.Value;
            }
            set
            {
                PlayerList.Title.Value = value;
            }
        }

        public override bool BanIpAddress(string username, string ipAddress, int duration, string reason = "", string issuer = "Server")
        {
            long issuanceTime = TimeBehaviour.CurrentTimestamp();
            long banExpirationTime = TimeBehaviour.GetBanExpirationTime((uint)duration);

            return BanHandler.IssueBan(new BanDetails
            {
                OriginalName = username,
                Id = ipAddress,
                IssuanceTime = issuanceTime,
                Expires = banExpirationTime,
                Reason = reason,
                Issuer = issuer
            }, BanHandler.BanType.IP);
        }

        public override bool BanUserId(string username, string userId, int duration, string reason = "", string issuer = "Server")
        {
            long issuanceTime = TimeBehaviour.CurrentTimestamp();
            long banExpirationTime = TimeBehaviour.GetBanExpirationTime((uint)duration);

            return BanHandler.IssueBan(new BanDetails
            {
                OriginalName = username,
                Id = userId,
                IssuanceTime = issuanceTime,
                Expires = banExpirationTime,
                Reason = reason,
                Issuer = issuer
            }, BanHandler.BanType.UserId);
        }

        public override string GetAppFolder(bool addSeparator = true, bool serverConfig = false, string centralConfig = "")
        {
            throw new NotImplementedException();
        }

        public override List<Connection> GetConnections(string filter = "")
        {
            throw new NotImplementedException();
        }

        public override Player GetPlayerById(int playerId)
        {
            return SLPlayer.GetPlayerById(playerId);
        }

        public override Player GetPlayerByUserId(string userId)
        {
            return SLPlayer.GetPlayerByUserId(userId);
        }

        public override Player GetPlayerByUsername(string username)
        {
            return SLPlayer.GetPlayerByUsername(username);
        }

        public override List<Player> GetPlayers()
        {
            return SLPlayer.GetPlayers();
        }

        public override List<Player> GetPlayers(string filter)
        {
            return SLPlayer.GetPlayers(filter);
        }

        public override List<Player> GetPlayers(IRole role)
        {
            return SLPlayer.GetPlayers(role);
        }

        public override List<Player> GetPlayers(TeamType team)
        {
            return SLPlayer.GetPlayers(team);
        }

        public override List<Player> GetPlayers(Predicate<Player> predicate)
        {
            return SLPlayer.GetPlayers(predicate);
        }

        public override List<TeamType> GetRoles(string filter = "")
        {
            return SLPlayer.GetRoles(filter);
        }
    }
}
