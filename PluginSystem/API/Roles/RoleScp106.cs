using System.Collections.Generic;

namespace PluginSystem.API.Roles
{
    public class RoleScp106 : IRole
    {
        public int RoleId => 3;
        public virtual string Name => "Scp 106";
        public virtual TeamType Team => TeamType.Scp;
        public virtual bool IsScp => true;
        public virtual bool IsHuman => false;
        public virtual bool IsNtf => false;
        public virtual bool IsChaos => false;
        public virtual float MaxHealth { get; set; } = 850;
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
