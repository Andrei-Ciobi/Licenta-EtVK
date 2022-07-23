using EtVK.Inventory_Module;
using EtVK.Utyles;
using UnityEngine;

namespace EtVK.Items_Module.Armors
{
    public abstract class ArmorData : ItemData
    {
        [SerializeField] protected ArmorType armorType;

        public ArmorType ArmorType => armorType;
    }
}