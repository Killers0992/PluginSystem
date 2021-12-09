using Hints;
using InventorySystem.Disarming;
using PlayerStatsSystem;
using PluginSystem.API;
using PluginSystem.API.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PluginSystem
{
    public class SLPlayer : Player
    {
        private ReferenceHub _hub;
        public IRole CurrentRole = new RoleNone();

        private UserIdType? _useridtype;

        public static Dictionary<int, SLPlayer> PlayersIds = new Dictionary<int, SLPlayer>();
        public static Dictionary<string, SLPlayer> PlayersUserIds = new Dictionary<string, SLPlayer>();
        public static Dictionary<ReferenceHub, SLPlayer> Players = new Dictionary<ReferenceHub, SLPlayer>();

        public SLPlayer(ReferenceHub hub)
        {
            this._hub = hub;
            PlayersUserIds.Add(UserId, this);
            PlayersIds.Add(PlayerId, this);
        }

        public void OnDestroy()
        {
            PlayersUserIds.Remove(UserId);
            PlayersIds.Remove(PlayerId);
            Players.Remove(_hub);
        }

        public static SLPlayer GetOrAdd(ReferenceHub hub)
        {
            if (hub == null)
                return null;

            if (Players.TryGetValue(hub, out SLPlayer player))
                return player;

            Players.Add(hub, new SLPlayer(hub));
            return Players[hub];
        }

        public static SLPlayer GetOrAdd(GameObject gameObject)
        {
            if (gameObject == null)
                return null;

            if (!ReferenceHub.Hubs.TryGetValue(gameObject, out ReferenceHub hub))
                return null;

            if (Players.TryGetValue(hub, out SLPlayer player))
                return player;

            Players.Add(hub, new SLPlayer(hub));
            return Players[hub];
        }

        public static Player GetPlayerById(int playerId)
        {
            if (SLPlayer.PlayersIds.TryGetValue(playerId, out SLPlayer plr))
                return plr;

            return null;
        }

        public static Player GetPlayerByUserId(string userId)
        {
            if (userId == null)
                return null;

            if (SLPlayer.PlayersUserIds.TryGetValue(userId, out SLPlayer plr))
                return plr;

            return null;
        }

        public static Player GetPlayerByUsername(string userName)
        {
            return Players.Values.FirstOrDefault(p => p.Name.ToLower().Replace(" ", "") == userName.ToLower().Replace(" ", ""));
        }

        public static List<Player> GetPlayers()
        {
            return SLPlayer.Players.Values
                .ToList<Player>();
        }

        public static List<Player> GetPlayers(string filter)
        {
            return SLPlayer.Players.Values
                .ToList<Player>();
        }

        public static List<Player> GetPlayers(IRole role)
        {
            return SLPlayer.Players.Values
                .Where(p => p.Role.RoleId == role.RoleId)
                .ToList<Player>();
        }

        public static List<Player> GetPlayers(TeamType team)
        {
            return SLPlayer.Players.Values
                .Where(p => p.Role.Team == team)
                .ToList<Player>();
        }

        public static List<Player> GetPlayers(Predicate<Player> predicate)
        {
            List<Player> plrs = new List<Player>();
            foreach (var plr in GetPlayers())
            {
                if (predicate(plr))
                    plrs.Add(plr);
            }
            return plrs;
        }

        public static List<TeamType> GetRoles(string filter = "")
        {
            return null;
        }

        public override string Name => _hub.nicknameSync.MyNick;

        public override string DisplayNickname 
        {
            get
            {
                return _hub.nicknameSync.DisplayName;
            }
            set
            {
                _hub.nicknameSync.DisplayName = value;
            }
        }

        public override string IpAddress => _hub.characterClassManager.connectionToClient.address;

        public override int PlayerId => _hub.playerId;

        public override uint NetworkId => _hub.networkIdentity.netId;

        public override string UserId => _hub.characterClassManager.UserId;

        public override UserIdType UserIdType
        {
            get
            {
                if (_useridtype.HasValue)
                    return _useridtype.Value;

                _useridtype = (UserIdType)Enum.Parse(typeof(UserIdType), UserId.Substring(UserId.LastIndexOf("@"), UserId.Length - 1), true);
                
                return _useridtype.Value;
            }
        }

        public override IRole Role
        {
            get
            {
                return CurrentRole;
            }
            set
            {
                CurrentRole = value;
            }
        }
        
        
        public override bool Overwatch
        {
            get
            {
                return _hub.serverRoles.OverwatchEnabled;
            }
            set
            {
                _hub.serverRoles.OverwatchEnabled = value;
            }
        }

        public override bool DoNotTrack => _hub.serverRoles.DoNotTrack;

        public override float Health 
        {
            get
            {
                return _hub.playerStats.GetModule<HealthStat>().CurValue;
            }
            set
            {
                _hub.playerStats.GetModule<HealthStat>().CurValue = value;
            }
        }

        public override float MaxHealth
        {
            get
            {
                return Role.MaxHealth;
            }
            set
            {
                Role.MaxHealth = value;
            }
        } 

        public override float ArtificialHealth
        {
            get
            {
                return _hub.playerStats.GetModule<AhpStat>().CurValue;
            }
            set
            {
                _hub.playerStats.GetModule<AhpStat>().CurValue = value;
            }
        }

        public override float MaxArtificialHealth
        {
            get
            {
                return _hub.playerStats.GetModule<AhpStat>().MaxValue;
            }
            set
            {
                _hub.playerStats.GetModule<AhpStat>()._maxSoFar = value;
            }
        }

        public override float Stamina
        {
            get
            {
                return _hub.fpc.staminaController.RemainingStamina;
            }
            set
            {
                _hub.fpc.staminaController.RemainingStamina = value;
            }
        }

        public override bool IsMuted
        {
            get
            {
                return _hub.dissonanceUserSetup.AdministrativelyMuted;
            }
            set
            {
                _hub.dissonanceUserSetup.AdministrativelyMuted = value;
            }
        }

        public override bool IsIntercomMuted
        {
            get
            {
                return _hub.characterClassManager.IntercomMuted;
            }
            set
            {
                _hub.characterClassManager.IntercomMuted = value;
            }
        }

        public override bool BypassMode
        { 
            get
            {
                return _hub.serverRoles.BypassMode;
            }
            set
            {
                _hub.serverRoles.BypassMode = value;
            }
        }

        public override bool GodMode
        {
            get
            {
                return _hub.characterClassManager.GodMode;
            }
            set
            {
                _hub.characterClassManager.GodMode = value;
            }
        }

        public override bool Noclip
        {
            get
            {
                return _hub.serverRoles.NoclipReady;
            }
            set
            {
                _hub.serverRoles.NoclipReady = value;
            }
        }

        public override bool IsAlive => Role.Team != TeamType.Spectator;

        public override bool IsDead => Role.Team == TeamType.Spectator;

        public override bool IsCuffed => _hub.inventory.IsDisarmed();

        public override Player Cuffer
        {
            get
            {
                return null;
            }
            set
            {
                _hub.inventory.SetDisarmedStatus(null);
                DisarmedPlayers.Entries.Add(new DisarmedPlayers.DisarmedEntry(_hub.networkIdentity.netId, value.NetworkId));
            }
        } 

        public override ushort GetAmmo(API.ItemType type)
        {
            if (_hub.inventory.UserInventory.ReserveAmmo.TryGetValue((ItemType)type, out ushort val))
                return val;
            return 0;
        }

        public override void SetAmmo(API.ItemType type, ushort amount)
        {
            _hub.inventory.UserInventory.ReserveAmmo[(ItemType)type] = amount;
            _hub.inventory.ServerSendAmmo();
        }

        public override Vector GetRotation()
        {
            return new Vector(
                _hub.transform.eulerAngles.x,
                _hub.transform.eulerAngles.y,
                _hub.transform.eulerAngles.z);
        }

        public override Vector GetPosition()
        {
            return new Vector(
                _hub.transform.position.x,
                _hub.transform.position.y,
                _hub.transform.position.z);
        }

        public override void Teleport(Vector pos, bool unstuck = false)
        {
            _hub.playerMovementSync.OverridePosition(new UnityEngine.Vector3(pos.x, pos.y, pos.z), 0f, unstuck);
        }

        public override void SetRank(string color = null, string text = null, string group = null)
        {
            throw new System.NotImplementedException();
        }

        public override void ShowHint(string Text, float durationInSeconds = 1)
        {
            HintParameter[] parameters = new HintParameter[]
            {
                new StringHintParameter(Text),
            };

            _hub.hints.Show(new TextHint(Text, parameters, null, durationInSeconds));
        }

        public override string GetRankName()
        {
            return "";
        }

        public override void Disconnect()
        {
            ServerConsole.Disconnect(_hub.characterClassManager.connectionToClient, "Force Disconnect");
        }

        public override void Disconnect(string message)
        {
            ServerConsole.Disconnect(_hub.characterClassManager.connectionToClient, message);
        }

        public override void Ban(int duration)
        {
            ReferenceHub.HostHub.GetComponent<BanPlayer>().BanUser(_hub.gameObject, duration, "No Reason", "server");
        }

        public override void Ban(int duration, string message)
        {
            ReferenceHub.HostHub.GetComponent<BanPlayer>().BanUser(_hub.gameObject, duration, message, "server");
        }

        public override bool HasItem(API.ItemType type)
        {
            return false;
        }

        public override void ClearInventory()
        {
            _hub.inventory.UserInventory.Items.Clear();
            _hub.inventory.ServerSendItems();
        }

        public override object GetGameObject()
        {
            return _hub.gameObject;
        }

        public override void SendConsoleMessage(string message, string color = "green")
        {
            _hub.characterClassManager.TargetConsolePrint(_hub.characterClassManager.connectionToClient, message, color);
        }
    }
}
