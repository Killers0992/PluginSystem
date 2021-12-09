using System.Collections.Generic;

namespace PluginSystem.API.Roles
{
    public class RoleChaosConscript : IRole
    {
        public int RoleId => 8;
        public string Name { get; set; } = "Chaos Conscript";
        public TeamType Team { get; set; } = TeamType.ChaosInsurgency;

        public bool IsScp => false;
        public bool IsHuman => true;
        public bool IsNtf => false;
        public bool IsChaos => true;

        public float MaxHealth { get; set; } = 100;

        public Dictionary<ItemType, ushort> StartAmmo { get; set; } = new Dictionary<ItemType, ushort>()
        {
            { ItemType.Ammo762x39, 120 }
        };

        public List<ItemType> StartInventory { get; set; } = new List<ItemType>()
        {
            ItemType.KeycardChaosInsurgency,
            ItemType.GunAK,
            ItemType.Medkit,
            ItemType.Painkillers,
            ItemType.ArmorCombat
        };
    }
}
