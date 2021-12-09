using System;
using System.Collections.Generic;

namespace PluginSystem.API
{
    public abstract class Player : IEquatable<Player>
	{
		public abstract string Name { get; }
		public abstract string DisplayNickname { get; set; }

		public abstract string IpAddress { get; }

		public abstract int PlayerId { get; }
		public abstract uint NetworkId { get; }

		public abstract string UserId { get; }
		public abstract UserIdType UserIdType { get; }
		
		public abstract IRole Role { get; set; }

		public abstract bool Overwatch { get; set; }
		public abstract bool DoNotTrack { get; }

		public abstract float Health { get; set; }
		public abstract float MaxHealth { get; set; }

		public abstract float ArtificialHealth { get; set; }
		public abstract float MaxArtificialHealth { get; set; }

		public abstract float Stamina { get; set; }

		public abstract bool IsMuted { get; set; }
		public abstract bool IsIntercomMuted { get; set; }
		public abstract bool BypassMode { get; set; }
		public abstract bool GodMode { get; set; }
		public abstract bool Noclip { get; set; }

		public abstract bool IsAlive { get; }
		public abstract bool IsDead { get; }

		public abstract bool IsCuffed { get; }
		public abstract Player Cuffer { get; set; }

		public abstract ushort GetAmmo(ItemType type);
		public abstract void SetAmmo(ItemType type, ushort amount);

		public abstract Vector GetRotation();
		public abstract Vector GetPosition();
		public abstract void Teleport(Vector pos, bool unstuck = false);

		public abstract void SetRank(string color = null, string text = null, string group = null);
		public abstract void ShowHint(string Text, float durationInSeconds = 1);

		public abstract string GetRankName();

		public abstract void Disconnect();
		public abstract void Disconnect(string message);

		public abstract void Ban(int duration);
		public abstract void Ban(int duration, string message);

		public abstract bool HasItem(ItemType type);

		public abstract void ClearInventory();
		
		public abstract object GetGameObject();

		public abstract void SendConsoleMessage(string message, string color = "green");

		public override bool Equals(object obj)
		{
			return Equals(obj as Player);
		}

		public bool Equals(Player other)
		{
			return other != null &&
				   PlayerId == other.PlayerId;
		}

		public override int GetHashCode()
		{
			return 956575109 + PlayerId.GetHashCode();
		}

		public static bool operator ==(Player left, Player right)
		{
			return EqualityComparer<Player>.Default.Equals(left, right);
		}

		public static bool operator !=(Player left, Player right)
		{
			return !(left == right);
		}
	}
}
