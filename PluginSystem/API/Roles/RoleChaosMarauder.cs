﻿using System.Collections.Generic;

namespace PluginSystem.API.Roles
{
    public class RoleChaosMarauder : IRole
    {
        public int RoleId => 20;
        public string Name { get; set; } = "Chaos Marauder";
        public TeamType Team { get; set; } = TeamType.ChaosInsurgency;
        public bool IsScp => false;
        public bool IsHuman => true;
        public bool IsNtf => false;
        public bool IsChaos => true;

        public float MaxHealth { get; set; } = 100;

        public Dictionary<ItemType, ushort> StartAmmo { get; set; } = new Dictionary<ItemType, ushort>()
        {
            { ItemType.Ammo762x39, 200 }
        };

        public List<ItemType> StartInventory { get; set; } = new List<ItemType>()
        {
            ItemType.KeycardChaosInsurgency,
            ItemType.GunLogicer,
            ItemType.Medkit,
            ItemType.Adrenaline,
            ItemType.ArmorHeavy
        };
    }
}
