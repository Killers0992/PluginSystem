using System.Collections.Generic;

namespace PluginSystem.API.Roles
{
    public class RoleNone : IRole
    {
        public int RoleId => -1;
        public string Name { get; set; } = "None";
        public TeamType Team { get; set; } = TeamType.None;
        public bool IsScp => false;
        public bool IsHuman => false;
        public bool IsNtf => false;
        public bool IsChaos => false;
        public float MaxHealth { get; set; } = 0;
        public Dictionary<ItemType, ushort> StartAmmo { get; set; } = new Dictionary<ItemType, ushort>();
        public List<ItemType> StartInventory { get; set; } = new List<ItemType>();
    }
}
