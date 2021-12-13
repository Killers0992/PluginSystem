using PluginSystem.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginSystem.EventHandlers
{
    public interface IEventHandlerPlayerHurt : IEventHandler
    {

        /// <summary>  
        /// This is called before the player is going to take damage.
        /// In case the attacker can't be passed, attacker will be null (fall damage etc)
        /// This may be broken into two events in the future
        /// </summary> 
        void OnPlayerHurt(PlayerHurtEvent ev);
    }

    public interface IEventHandlerPlayerDie : IEventHandler
    {
        /// <summary>  
        /// This is called before the player is about to die. Be sure to check if player is SCP106 (classID 3) and if so, set spawnRagdoll to false.
        /// In case the killer can't be passed, attacker will be null, so check for that before doing something.
        /// </summary> 
        void OnPlayerDie(PlayerDeathEvent ev);
    }

    public interface IEventHandlerPlayerDropAllItems : IEventHandler
    {
        /// <summary>  
        /// This is called when all of the items in a player's inventory are going to be dropped.
        /// </summary> 
        void OnPlayerDropAllItems(PlayerDropAllItemsEvent ev);
    }
    public interface IEventHandlerPlayerJoin : IEventHandler
    {
        /// <summary>  
        /// This is called when a player joins and is initialised.
        /// </summary> 
        void OnPlayerJoin(PlayerJoinEvent ev);
    }

    public interface IEventHandlerNicknameSet : IEventHandler
    {
        /// <summary>  
        /// This is called when a player attempts to set their nickname after joining. This will only be called once per game join.
        /// </summary> 
        void OnNicknameSet(PlayerNicknameSetEvent ev);
    }

    public interface IEventHandlerInitialAssignTeam : IEventHandler
    {
        /// <summary>  
        /// Called when a team is picked for a player. Nothing is assigned to the player, but you can change what team the player will spawn as.
        /// </summary>  
        void OnAssignTeam(PlayerInitialAssignTeamEvent ev);
    }

    public interface IEventHandlerCheckEscape : IEventHandler
    {
        /// <summary>  
        /// Called when a player is checking if they should escape (this is regardless of class)
        /// </summary>  
        void OnCheckEscape(PlayerCheckEscapeEvent ev);
    }

    public interface IEventHandlerSpawn : IEventHandler
    {
        /// <summary>  
        /// Called when a player spawns into the world
        /// </summary>  
        void OnSpawn(PlayerSpawnEvent ev);
    }

    public interface IEventHandlerIntercom : IEventHandler
    {
        /// <summary>  
        /// Called when a player attempts to use intercom.
        /// </summary>  
        void OnIntercom(PlayerIntercomEvent ev);
    }

    public interface IEventHandlerIntercomCooldownCheck : IEventHandler
    {
        /// <summary>  
        /// Called when a player attempts to use intercom. This happens before the cooldown check.
        /// </summary>  
        void OnIntercomCooldownCheck(PlayerIntercomCooldownCheckEvent ev);
    }

    public interface IEventHandlerPocketDimensionExit : IEventHandler
    {
        /// <summary>  
        /// Called when a player escapes from Pocket Demension
        /// </summary>  
        void OnPocketDimensionExit(PlayerPocketDimensionExitEvent ev);
    }

    public interface IEventHandlerPocketDimensionEnter : IEventHandler
    {
        /// <summary>  
        /// Called when a player enters Pocket Demension
        /// </summary>  
        void OnPocketDimensionEnter(PlayerPocketDimensionEnterEvent ev);
    }

    public interface IEventHandlerPocketDimensionDie : IEventHandler
    {
        /// <summary>  
        /// Called when a player enters the wrong way of Pocket Demension. This happens before the player is killed.
        /// </summary>  
        void OnPocketDimensionDie(PlayerPocketDimensionDieEvent ev);
    }

    public interface IEventHandlerInfected : IEventHandler
    {
        /// <summary>  
        /// Called when a player is cured by SCP-049
        /// </summary>  
        void OnPlayerInfected(PlayerInfectedEvent ev);
    }

    public interface IEventHandlerSpawnRagdoll : IEventHandler
    {
        /// <summary>  
        /// Called when a ragdoll is spawned
        /// </summary>  
        void OnSpawnRagdoll(PlayerSpawnRagdollEvent ev);
    }

    public interface IEventHandlerLure : IEventHandler
    {
        /// <summary>  
        /// Called when a player enters FemurBreaker
        /// </summary> 
        void OnLure(PlayerLureEvent ev);
    }

    public interface IEventHandlerContain106 : IEventHandler
    {
        /// <summary>  
        /// Called when a player presses the button to contain SCP-106
        /// </summary>
        void OnContain106(PlayerContain106Event ev);
    }

    public interface IEventHandlerMedicalUse : IEventHandler
    {
        /// <summary>  
        /// Called when a player uses Medkit
        /// </summary>
        void OnMedicalUse(PlayerMedicalUseEvent ev);
    }

    public interface IEventHandler106CreatePortal : IEventHandler
    {
        /// <summary>  
        /// Called when SCP-106 creates a portal
        /// </summary>
        void On106CreatePortal(Player106CreatePortalEvent ev);
    }

    public interface IEventHandler106Teleport : IEventHandler
    {
        /// <summary>  
        /// Called when SCP-106 teleports through portals
        /// </summary>
        void On106Teleport(Player106TeleportEvent ev);
    }

    public interface IEventHandlerHandcuffed : IEventHandler
    {
        /// <summary>  
        /// Called when a player is about to be handcuffed/released
        /// </summary>
        void OnHandcuffed(PlayerHandcuffedEvent ev);
    }

    public interface IEventHandlerSCP914ChangeKnob : IEventHandler
    {
        /// <summary>  
        /// Called when a player changes the knob of SCP-914
        /// </summary>
        void OnSCP914ChangeKnob(PlayerSCP914ChangeKnobEvent ev);
    }

    public interface IEventHandlerMakeNoise : IEventHandler
    {
        /// <summary>  
        /// Called when a player makes noise
        /// </summary>
        void OnMakeNoise(PlayerMakeNoiseEvent ev);
    }

    public interface IEventHandlerRecallZombie : IEventHandler
    {
        /// <summary>  
        /// Called when SCP-049 turns someone into a zombie
        /// </summary>
        void OnRecallZombie(PlayerRecallZombieEvent ev);
    }

    public interface IEventHandlerCallCommand : IEventHandler
    {
        /// <summary>  
        /// Called when a player uses a command that starts with .
        /// </summary>
        void OnCallCommand(PlayerCallCommandEvent ev);
    }

    public interface IEventHandler079LevelUp : IEventHandler
    {
        /// <summary>
        /// Called when a player's SCP-079 level is incremented.
        /// </summary>
        void On079LevelUp(Player079LevelUpEvent ev);
    }

    public interface IEventHandler079UnlockDoors : IEventHandler
    {
        /// <summary>
        /// Called when SCP-079 unlocks all doors.
        /// </summary>
        void On079UnlockDoors(Player079UnlockDoorsEvent ev);
    }

    public interface IEventHandler079CameraTeleport : IEventHandler
    {
        /// <summary>
        /// Called when SCP-079 teleports to a new camera.
        /// </summary>
        void On079CameraTeleport(Player079CameraTeleportEvent ev);
    }

    public interface IEventHandlerScp096Panic : IEventHandler
    {
        /// <summary>
        /// Called when SCP-096 enters panic mode.
        /// </summary>
        void OnScp096Panic(Scp096PanicEvent ev);
    }

    public interface IEventHandlerScp096Enrage : IEventHandler
    {
        /// <summary>
        /// Called when SCP-096 enters rage mode.
        /// </summary>
        void OnScp096Enrage(Scp096EnrageEvent ev);
    }

    public interface IEventHandlerScp096CooldownStart : IEventHandler
    {
        /// <summary>
        /// Called when SCP-096 enters cooldown.
        /// </summary>
        void OnScp096CooldownStart(Scp096CooldownStartEvent ev);
    }

    public interface IEventHandlerScp096CooldownEnd : IEventHandler
    {
        /// <summary>
        /// Called when SCP-096 exits cooldown.
        /// </summary>
        void OnScp096CooldownEnd(Scp096CooldownEndEvent ev);
    }

    public interface IEventHandlerScp096AddTarget : IEventHandler
    {
        /// <summary>
        /// Called when SCP-096 adds a target.
        /// </summary>
        void OnScp096AddTarget(Scp096AddTargetEvent ev);
    }

    public interface IEventHandlerPlayerLockerAccess : IEventHandler
    {
        /// <summary>
        ///	Called when the player interacts with the locker.
        /// </summary>
        void OnPlayerLockerAccess(PlayerLockerAccessEvent ev);
    }
}
