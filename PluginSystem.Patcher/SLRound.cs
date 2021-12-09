using GameCore;
using PluginSystem.API;
using Respawning;
using Respawning.NamingRules;
using RoundRestarting;
using System;

namespace PluginSystem
{
    public class SLRound : Round
    {
        public override int Duration => (int)RoundStart.RoundLength.TotalSeconds;

        public override bool RoundLock 
        {
            get
            {
                return RoundSummary.RoundLock;
            }
            set
            {
                RoundSummary.RoundLock = value;
            }
        }

        public override bool LobbyLock
        {
            get
            {
                return RoundStart.LobbyLock;
            }
            set
            {
                RoundStart.LobbyLock = value;
            }
        }

        public override void AddNTFUnit(string unit)
        {
            RespawnManager.Singleton.NamingManager.AllUnitNames.Add(new SyncUnit()
            {
                SpawnableTeam = (byte)SpawnableTeamType.None,
                UnitName = unit,
            });
        }

        public override void EndRound()
        {
            RoundSummary.singleton.ForceEnd();
        }

        public override void MTFRespawn(bool isCI)
        {
            if (isCI)
                RespawnManager.Singleton.ForceSpawnTeam(SpawnableTeamType.ChaosInsurgency);
            else
                RespawnManager.Singleton.ForceSpawnTeam(SpawnableTeamType.NineTailedFox);
        }

        public override void RestartRound()
        {
            RoundRestart.InitiateRoundRestart();
        }
    }
}
