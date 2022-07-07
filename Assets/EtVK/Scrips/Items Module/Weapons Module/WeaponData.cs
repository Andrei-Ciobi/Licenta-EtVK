using EtVK.Scrips.Invenotry_Module;
using EtVK.Scrips.Utyles;
using UnityEngine;

namespace EtVK.Scrips.Items_Module.Weapons_Module
{
    public abstract class WeaponData : ItemData
    {
        [SerializeField] protected WeaponType weaponType;
        [SerializeField] private float baseWeaponDamage;
        [SerializeField] private AnimatorOverrideController animatorOverride;

        public WeaponType WeaponType => weaponType;

        public float BaseWeaponDamage => baseWeaponDamage;

        public AnimatorOverrideController AnimatorOverride => animatorOverride;
    }
}