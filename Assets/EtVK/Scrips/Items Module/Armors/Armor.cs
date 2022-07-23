using System;
using System.Linq;
using EtVK.Inventory_Module;
using UnityEngine;

namespace EtVK.Items_Module.Armors
{
    public class Armor : Item
    {
        private SkinnedMeshRenderer meshRenderer;
        private MeshFilter meshFilter;
        protected ArmorData armorData;
        

        public override void LoadItemFromInventory(InventoryManager inventoryManager)
        {
            var armorSlotList = inventoryManager.GetAllHolderSlots().FindAll(slot => slot.HolderSlotType == armorData.ItemType).Cast<ArmorHolderSlot>().ToList();

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
            
            transform.parent = armorSLot.ParentOverride;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            SetSkinBones(armorSLot);
            armorSLot.DefaultMeshRenderer.gameObject.SetActive(false);
            DeactivateVisual();
        }

        public override void AddItemToInventory(InventoryManager inventoryManager)
        {
            throw new System.NotImplementedException();
        }

        protected void InitializeReferences()
        {
            meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>(true);
            meshFilter = GetComponentInChildren<MeshFilter>(true);
        }

        protected void DeactivateVisual()
        {
            meshRenderer.gameObject.SetActive(true);
            meshFilter.gameObject.SetActive(false);
        }
        
        protected void ActivateVisual()
        {
            meshFilter.gameObject.SetActive(true);
            meshRenderer.gameObject.SetActive(false);
        }

        private void SetSkinBones(ArmorHolderSlot armorHolderSlot)
        {
            Debug.Log(meshRenderer);
            meshRenderer.bones = armorHolderSlot.DefaultMeshRenderer.bones;
            meshRenderer.rootBone = armorHolderSlot.DefaultMeshRenderer.rootBone;
        }
    }
}