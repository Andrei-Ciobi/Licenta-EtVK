using EtVK.Core.Utyles;
using EtVK.Inventory_Module;
using UnityEngine;

namespace EtVK.Items_Module.Armors
{
    public abstract class ArmorData : ItemData
    {
        [SerializeField] protected ArmorType armorType;

        public ArmorType ArmorType => armorType;
    }
}