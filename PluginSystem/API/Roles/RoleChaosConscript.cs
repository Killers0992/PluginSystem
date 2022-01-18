using System.Collections.Generic;

namespace PluginSystem.API.Roles
{
    public class RoleChaosConscript : IRole
    {
        public int RoleId => 8;
        public virtual string Name => "Chaos Conscript";
        public virtual TeamType Team => TeamType.ChaosInsurgency;

        public virtual bool IsScp => false;
        public virtual bool IsHuman => true;
        public virtual bool IsNtf => false;
        public virtual bool IsChaos => true;

        public virtual float MaxHealth { get; set; } = 100;

        public virtual Dictionary<ItemType, ushort> StartAmmo { get; set; } = new Dictionary<ItemType, ushort>()
        {
            { ItemType.Ammo762x39, 120 }
        };

        public virtual List<ItemType> StartInventory { get; set; } = new List<ItemType>()
        {
            ItemType.KeycardChaosInsurgency,
            ItemType.GunAK,
            ItemType.Medkit,
            ItemType.Painkillers,
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
