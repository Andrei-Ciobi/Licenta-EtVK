using System.Collections.Generic;
using EtVK.Utyles;
using UnityEngine;

namespace EtVK.Inventory_Module
{
    public class ArmorHolderSlot : HolderSlot
    {
        [SerializeField] private ArmorType armorType;
        [SerializeField] private SkinnedMeshRenderer defaultMeshRenderer;

        public ArmorType ArmorType => armorType;
        public SkinnedMeshRenderer DefaultMeshRenderer => defaultMeshRenderer;
        
    }
}