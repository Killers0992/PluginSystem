using System.Collections.Generic;

namespace PluginSystem.API.Roles
{
    public class RoleChaosRepressor : IRole
    {
        public int RoleId => 19;
        public string Name { get; set; } = "Chaos Repressor";
        public TeamType Team { get; set; } = TeamType.ChaosInsurgency;
        public bool IsScp => false;
        public bool IsHuman => true;
        public bool IsNtf => false;
        public bool IsChaos => true;

        public float MaxHealth { get; set; } = 100;

        public Dictionary<ItemType, ushort> StartAmmo { get; set; } = new Dictionary<ItemType, ushort>()
        {
            { ItemType.Ammo44cal, 24 },
            { ItemType.Ammo12gauge, 42 }
        };

        public List<ItemType> StartInventory { get; set; } = new List<ItemType>()
        {
            ItemType.KeycardChaosInsurgency,
            ItemType.GunShotgun,
            ItemType.GunRevolver,
            ItemType.Medkit,
            ItemType.Painkillers,
            ItemType.ArmorCombat
        };
    }
}
