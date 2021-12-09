using System.Collections.Generic;

namespace PluginSystem.API.Roles
{
    public class RoleNtfCaptain : IRole
    {
        public int RoleId => 12;
        public string Name { get; set; } = "Ntf Captain";
        public TeamType Team { get; set; } = TeamType.NineTailFox;
        public bool IsScp => false;
        public bool IsHuman => true;
        public bool IsNtf => true;
        public bool IsChaos => false;
        public float MaxHealth { get; set; } = 100;

        public Dictionary<ItemType, ushort> StartAmmo { get; set; } = new Dictionary<ItemType, ushort>()
        {
            { ItemType.Ammo9x19, 40 },
            { ItemType.Ammo556x45, 160 }
        };

        public List<ItemType> StartInventory { get; set; } = new List<ItemType>()
        {
            ItemType.KeycardNTFCommander,
            ItemType.GunE11SR,
            ItemType.Adrenaline,
            ItemType.Medkit,
            ItemType.GrenadeHE,
            ItemType.Radio,
            ItemType.ArmorHeavy
        };
    }
}
