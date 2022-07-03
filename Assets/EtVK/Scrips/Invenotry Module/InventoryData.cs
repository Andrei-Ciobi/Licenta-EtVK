using System.Collections.Generic;
using EtVK.Scrips.Core_Module;
using UnityEngine;

namespace EtVK.Scrips.Invenotry_Module
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Inventory/NewInventory")]
    public class InventoryData : ScriptableObject
    {
        [SerializeField]
        private List<SerializableSet<ItemType, List<ItemData>>> listOfItems =
            new List<SerializableSet<ItemType, List<ItemData>>>();

        public List<ItemData> AllItemsByType(ItemType type)
        {
            return listOfItems.Find((items) => items.GetKey().Equals(type)).GetValue();
        }

        public void AddItem(ItemData item)
        {
            var set = listOfItems.Find((items) => items.GetKey().Equals(item.ItemType));

            if (set != null) 
            {
                set.GetValue().Add(item);
            }
            else
            {
                var newItemSet = new SerializableSet<ItemType, List<ItemData>>();
                newItemSet.AddKeyValue(item.ItemType, new List<ItemData>(){item});
                listOfItems.Add(newItemSet);
            }
        }

        public List<ItemData> AllItems()
        {
            var itemList = new List<ItemData>();

            foreach (var itemSet in listOfItems)
            {
                itemList.AddRange(itemSet.GetValue());
            }

            return itemList;
        }

        public int InventorySize()
        {
            return listOfItems.Count;
        }
    }
}