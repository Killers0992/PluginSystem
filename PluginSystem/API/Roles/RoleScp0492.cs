using System.Collections.Generic;

namespace PluginSystem.API.Roles
{
    public class RoleScp0492 : IRole
    {
        public int RoleId => 10;
        public virtual string Name => "Scp 049-2";
        public virtual TeamType Team => TeamType.Scp;
        public virtual bool IsScp => true;
        public virtual bool IsHuman => false;
        public virtual bool IsNtf => false;
        public virtual bool IsChaos => false;
        public virtual float MaxHealth { get; set; } = 500;
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
