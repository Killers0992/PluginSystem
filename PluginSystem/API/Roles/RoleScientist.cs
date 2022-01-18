using System.Collections.Generic;

namespace PluginSystem.API.Roles
{
    public class RoleScientist : IRole
    {
        public int RoleId => 6;
        public virtual string Name { get; set; } = "Scientist";
        public virtual TeamType Team { get; set; } = TeamType.Scientist;
        public virtual bool IsScp => false;
        public virtual bool IsHuman => true;
        public virtual bool IsNtf => false;
        public virtual bool IsChaos => false;
        public virtual float MaxHealth { get; set; } = 100;
        public virtual Dictionary<ItemType, ushort> StartAmmo { get; set; } = new Dictionary<ItemType, ushort>();
        public virtual List<ItemType> StartInventory { get; set; } = new List<ItemType>()
        {
            ItemType.KeycardScientist,
            ItemType.Medkit
        };

        public virtual void OnPlayerStartUsingRole(Player player)
        {
        }

        public virtual void OnPlayerStopUsingRole(Player player)
        {
        }
    }
}
