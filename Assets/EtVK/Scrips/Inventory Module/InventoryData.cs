using System.Collections.Generic;
using EtVK.Core.Utyles;
using UnityEngine;

namespace EtVK.Inventory_Module
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Inventory/NewInventory")]
    public class InventoryData : ScriptableObject
    {
        [SerializeField] private ArmorSetData armorSet;
        [SerializeField] private List<SerializableSet<ItemType, int>> inventorySpace = new();
        [SerializeField] private List<ItemData> itemList = new();

        
        public List<T> GetItemsOfType<T>() where T : ItemData
        {
            var items = new List<T>();

            foreach (var itemData in itemList)
            {
                if (itemData is T data)
                {
                    items.Add(data);
                }
            }

            return items;
        }
        public int GetInventorySpace(ItemType type)
        {
            return inventorySpace.Find(x => x.GetKey().Equals(type)).GetValue();
        }

        public void AddItem(ItemData item)
        {
            itemList.Add(item);
        }

        public List<ItemData> GetAllItems()
        {
            var totalItems = new List<ItemData>();
            totalItems.AddRange(itemList);
            totalItems.AddRange(armorSet.GetArmorSet());

            return totalItems;
        }
    }
}