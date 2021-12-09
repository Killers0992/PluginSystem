using System.Collections.Generic;

namespace PluginSystem.API
{
	public enum Scp914KnobSetting : byte
	{
		Rough,
		Coarse,
		OneToOne,
		Fine,
		VeryFine
	}

	public abstract class Map
	{
		public abstract Vector GetRandomSpawnPoint(IRole role);
		public abstract List<Vector> GetBlastDoorPoints();
		public abstract Dictionary<Vector, Vector> GetElevatorTeleportPoints();
		public abstract void DetonateWarhead();
		public abstract void StartWarhead();
		public abstract void StopWarhead();
		public abstract void Shake();
		public abstract bool WarheadDetonated { get; }
		public abstract bool LCZDecontaminated { get; }
		public abstract void SpawnItem(ItemType type, Vector position, Vector rotation);
		public abstract void FemurBreaker(bool enable);
		public abstract void AnnounceNtfEntrance(int scpsLeft, int mtfNumber, char mtfLetter);
		public abstract void AnnounceScpKill(string scpNumber, Player killer = null);
		public abstract void AnnounceCustomMessage(string words);
		public abstract void AnnounceCustomMessage(string words, bool makeHold, bool makeNoise);
		public abstract void SetIntercomSpeaker(Player player);
		public abstract Player GetIntercomSpeaker();
		public abstract void Broadcast(uint duration, string message, bool isMonoSpaced);
		public abstract void ClearBroadcasts();
		public abstract bool WarheadLeverEnabled { get; set; }
		public abstract bool WarheadKeycardEntered { get; set; }
		public abstract void OverchargeLights(float forceDuration, bool onlyHeavy);
	}
}
