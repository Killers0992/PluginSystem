using System.Collections.Generic;

namespace PluginSystem.API.Roles
{
    public class RoleNtfPrivate : IRole
    {
        public int RoleId => 13;
        public string Name { get; set; } = "Ntf Private";
        public TeamType Team { get; set; } = TeamType.NineTailFox;
        public bool IsScp => false;
        public bool IsHuman => true;
        public bool IsNtf => true;
        public bool IsChaos => false;
        public float MaxHealth { get; set; } = 100;

        public Dictionary<ItemType, ushort> StartAmmo { get; set; } = new Dictionary<ItemType, ushort>()
        {
            { ItemType.Ammo9x19, 160 },
            { ItemType.Ammo556x45, 40 }
        };

        public List<ItemType> StartInventory { get; set; } = new List<ItemType>()
        {
            ItemType.KeycardNTFOfficer,
            ItemType.GunCrossvec,
            ItemType.Medkit,
            ItemType.Radio,
            ItemType.ArmorCombat
        };
    }
}
