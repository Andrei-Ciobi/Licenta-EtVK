using EtVK.Scrips.Core_Module;
using EtVK.Scrips.Invenotry_Module;
using UnityEngine;

namespace EtVK.Scrips.Items_Module.Armor_Module
{
    public abstract class ArmorData : ItemData
    {
        [SerializeField] protected ArmorType armorType;

        public ArmorType ArmorType => armorType;
    }
}