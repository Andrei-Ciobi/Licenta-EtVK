using EtVK.Items_Module.Weapons;
using EtVK.Utyles;
using UnityEngine;

namespace EtVK.Inventory_Module
{
    public class WeaponHolderSlot : HolderSlot
    {
        [SerializeField] private WeaponType weaponType;
        [SerializeField] private Transform drawParentOverride;
        public WeaponType WeaponType => weaponType;
        public Transform DrawParentOverride => drawParentOverride;

    }
}