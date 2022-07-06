using System.Collections.Generic;
using UnityEngine;

namespace EtVK.Scrips.Invenotry_Module
{
    public abstract class Item : MonoBehaviour
    {
        public abstract void LoadItemFromInvetory(InventoryManager inventoryManager);
        public abstract void AddItemToInvetory(InventoryManager inventoryManager);
    }
}