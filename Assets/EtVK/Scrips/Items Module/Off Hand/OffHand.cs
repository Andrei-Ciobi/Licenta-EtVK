using System.Linq;
using EtVK.Inventory_Module;
using EtVK.Inventory_Module.Holder_Slots;
using EtVK.Player_Module.Interactable;
using UnityEngine;

namespace EtVK.Items_Module.Off_Hand
{
    public abstract class OffHand : Item
    {
        public bool IsArmed => isArmed;
        public OffHandData OffHandData => offHandData;

        protected OffHandData offHandData;
        protected OffHandHolderSlot currentHolderSlot;
        protected bool isArmed;

        public override void LoadItemFromInventory(BaseInventoryManager inventory)
        {
            var offHandSlotList = inventory.GetAllHolderSlots()
                .FindAll(slot => slot.HolderSlotType == OffHandData.ItemType).Cast<OffHandHolderSlot>().ToList();

            if (offHandSlotList.Count == 0)
            {
                Debug.LogError("No reference to an holder slot");
                return;
            }

            var offHandSLot = offHandSlotList.Find(slot => slot.OffHandType == offHandData.OffHandType);

            if (offHandSLot == null)
            {
                Debug.LogError("No reference to an offHand holder slot");
                return;
            }

            //We position it to the inventory slot
            transform.parent = offHandSLot.ParentOverride;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;

            currentHolderSlot = offHandSLot;
        }
        public override void AddItemToInventory(BaseInventoryManager inventory, Interactable interactable) { }

        public abstract void DrawOffHand();
        public abstract void WithdrawOffHand();
    }
}