using System.Collections.Generic;

namespace PluginSystem.API.Roles
{
    public class RoleNtfSergeant : IRole
    {
        public int RoleId => 11;
        public string Name { get; set; } = "Ntf Sergeant";
        public TeamType Team { get; set; } = TeamType.NineTailFox;
        public bool IsScp => false;
        public bool IsHuman => true;
        public bool IsNtf => true;
        public bool IsChaos => false;
        public float MaxHealth { get; set; } = 100;

        public Dictionary<ItemType, ushort> StartAmmo { get; set; } = new Dictionary<ItemType, ushort>()
        {
            { ItemType.Ammo9x19, 40 },
            { ItemType.Ammo556x45, 120 }
        };

        public List<ItemType> StartInventory { get; set; } = new List<ItemType>()
        {
            ItemType.KeycardNTFLieutenant,
            ItemType.GunE11SR,
            ItemType.Medkit,
            ItemType.GrenadeHE,
            ItemType.Radio,
            ItemType.ArmorCombat
        };
    }
}
