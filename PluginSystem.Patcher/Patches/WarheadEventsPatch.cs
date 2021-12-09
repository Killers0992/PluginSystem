using MonoMod;
using PluginSystem.EventHandlers;
using PluginSystem.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PluginSystem.Patcher.Patches
{
    [MonoModPatch("global::AlphaWarheadController")]
    public class WarheadEventsPatch : AlphaWarheadController
    {
        public extern void orig_CancelDetonation(GameObject disabler);
        public extern void orig_StartDetonation(bool automatic = false);

        public void CancelDetonation(GameObject disabler)
        {
            var plr = SLPlayer.GetOrAdd(disabler);

            WarheadStopEvent ev = new WarheadStopEvent(plr, this.timeToDetonation);

            ev.Cancel = this.timeToDetonation <= 10f;

            EventManager.Manager.HandleEvent<IEventHandlerWarheadStopCountdown>(ev);

            if (ev.Cancel)
                return;

            orig_CancelDetonation(disabler);
        }

        public void StartDetonation(bool automatic = false)
        {
            var plr = SLPlayer.GetOrAdd(PluginSystem.LastStartedDetonationBy);

            WarheadStartEvent ev = new WarheadStartEvent(plr, timeToDetonation, _resumeScenario != -1, true);

            ev.Cancel = !(AlphaWarheadController._resumeScenario == -1 && Math.Abs(this.scenarios_start[AlphaWarheadController._startScenario].SumTime() - this.timeToDetonation) < 0.0001f) || (AlphaWarheadController._resumeScenario != -1 && Math.Abs(this.scenarios_resume[AlphaWarheadController._resumeScenario].SumTime() - this.timeToDetonation) < 0.0001f);

            EventManager.Manager.HandleEvent<IEventHandlerWarheadStartCountdown>(ev);

            if (ev.Cancel)
                return;

            orig_StartDetonation(automatic);
        }
    }
}
