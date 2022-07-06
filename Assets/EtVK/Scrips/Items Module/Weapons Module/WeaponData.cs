using EtVK.Scrips.Core_Module;
using EtVK.Scrips.Invenotry_Module;
using UnityEngine;

namespace EtVK.Scrips.Weapons_Module
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