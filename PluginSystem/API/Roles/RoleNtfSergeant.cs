using System.Collections.Generic;

namespace PluginSystem.API.Roles
{
    public class RoleNtfSergeant : IRole
    {
        public int RoleId => 11;
        public virtual string Name => "Ntf Sergeant";
        public virtual TeamType Team => TeamType.NineTailFox;
        public virtual bool IsScp => false;
        public virtual bool IsHuman => true;
        public virtual bool IsNtf => true;
        public virtual bool IsChaos => false;
        public virtual float MaxHealth { get; set; } = 100;

        public virtual Dictionary<ItemType, ushort> StartAmmo { get; set; } = new Dictionary<ItemType, ushort>()
        {
            { ItemType.Ammo9x19, 40 },
            { ItemType.Ammo556x45, 120 }
        };

        public virtual List<ItemType> StartInventory { get; set; } = new List<ItemType>()
        {
            ItemType.KeycardNTFLieutenant,
            ItemType.GunE11SR,
            ItemType.Medkit,
            ItemType.GrenadeHE,
            ItemType.Radio,
            ItemType.ArmorCombat
        };

        public virtual void OnPlayerStartUsingRole(Player player)
        {
        }

        public virtual void OnPlayerStopUsingRole(Player player)
        {
        }
    }
}
