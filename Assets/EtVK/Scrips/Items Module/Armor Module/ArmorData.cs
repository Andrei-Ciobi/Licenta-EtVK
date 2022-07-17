using EtVK.Scrips.Inventory_Module;
using EtVK.Scrips.Utyles;
using UnityEngine;

namespace EtVK.Scrips.Items_Module.Armor_Module
{
    public abstract class ArmorData : ItemData
    {
        [SerializeField] protected ArmorType armorType;

        public ArmorType ArmorType => armorType;
    }
}