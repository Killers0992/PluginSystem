using System.Collections.Generic;

namespace PluginSystem.API.Roles
{
    public class RoleFacilityGuard : IRole
    {
        public int RoleId => 15;
        public virtual string Name => "Facility Guard";
        public virtual TeamType Team => TeamType.NineTailFox;

        public virtual bool IsScp => false;
        public virtual bool IsHuman => true;
        public virtual bool IsNtf => false;
        public virtual bool IsChaos => false;

        public virtual float MaxHealth { get; set; } = 100;

        public virtual Dictionary<ItemType, ushort> StartAmmo { get; set; } = new Dictionary<ItemType, ushort>()
        {
            { ItemType.Ammo9x19, 60 },
        };

        public virtual List<ItemType> StartInventory { get; set; } = new List<ItemType>()
        {
            ItemType.KeycardGuard,
            ItemType.GunFSP9,
            ItemType.Medkit,
            ItemType.GrenadeFlash,
            ItemType.Radio,
            ItemType.ArmorLight
        };

        public virtual void OnPlayerStartUsingRole(Player player)
        {
        }

        public virtual void OnPlayerStopUsingRole(Player player)
        {
        }
    }
}
