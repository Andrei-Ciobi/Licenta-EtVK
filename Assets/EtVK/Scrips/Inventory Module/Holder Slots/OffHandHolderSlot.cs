using EtVK.Utyles;
using UnityEngine;

namespace EtVK.Inventory_Module.Holder_Slots
{
    public class OffHandHolderSlot : HolderSlot
    {
        [SerializeField] private OffHandType offHandType;
        [SerializeField] private Transform drawParentOverride;
        
        public OffHandType OffHandType => offHandType;
        public Transform DrawParentOverride => drawParentOverride;

    }
}