using EtVK.Scrips.Utyles;
using UnityEngine;

namespace EtVK.Scrips.Inventory_Module
{
    public class HolderSlot : MonoBehaviour
    { 
        [SerializeField] private Transform parentOverride;
        [SerializeField] private ItemType holderSlotType;

        public Transform ParentOverride => parentOverride;

        public ItemType HolderSlotType => holderSlotType;
    }
}