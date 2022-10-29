using EtVK.Utyles;
using UnityEngine;

namespace EtVK.Inventory_Module.Holder_Slots
{
    public class HolderSlot : MonoBehaviour
    { 
        [SerializeField] private Transform parentOverride;
        [SerializeField] private ItemType holderSlotType;

        public Transform ParentOverride => parentOverride;

        public ItemType HolderSlotType => holderSlotType;
    }
}