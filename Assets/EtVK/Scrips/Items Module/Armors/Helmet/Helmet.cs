using System;
using System.Linq;
using EtVK.Inventory_Module;
using UnityEngine;

namespace EtVK.Items_Module.Armors.Helmet
{
    public class Helmet : Armor
    {
        [SerializeField] private HelmetData helmetData;

        private void Awake()
        {
            armorData = helmetData;
            InitializeReferences();
        }

        public override void LoadItemFromInventory(InventoryManager inventoryManager)
        {
            // inventoryManager.GetAllHolderSlots().FindAll(slot => slot.HolderSlotType == armorData.ItemType).ForEach(Debug.Log);
            var armorSlotList = inventoryManager.GetAllHolderSlots().FindAll(slot => slot.HolderSlotType == armorData.ItemType).Cast<ArmorHolderSlot>().ToList();

            if (armorSlotList.Count == 0)
            {
                Debug.LogError("No reference to an holder slot");
                return;
            }

            var armorSLot = (HelmetHolderSlot) armorSlotList.Find(slot => slot.ArmorType == armorData.ArmorType);
            
            if (armorSLot == null)
            {
                Debug.LogError("No reference to an armor holder slot");
                return;
            }
            
            transform.parent = armorSLot.ParentOverride;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            SetSkinBones(armorSLot);
            armorSLot.DefaultMeshRenderer.gameObject.SetActive(false);
            DeactivateVisual();
            armorSLot.DependencyList.ForEach(x => x.SetActive(false));
        }
    }
}