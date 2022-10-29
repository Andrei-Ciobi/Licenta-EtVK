using EtVK.Utyles;
using UnityEngine;

namespace EtVK.Inventory_Module.Holder_Slots
{
    public class ArmorHolderSlot : HolderSlot
    {
        [SerializeField] private ArmorType armorType;
        [SerializeField] private SkinnedMeshRenderer defaultMeshRenderer;

        public ArmorType ArmorType => armorType;
        public SkinnedMeshRenderer DefaultMeshRenderer => defaultMeshRenderer;
        
    }
}