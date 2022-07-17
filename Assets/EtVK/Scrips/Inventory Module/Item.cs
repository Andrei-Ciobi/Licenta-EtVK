using UnityEngine;

namespace EtVK.Scrips.Inventory_Module
{
    public abstract class Item : MonoBehaviour
    {
        public abstract void LoadItemFromInventory(InventoryManager inventoryManager);
        public abstract void AddItemToInventory(InventoryManager inventoryManager);
    }
}