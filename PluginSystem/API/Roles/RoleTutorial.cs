using System.Collections.Generic;

namespace PluginSystem.API.Roles
{
    public class RoleTutorial : IRole
    {
        public int RoleId => 14;
        public string Name { get; set; } = "Tutorial";
        public TeamType Team { get; set; } = TeamType.Tutorial;
        public bool IsScp => false;
        public bool IsHuman => false;
        public bool IsNtf => false;
        public bool IsChaos => false;
        public float MaxHealth { get; set; } = 100;
        public Dictionary<ItemType, ushort> StartAmmo { get; set; } = new Dictionary<ItemType, ushort>();
        public List<ItemType> StartInventory { get; set; } = new List<ItemType>();
    }
}
