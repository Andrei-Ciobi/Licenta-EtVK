using EtVK.Scrips.Core_Module;
using EtVK.Scrips.Utyles;
using UnityEngine;

namespace EtVK.Scrips.Invenotry_Module
{
    public class HolderSlot : MonoBehaviour
    { 
        [SerializeField] private Transform parentOverride;
        [SerializeField] private ItemType holderSlotType;

        public Transform ParentOverride => parentOverride;

        public ItemType HolderSlotType => holderSlotType;
    }
}