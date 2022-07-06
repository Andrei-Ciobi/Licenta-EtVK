using System.Linq;
using EtVK.Scrips.Invenotry_Module;
using UnityEngine;

namespace EtVK.Scrips.Items_Module.Armor_Module
{
    public class Armor : Item
    {
        protected ArmorData armorData;
        public override void LoadItemFromInvetory(InventoryManager inventoryManager)
        {
            var armorSlotList = inventoryManager.GetAllHolderSlots().FindAll((slot) => slot.HolderSlotType == armorData.ItemType).Cast<ArmorHolderSlot>().ToList();

            if (armorSlotList.Count == 0)
            {
                Debug.LogError("No reference to an holder slot");
                return;
            }

            var armorSLot = armorSlotList.Find((slot) => slot.ArmorType == armorData.ArmorType);
            
            if (armorSLot == null)
            {
                Debug.LogError("No reference to an armor holder slot");
                return;
            }
            
            //We position it to the inventory slot
            transform.parent = armorSLot.ParentOverride;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }

        public override void AddItemToInvetory(InventoryManager inventoryManager)
        {
            throw new System.NotImplementedException();
        }
    }
}