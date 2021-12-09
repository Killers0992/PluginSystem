using InventorySystem;
using InventorySystem.Items;
using InventorySystem.Items.Armor;
using InventorySystem.Items.Pickups;
using Mirror;
using MonoMod;
using System.Collections.Generic;
using System.Linq;

namespace PluginSystem.Patcher.Patches
{
    [MonoModPatch("global::InventorySystem.InventoryItemProvider")]
    public class ItemProviderPatch
    {
		private static void RoleChanged(ReferenceHub ply, RoleType prevRole, RoleType newRole, bool lite, CharacterClassManager.SpawnReason spawnReason)
		{
            if (!NetworkServer.active)
                return;

            Inventory inventory = ply.inventory;
            if (spawnReason == CharacterClassManager.SpawnReason.Escaped && prevRole != newRole)
            {
                List<ItemPickupBase> list = new List<ItemPickupBase>();
                if (inventory.TryGetBodyArmor(out BodyArmor bodyArmor))
                {
                    bodyArmor.DontRemoveExcessOnDrop = true;
                }

                while (inventory.UserInventory.Items.Count > 0)
                {
                    list.Add(inventory.ServerDropItem(inventory.UserInventory.Items.ElementAt(0).Key));
                }

                InventoryItemProvider.PreviousInventoryPickups[ply] = list;
            }
            else
            {
                while (inventory.UserInventory.Items.Count > 0)
                {
                    inventory.ServerRemoveItem(inventory.UserInventory.Items.ElementAt(0).Key, null);
                }

                inventory.UserInventory.ReserveAmmo.Clear();
                inventory.SendAmmoNextFrame = true;
            }

            if (!RoleManager.Manager.RegisteredRoles.TryGetValue((int)newRole, out IRole role))
                return;

            foreach (var item in role.StartAmmo)
            {
                inventory.ServerAddAmmo((ItemType)item.Key, item.Value);
            }

            for (int i = 0; i < role.StartInventory.Count; i++)
            {
                ItemBase arg = inventory.ServerAddItem((ItemType)role.StartInventory[i], 0);
                InventoryItemProvider.OnItemProvided?.Invoke(ply, arg);
            }
        }
	}
}
