using EtVK.Player_Module.Interactable;
using UnityEngine;

namespace EtVK.Inventory_Module
{
    public abstract class Item : MonoBehaviour
    {
        public abstract void LoadItemFromInventory(InventoryManager inventoryManager);
        public abstract void AddItemToInventory(InventoryManager inventoryManager, Interactable interactable);
    }
}