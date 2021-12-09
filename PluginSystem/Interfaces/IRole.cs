﻿using PluginSystem.API;
using System.Collections.Generic;

namespace PluginSystem
{
    public interface IRole
    {
        int RoleId { get; }

        string Name { get; set; }

        TeamType Team { get; set; }

        bool IsScp { get; }

        bool IsHuman { get; }

        bool IsNtf { get; }

        bool IsChaos { get; }

        float MaxHealth { get; set; }

        Dictionary<ItemType, ushort> StartAmmo { get; set; }

        List<ItemType> StartInventory { get; set; }
    }
}
