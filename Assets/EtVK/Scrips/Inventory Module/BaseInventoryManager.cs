using System.Collections.Generic;
using System.Linq;
using EtVK.Inventory_Module.Holder_Slots;
using UnityEngine;

namespace EtVK.Inventory_Module
{
    public abstract class BaseInventoryManager : MonoBehaviour
    {
        protected List<HolderSlot> holderSlots = new();
        protected abstract void LoadInventory();
        public List<HolderSlot> GetAllHolderSlots()
        {
            return holderSlots;
        }

        protected void Initialize()
        {
            holderSlots = transform.root.GetComponentsInChildren<HolderSlot>().ToList();
        }
    }
}