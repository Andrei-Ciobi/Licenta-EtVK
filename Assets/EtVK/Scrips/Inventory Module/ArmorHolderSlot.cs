using EtVK.Scrips.Utyles;
using UnityEngine;

namespace EtVK.Scrips.Inventory_Module
{
    public class ArmorHolderSlot : HolderSlot
    {
        [SerializeField] private ArmorType armorType;

        public ArmorType ArmorType => armorType;
    }
}