using System.Linq;
using EtVK.Inventory_Module;
using EtVK.Inventory_Module.Holder_Slots;
using EtVK.Player_Module.Interactable;
using UnityEngine;

namespace EtVK.Items_Module.Armors
{
    public class Armor : Item
    {
        private SkinnedMeshRenderer meshRenderer;
        private MeshFilter meshFilter;
        protected ArmorData armorData;
        

        public override void LoadItemFromInventory(BaseInventoryManager inventory)
        {
            var armorSlotList = inventory.GetAllHolderSlots().FindAll(slot => slot.HolderSlotType == armorData.ItemType).Cast<ArmorHolderSlot>().ToList();

            if (armorSlotList.Count == 0)
            {
                Debug.LogError("No reference to an holder slot");
                return;
            }

            var armorSLot = armorSlotList.Find(slot => slot.ArmorType == armorData.ArmorType);
            
            if (armorSLot == null)
            {
                Debug.LogError("No reference to an armor holder slot");
                return;
            }
            
            transform.parent = armorSLot.ParentOverride;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            SetSkinBones(armorSLot);
            DeactivateVisual();
            var playerInventory = (PlayerInventoryManager) inventory;
            // playerInventory.AddArmorReference(this);
        }

        public override void AddItemToInventory(BaseInventoryManager inventory, Interactable interactable)
        {
            // var playerInventory = (PlayerInventoryManager) inventory;
            // if (!playerInventory.SpaceAvailable(armorData.ItemType))
            // {
            //     interactable.Response(StatusResponse.Fail, "No space available");
            //     return;
            // }
            //
            // playerInventory.GetInventoryData().AddItem(armorData);
            // interactable.Response(StatusResponse.Success);
        }

        protected void InitializeReferences()
        {
            meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>(true);
            meshFilter = GetComponentInChildren<MeshFilter>(true);
        }

        protected virtual void DeactivateVisual()
        {
            meshRenderer.gameObject.SetActive(true);
            meshFilter.gameObject.SetActive(false);
        }
        
        protected void ActivateVisual()
        {
            meshFilter.gameObject.SetActive(true);
            meshRenderer.gameObject.SetActive(false);
        }

        protected virtual void SetSkinBones(ArmorHolderSlot armorHolderSlot)
        {
            meshRenderer.bones = armorHolderSlot.DefaultMeshRenderer.bones;
            meshRenderer.rootBone = armorHolderSlot.DefaultMeshRenderer.rootBone;
            armorHolderSlot.DefaultMeshRenderer.gameObject.SetActive(false);
        }
    }
}