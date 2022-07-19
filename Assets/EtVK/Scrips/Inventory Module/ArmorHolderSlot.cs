using EtVK.Utyles;
using UnityEngine;

namespace EtVK.Inventory_Module
{
    public class ArmorHolderSlot : HolderSlot
    {
        [SerializeField] private ArmorType armorType;

        public ArmorType ArmorType => armorType;
    }
}