using EtVK.Player_Module.Interactable;
using UnityEngine;

namespace EtVK.Inventory_Module
{
    public abstract class Item : MonoBehaviour
    {
        public abstract void LoadItemFromInventory(BaseInventoryManager inventory);
        public abstract void AddItemToInventory(BaseInventoryManager inventory, Interactable interactable);
    }
}