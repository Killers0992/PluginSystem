using System.Collections.Generic;

namespace PluginSystem.API.Roles
{
    public class RoleClassd : IRole
    {
        public int RoleId => 1;
        public virtual string Name => "ClassD";
        public virtual TeamType Team => TeamType.ClassD;

        public virtual bool IsScp => false;
        public virtual bool IsHuman => true;
        public virtual bool IsNtf => false;
        public virtual bool IsChaos => false;

        public virtual float MaxHealth { get; set; } = 100;
        public virtual Dictionary<ItemType, ushort> StartAmmo { get; set; } = new Dictionary<ItemType, ushort>();
        public virtual List<ItemType> StartInventory { get; set; } = new List<ItemType>();

        public virtual void OnPlayerStartUsingRole(Player player)
        {
        }

        public virtual void OnPlayerStopUsingRole(Player player)
        {
        }
    }
}
