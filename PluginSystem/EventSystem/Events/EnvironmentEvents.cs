using PluginSystem.API;
using PluginSystem.EventHandlers;
using System.Collections.Generic;

namespace PluginSystem.Events
{
    public class SCP914ActivateEvent : Event
    {
        public Player User { get; }
        public Scp914KnobSetting KnobSetting { get; set; }
        public List<Player> PlayerInputs { get; set; }
        public Vector IntakePos { get; set; }
        public Vector OutputPos { get; set; }

        public SCP914ActivateEvent(Player user, Scp914KnobSetting knobSetting, List<Player> playerList, Vector intakePos, Vector outputPos)
        {
            this.User = user;
            this.KnobSetting = knobSetting;
            this.PlayerInputs = playerList;
            this.IntakePos = intakePos;
            this.OutputPos = outputPos;
        }

        public override void ExecuteHandler(IEventHandler handler)
        {
            ((IEventHandlerSCP914Activate)handler).OnSCP914Activate(this);
        }
    }

    public abstract class WarheadEvent : Event
    {
        public WarheadEvent(Player player, float timeLeft)
        {
            this.player = player;
            this.TimeLeft = timeLeft;
            this.Cancel = false;
        }

        public float TimeLeft { get; set; }
        private Player player;
        public Player Activator { get => player; }
        public bool Cancel { get; set; }
    }

    public class WarheadStartEvent : WarheadEvent
    {
        public WarheadStartEvent(Player activator, float timeLeft, bool isResumed, bool openDoorsAfter) : base(activator, timeLeft)
        {
            IsResumed = isResumed;
            OpenDoorsAfter = openDoorsAfter;
        }

        public bool IsResumed { get; set; }
        public bool OpenDoorsAfter { get; set; }
        public override void ExecuteHandler(IEventHandler handler)
        {
            ((IEventHandlerWarheadStartCountdown)handler).OnStartCountdown(this);
        }
    }

    public class WarheadStopEvent : WarheadEvent
    {
        public WarheadStopEvent(Player player, float timeLeft) : base(player, timeLeft)
        {
        }

        public override void ExecuteHandler(IEventHandler handler)
        {
            ((IEventHandlerWarheadStopCountdown)handler).OnStopCountdown(this);
        }
    }

    public class WarheadChangeLeverEvent : Event
    {
        public Player Player { get; }
        public bool Allow { get; set; }

        public WarheadChangeLeverEvent(Player player)
        {
            Player = player;
            Allow = true;
        }

        public override void ExecuteHandler(IEventHandler handler)
        {
            ((IEventHandlerWarheadChangeLever)handler).OnChangeLever(this);
        }
    }

    public class WarheadKeycardAccessEvent : Event
    {
        public Player Player { get; }
        public bool Allow { get; set; }

        public WarheadKeycardAccessEvent(Player player, bool allow)
        {
            Player = player;
            Allow = allow;
        }

        public override void ExecuteHandler(IEventHandler handler)
        {
            ((IEventHandlerWarheadKeycardAccess)handler).OnWarheadKeycardAccess(this);
        }
    }

    public class WarheadDetonateEvent : Event
    {
        public override void ExecuteHandler(IEventHandler handler)
        {
            ((IEventHandlerWarheadDetonate)handler).OnDetonate();
        }
    }

    public class LCZDecontaminateEvent : Event
    {
        public override void ExecuteHandler(IEventHandler handler)
        {
            ((IEventHandlerLCZDecontaminate)handler).OnDecontaminate();
        }
    }

    public class SummonVehicleEvent : Event
    {
        public bool IsCI { get; set; }

        public bool AllowSummon { get; set; }

        public SummonVehicleEvent(bool isCI, bool allowSummon)
        {
            this.IsCI = isCI;
            this.AllowSummon = allowSummon;
        }

        public override void ExecuteHandler(IEventHandler handler)
        {
            ((IEventHandlerSummonVehicle)handler).OnSummonVehicle(this);
        }
    }


    public class ScpDeathAnnouncementEvent : Event
    {
        public bool ShouldPlay { get; set; }
        public Player DeadPlayer { get; }
        public IRole PlayerRole { get; }

        public ScpDeathAnnouncementEvent(bool shouldPlay, Player deadPlayer, IRole playerRole)
        {
            ShouldPlay = shouldPlay;
            DeadPlayer = deadPlayer;
            PlayerRole = playerRole;
        }

        public override void ExecuteHandler(IEventHandler handler)
        {
            ((IEventHandlerScpDeathAnnouncement)handler).OnScpDeathAnnouncement(this);
        }
    }

    public class CassieCustomAnnouncementEvent : Event
    {
        public string Words { get; set; }
        public bool MonoSpaced { get; set; }
        public bool Allow { get; set; }

        public CassieCustomAnnouncementEvent(string words, bool monospaced, bool allow = true)
        {
            this.Words = words;
            this.MonoSpaced = monospaced;
            this.Allow = allow;
        }

        public override void ExecuteHandler(IEventHandler handler)
        {
            ((IEventHandlerCassieCustomAnnouncement)handler).OnCassieCustomAnnouncement(this);
        }
    }

    public class CassieTeamAnnouncementEvent : Event
    {
        public char NatoLetter { get; set; }
        public int NatoNumber { get; set; }
        public int SCPsLeft { get; set; }
        public bool Allow { get; set; }

        public CassieTeamAnnouncementEvent(char natoLetter, int natoNumber, int scpsLeft, bool allow = true)
        {
            this.NatoLetter = natoLetter;
            this.NatoNumber = natoNumber;
            this.SCPsLeft = scpsLeft;
            this.Allow = allow;
        }

        public override void ExecuteHandler(IEventHandler handler)
        {
            ((IEventHandlerCassieTeamAnnouncement)handler).OnCassieTeamAnnouncement(this);
        }
    }
}
