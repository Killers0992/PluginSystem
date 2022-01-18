using System.Collections.Generic;

namespace PluginSystem.API.Roles
{
    public class RoleNtfPrivate : IRole
    {
        public int RoleId => 13;
        public virtual string Name => "Ntf Private";
        public virtual TeamType Team => TeamType.NineTailFox;
        public virtual bool IsScp => false;
        public virtual bool IsHuman => true;
        public virtual bool IsNtf => true;
        public virtual bool IsChaos => false;
        public virtual float MaxHealth { get; set; } = 100;

        public virtual Dictionary<ItemType, ushort> StartAmmo { get; set; } = new Dictionary<ItemType, ushort>()
        {
            { ItemType.Ammo9x19, 160 },
            { ItemType.Ammo556x45, 40 }
        };

        public virtual List<ItemType> StartInventory { get; set; } = new List<ItemType>()
        {
            ItemType.KeycardNTFOfficer,
            ItemType.GunCrossvec,
            ItemType.Medkit,
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
