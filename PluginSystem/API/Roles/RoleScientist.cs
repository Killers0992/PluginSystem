using System.Collections.Generic;

namespace PluginSystem.API.Roles
{
    public class RoleScientist : IRole
    {
        public int RoleId => 6;
        public string Name { get; set; } = "Scientist";
        public TeamType Team { get; set; } = TeamType.Scientist;
        public bool IsScp => false;
        public bool IsHuman => true;
        public bool IsNtf => false;
        public bool IsChaos => false;
        public float MaxHealth { get; set; } = 100;
        public Dictionary<ItemType, ushort> StartAmmo { get; set; } = new Dictionary<ItemType, ushort>();
        public List<ItemType> StartInventory { get; set; } = new List<ItemType>()
        {
            ItemType.KeycardScientist,
            ItemType.Medkit
        };
    }
}
