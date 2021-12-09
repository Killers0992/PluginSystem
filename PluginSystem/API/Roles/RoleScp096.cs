using System.Collections.Generic;

namespace PluginSystem.API.Roles
{
    public class RoleScp096 : IRole
    {
        public int RoleId => 9;
        public string Name { get; set; } = "Scp 106";
        public TeamType Team { get; set; } = TeamType.Scp;
        public bool IsScp => true;
        public bool IsHuman => false;
        public bool IsNtf => false;
        public bool IsChaos => false;
        public float MaxHealth { get; set; } = 2000;
        public Dictionary<ItemType, ushort> StartAmmo { get; set; } = new Dictionary<ItemType, ushort>();
        public List<ItemType> StartInventory { get; set; } = new List<ItemType>();
    }
}
