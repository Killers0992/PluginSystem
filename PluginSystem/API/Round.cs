namespace PluginSystem.API
{
    public abstract class Round
    {
        public abstract void EndRound();
        public abstract int Duration { get; }
        public abstract void AddNTFUnit(string unit);
        public abstract void MTFRespawn(bool isCI);
        public abstract void RestartRound();
        public abstract bool RoundLock { get; set; }
        public abstract bool LobbyLock { get; set; }
    }
}
