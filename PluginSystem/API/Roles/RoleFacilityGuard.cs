using System.Collections.Generic;

namespace PluginSystem.API.Roles
{
    public class RoleFacilityGuard : IRole
    {
        public int RoleId => 15;
        public string Name { get; set; } = "Facility Guard";
        public TeamType Team { get; set; } = TeamType.NineTailFox;

        public bool IsScp => false;
        public bool IsHuman => true;
        public bool IsNtf => false;
        public bool IsChaos => false;

        public float MaxHealth { get; set; } = 100;

        public Dictionary<ItemType, ushort> StartAmmo { get; set; } = new Dictionary<ItemType, ushort>()
        {
            { ItemType.Ammo9x19, 60 },
        };

        public List<ItemType> StartInventory { get; set; } = new List<ItemType>()
        {
            ItemType.KeycardGuard,
            ItemType.GunFSP9,
            ItemType.Medkit,
            ItemType.GrenadeFlash,
            ItemType.Radio,
            ItemType.ArmorLight
        };
    }
}
