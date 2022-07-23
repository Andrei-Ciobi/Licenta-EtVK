using EtVK.Utyles;
using UnityEngine;

namespace EtVK.Inventory_Module
{
    public class HolderSlot : MonoBehaviour
    { 
        [SerializeField] private Transform parentOverride;
        [SerializeField] private ItemType holderSlotType;

        public Transform ParentOverride => parentOverride;

        public ItemType HolderSlotType => holderSlotType;
    }
}