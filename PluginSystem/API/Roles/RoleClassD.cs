using System.Collections.Generic;

namespace PluginSystem.API.Roles
{
    public class RoleClassd : IRole
    {
        public int RoleId => 1;
        public string Name { get; set; } = "ClassD";
        public TeamType Team { get; set; } = TeamType.ClassD;

        public bool IsScp => false;
        public bool IsHuman => true;
        public bool IsNtf => false;
        public bool IsChaos => false;

        public float MaxHealth { get; set; } = 100;
        public Dictionary<ItemType, ushort> StartAmmo { get; set; } = new Dictionary<ItemType, ushort>();
        public List<ItemType> StartInventory { get; set; } = new List<ItemType>();
    }
}
