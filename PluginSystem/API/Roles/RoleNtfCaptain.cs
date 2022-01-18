using System.Collections.Generic;

namespace PluginSystem.API.Roles
{
    public class RoleNtfCaptain : IRole
    {
        public int RoleId => 12;
        public virtual string Name => "Ntf Captain";
        public virtual TeamType Team => TeamType.NineTailFox;
        public virtual bool IsScp => false;
        public virtual bool IsHuman => true;
        public virtual bool IsNtf => true;
        public virtual bool IsChaos => false;
        public virtual float MaxHealth { get; set; } = 100;

        public virtual Dictionary<ItemType, ushort> StartAmmo { get; set; } = new Dictionary<ItemType, ushort>()
        {
            { ItemType.Ammo9x19, 40 },
            { ItemType.Ammo556x45, 160 }
        };

        public virtual List<ItemType> StartInventory { get; set; } = new List<ItemType>()
        {
            ItemType.KeycardNTFCommander,
            ItemType.GunE11SR,
            ItemType.Adrenaline,
            ItemType.Medkit,
            ItemType.GrenadeHE,
            ItemType.Radio,
            ItemType.ArmorHeavy
        };

        public virtual void OnPlayerStartUsingRole(Player player)
        {
        }

        public virtual void OnPlayerStopUsingRole(Player player)
        {
        }
    }
}
