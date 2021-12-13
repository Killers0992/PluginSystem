using PluginSystem.API;
using PluginSystem.EventHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginSystem.Events
{
    public abstract class PlayerEvent : Event
    {
        protected PlayerEvent(Player player)
        {
            this.Player = player;
        }

        public Player Player { get; }
    }

    public class PlayerHurtEvent : PlayerEvent
    {
        public PlayerHurtEvent(Player player, Player attacker, float damage, DamageType damageType) : base(player)
        {
            this.Attacker = attacker;
            Damage = damage;
            DamageType = damageType;
        }

        public Player Attacker { get; }
        public float Damage { get; set; }
        public DamageType DamageType { get; set; }

        public override void ExecuteHandler(IEventHandler handler)
        {
            ((IEventHandlerPlayerHurt)handler).OnPlayerHurt(this);
        }
    }

    public class PlayerDeathEvent : PlayerEvent
    {
        public PlayerDeathEvent(Player player, Player killer, bool spawnRagdoll, DamageType damageType) : base(player)
        {
            Killer = killer;
            SpawnRagdoll = spawnRagdoll;
            DamageTypeVar = damageType;
        }

        public Player Killer { get; }
        public bool SpawnRagdoll { get; set; }
        public DamageType DamageTypeVar { get; set; }

        public override void ExecuteHandler(IEventHandler handler)
        {
            ((IEventHandlerPlayerDie)handler).OnPlayerDie(this);
        }
    }

    public class PlayerDropAllItemsEvent : PlayerEvent
    {
        public bool Allow { get; set; }

        public PlayerDropAllItemsEvent(Player player, bool allow = true) : base(player)
        {
            this.Allow = allow;
        }

        public override void ExecuteHandler(IEventHandler handler)
        {
            ((IEventHandlerPlayerDropAllItems)handler).OnPlayerDropAllItems(this);
        }
    }

    public class PlayerJoinEvent : PlayerEvent
    {
        public PlayerJoinEvent(Player player) : base(player)
        {
        }

        public override void ExecuteHandler(IEventHandler handler)
        {
            ((IEventHandlerPlayerJoin)handler).OnPlayerJoin(this);
        }
    }

    public class PlayerNicknameSetEvent : PlayerEvent
    {
        public PlayerNicknameSetEvent(Player player, string nickname) : base(player)
        {
            Nickname = nickname;
        }

        public string Nickname { get; set; }

        public override void ExecuteHandler(IEventHandler handler)
        {
            ((IEventHandlerNicknameSet)handler).OnNicknameSet(this);
        }
    }

    public class PlayerInitialAssignTeamEvent : PlayerEvent
    {
        public PlayerInitialAssignTeamEvent(Player player, TeamType team) : base(player)
        {
            Team = team;
        }

        public TeamType Team { get; set; }

        public override void ExecuteHandler(IEventHandler handler)
        {
            ((IEventHandlerInitialAssignTeam)handler).OnAssignTeam(this);
        }
    }

    public class PlayerCheckEscapeEvent : PlayerEvent
    {
        public bool AllowEscape { get; set; }
        public IRole ChangeRole { get; set; }

        public PlayerCheckEscapeEvent(Player player) : base(player)
        {
        }

        public override void ExecuteHandler(IEventHandler handler)
        {
            ((IEventHandlerCheckEscape)handler).OnCheckEscape(this);
        }
    }

    public class PlayerSpawnEvent : PlayerEvent
    {
        public Vector SpawnPos { get; set; }
        public PlayerSpawnEvent(Player player) : base(player)
        {
        }

        public override void ExecuteHandler(IEventHandler handler)
        {
            ((IEventHandlerSpawn)handler).OnSpawn(this);
        }
    }

    public class PlayerIntercomEvent : PlayerEvent
    {
        public float SpeechTime { get; set; }
        public float CooldownTime { get; set; }

        public PlayerIntercomEvent(Player player, float speechTime, float cooldownTime) : base(player)
        {
            SpeechTime = speechTime;
            CooldownTime = cooldownTime;
        }

        public override void ExecuteHandler(IEventHandler handler)
        {
            ((IEventHandlerIntercom)handler).OnIntercom(this);
        }
    }

    public class PlayerIntercomCooldownCheckEvent : PlayerEvent
    {
        public float CurrentCooldown { get; set; }

        public PlayerIntercomCooldownCheckEvent(Player player, float currCooldownTime) : base(player)
        {
            CurrentCooldown = currCooldownTime;
        }

        public override void ExecuteHandler(IEventHandler handler)
        {
            ((IEventHandlerIntercomCooldownCheck)handler).OnIntercomCooldownCheck(this);
        }
    }

    public class PlayerPocketDimensionExitEvent : PlayerEvent
    {
        public Vector ExitPosition { get; set; }

        public PlayerPocketDimensionExitEvent(Player player, Vector exitPosition) : base(player)
        {
            ExitPosition = exitPosition;
        }

        public override void ExecuteHandler(IEventHandler handler)
        {
            ((IEventHandlerPocketDimensionExit)handler).OnPocketDimensionExit(this);
        }
    }

    public class PlayerPocketDimensionEnterEvent : PlayerEvent
    {
        public float Damage { get; set; }
        public Vector LastPosition { get; }
        public Vector TargetPosition { get; set; }
        public Player Attacker { get; }

        public bool Allow { get; set; }

        public PlayerPocketDimensionEnterEvent(Player player, float damage, Vector lastPosition, Vector targetPosition, Player attacker, bool allow) : base(player)
        {
            Damage = damage;
            LastPosition = lastPosition;
            TargetPosition = targetPosition;
            Attacker = attacker;
            Allow = allow;
        }

        public override void ExecuteHandler(IEventHandler handler)
        {
            ((IEventHandlerPocketDimensionEnter)handler).OnPocketDimensionEnter(this);
        }
    }

    public class PlayerPocketDimensionDieEvent : PlayerEvent
    {
        public bool Die { get; set; }

        public PlayerPocketDimensionDieEvent(Player player, bool die) : base(player)
        {
            Die = die;
        }

        public override void ExecuteHandler(IEventHandler handler)
        {
            ((IEventHandlerPocketDimensionDie)handler).OnPocketDimensionDie(this);
        }
    }

    public class PlayerInfectedEvent : PlayerEvent
    {
        public float Damage { get; set; }
        public Player Attacker { get; }
        public float InfectTime { get; set; }

        public PlayerInfectedEvent(Player player, float damage, Player attacker, float infectTime) : base(player)
        {
            this.Damage = damage;
            this.Attacker = attacker;
            this.InfectTime = infectTime;
        }

        public override void ExecuteHandler(IEventHandler handler)
        {
            ((IEventHandlerInfected)handler).OnPlayerInfected(this);
        }
    }

    public class PlayerSpawnRagdollEvent : PlayerEvent
    {
        public IRole Role { get; set; }
        public Vector Position { get; set; }
        public Vector Rotation { get; set; }
        public Player Attacker { get; }
        public DamageType DamageType { get; set; }
        public bool AllowRecall { get; set; }

        public PlayerSpawnRagdollEvent(Player player, IRole role, Vector position, Vector rotation, Player attacker, DamageType damageType, bool allowRecall) : base(player)
        {
            this.Role = role;
            this.Position = position;
            this.Rotation = rotation;
            this.Attacker = attacker;
            this.DamageType = damageType;
            this.AllowRecall = allowRecall;
        }

        public override void ExecuteHandler(IEventHandler handler)
        {
            ((IEventHandlerSpawnRagdoll)handler).OnSpawnRagdoll(this);
        }
    }

    public class PlayerLureEvent : PlayerEvent
    {
        public bool AllowContain { get; set; }

        public PlayerLureEvent(Player player, bool allowContain) : base(player)
        {
            this.AllowContain = allowContain;
        }

        public override void ExecuteHandler(IEventHandler handler)
        {
            ((IEventHandlerLure)handler).OnLure(this);
        }
    }

    public class PlayerContain106Event : PlayerEvent
    {
        public Player[] SCP106s { get; }
        public bool ActivateContainment { get; set; }

        public PlayerContain106Event(Player player, Player[] scp106s, bool activateContainment) : base(player)
        {
            this.SCP106s = scp106s;
            this.ActivateContainment = activateContainment;
        }

        public override void ExecuteHandler(IEventHandler handler)
        {
            ((IEventHandlerContain106)handler).OnContain106(this);
        }
    }

    public class PlayerMedicalUseEvent : PlayerEvent
    {
        public int AmountHealth { get; set; }
        public int AmountArtificial { get; set; }
        public int AmountRegen { get; }
        public ItemType MedicalItem { get; }

        public PlayerMedicalUseEvent(Player player, int amountHealth, int artificalHP, int TotalhpRegenerated, ItemType item) : base(player)
        {
            this.AmountHealth = amountHealth;
            this.AmountArtificial = artificalHP;
            this.AmountRegen = TotalhpRegenerated;
            this.MedicalItem = item;
        }

        public override void ExecuteHandler(IEventHandler handler)
        {
            ((IEventHandlerMedicalUse)handler).OnMedicalUse(this);
        }
    }

    public class Player106CreatePortalEvent : PlayerEvent
    {
        public Vector Position { get; set; }

        public Player106CreatePortalEvent(Player player, Vector position) : base(player)
        {
            this.Position = position;
        }

        public override void ExecuteHandler(IEventHandler handler)
        {
            ((IEventHandler106CreatePortal)handler).On106CreatePortal(this);
        }
    }

    public class Player106TeleportEvent : PlayerEvent
    {
        public Vector Position { get; set; }

        public Player106TeleportEvent(Player player, Vector position) : base(player)
        {
            this.Position = position;
        }

        public override void ExecuteHandler(IEventHandler handler)
        {
            ((IEventHandler106Teleport)handler).On106Teleport(this);
        }
    }

    public class PlayerHandcuffedEvent : PlayerEvent
    {
        public bool Allow { get; set; }

        public Player Owner { get; set; }

        public PlayerHandcuffedEvent(Player player, Player owner, bool allow = true) : base(player)
        {
            this.Allow = allow;
            this.Owner = owner;
        }

        public override void ExecuteHandler(IEventHandler handler)
        {
            ((IEventHandlerHandcuffed)handler).OnHandcuffed(this);
        }
    }


    public class PlayerSCP914ChangeKnobEvent : PlayerEvent
    {
        public Scp914KnobSetting KnobSetting { get; set; }

        public PlayerSCP914ChangeKnobEvent(Player player, Scp914KnobSetting knobSetting) : base(player)
        {
            this.KnobSetting = knobSetting;
        }

        public override void ExecuteHandler(IEventHandler handler)
        {
            ((IEventHandlerSCP914ChangeKnob)handler).OnSCP914ChangeKnob(this);
        }
    }

    public class PlayerMakeNoiseEvent : PlayerEvent
    {
        public PlayerMakeNoiseEvent(Player player) : base(player)
        {
        }

        public override void ExecuteHandler(IEventHandler handler)
        {
            ((IEventHandlerMakeNoise)handler).OnMakeNoise(this);
        }
    }

    public class PlayerRecallZombieEvent : PlayerEvent
    {
        public Player Target { get; }

        public bool AllowRecall { get; set; }

        public PlayerRecallZombieEvent(Player player, Player target, bool allowRecall) : base(player)
        {
            this.Target = target;
            this.AllowRecall = allowRecall;
        }

        public override void ExecuteHandler(IEventHandler handler)
        {
            ((IEventHandlerRecallZombie)handler).OnRecallZombie(this);
        }
    }

    public class PlayerCallCommandEvent : PlayerEvent
    {
        public string ReturnMessage { get; set; }
        public string Command { get; }
        public PlayerCallCommandEvent(Player player, string command, string returnMessage) : base(player)
        {
            this.ReturnMessage = returnMessage;
            this.Command = command;
        }

        public override void ExecuteHandler(IEventHandler handler)
        {
            ((IEventHandlerCallCommand)handler).OnCallCommand(this);
        }
    }

    public class Player079LevelUpEvent : PlayerEvent
    {
        public Player079LevelUpEvent(Player player) : base(player) { }

        public override void ExecuteHandler(IEventHandler handler)
        {
            ((IEventHandler079LevelUp)handler).On079LevelUp(this);
        }
    }

    public class Player079UnlockDoorsEvent : PlayerEvent
    {
        public bool Allow { get; set; }

        public Player079UnlockDoorsEvent(Player player, bool allow) : base(player)
        {
            Allow = allow;
        }

        public override void ExecuteHandler(IEventHandler handler)
        {
            ((IEventHandler079UnlockDoors)handler).On079UnlockDoors(this);
        }
    }

    public class Player079CameraTeleportEvent : PlayerEvent
    {
        public Vector Camera { get; set; }
        public bool Allow { get; set; }
        public float APDrain { get; set; }

        public Player079CameraTeleportEvent(Player player, Vector camera, bool allow, float apDrain) : base(player)
        {
            Camera = camera;
            Allow = allow;
            APDrain = apDrain;
        }

        public override void ExecuteHandler(IEventHandler handler)
        {
            ((IEventHandler079CameraTeleport)handler).On079CameraTeleport(this);
        }
    }

    public class Scp096PanicEvent : PlayerEvent
    {
        public bool Allow { get; set; }
        public float PanicTime { get; set; }

        public Scp096PanicEvent(Player player, bool allow, float panicTime) : base(player)
        {
            Allow = allow;
            PanicTime = panicTime;
        }

        public override void ExecuteHandler(IEventHandler handler)
        {
            ((IEventHandlerScp096Panic)handler).OnScp096Panic(this);
        }
    }

    public class Scp096EnrageEvent : PlayerEvent
    {
        public bool Allow { get; set; }

        public Scp096EnrageEvent(Player player, bool allow) : base(player)
        {
            Allow = allow;
        }

        public override void ExecuteHandler(IEventHandler handler)
        {
            ((IEventHandlerScp096Enrage)handler).OnScp096Enrage(this);
        }
    }

    public class Scp096CooldownStartEvent : PlayerEvent
    {
        public bool Allow { get; set; }

        public Scp096CooldownStartEvent(Player player, bool allow) : base(player)
        {
            Allow = allow;
        }

        public override void ExecuteHandler(IEventHandler handler)
        {
            ((IEventHandlerScp096CooldownStart)handler).OnScp096CooldownStart(this);
        }
    }

    public class Scp096CooldownEndEvent : PlayerEvent
    {
        public bool Allow { get; set; }

        public Scp096CooldownEndEvent(Player player, bool allow) : base(player)
        {
            Allow = allow;
        }

        public override void ExecuteHandler(IEventHandler handler)
        {
            ((IEventHandlerScp096CooldownEnd)handler).OnScp096CooldownEnd(this);
        }
    }

    public class Scp096AddTargetEvent : PlayerEvent
    {
        public bool Allow { get; set; }
        public Player Target { get; }

        public Scp096AddTargetEvent(Player scp096, Player target, bool allow = true) : base(scp096)
        {
            Allow = allow;
            Target = target;
        }

        public override void ExecuteHandler(IEventHandler handler)
        {
            ((IEventHandlerScp096AddTarget)handler).OnScp096AddTarget(this);
        }
    }

    public sealed class PlayerLockerAccessEvent : PlayerEvent
    {
        public byte LockerId { get; }
        public byte ChamberId { get; }
        /// <summary>
        ///	Is the same permissions for items.
        /// </summary>
        public string ChamberAccessToken { get; }
        /// <summary>
        ///	true if the player is opening the locker; otherwise, false.
        /// </summary>
        public bool IsOpening { get; }
        public bool Allow { get; set; }

        public PlayerLockerAccessEvent(Player ply,
            byte lockerId, byte chamberId, string chamberAccessToken,
             bool isOpening, bool allow) : base(ply)
        {
            LockerId = lockerId;
            ChamberId = chamberId;
            ChamberAccessToken = chamberAccessToken;
            IsOpening = isOpening;
            Allow = allow;
        }

        public override void ExecuteHandler(IEventHandler handler)
        {
            ((IEventHandlerPlayerLockerAccess)handler).OnPlayerLockerAccess(this);
        }
    }
}
