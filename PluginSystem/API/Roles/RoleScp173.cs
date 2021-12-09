using System.Collections.Generic;

namespace PluginSystem.API.Roles
{
    public class RoleScp173 : IRole
    {
        public int RoleId => 0;
        public string Name { get; set; } = "Scp 173";
        public TeamType Team { get; set; } = TeamType.Scp;
        public bool IsScp => true;
        public bool IsHuman => false;
        public bool IsNtf => false;
        public bool IsChaos => false;
        public float MaxHealth { get; set; } = 3000;
        public Dictionary<ItemType, ushort> StartAmmo { get; set; } = new Dictionary<ItemType, ushort>();
        public List<ItemType> StartInventory { get; set; } = new List<ItemType>();
    }
}
