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

        public AnimatorOverrideController AnimatorOverride => virtualOverride;
        private AnimatorOverrideController virtualOverride;

        public void Initialize()
        {
            if(animatorOverride == null)
                return;

            virtualOverride = new AnimatorOverrideController(animatorOverride.runtimeAnimatorController);

            for (var i = 0; i < animatorOverride.animationClips.Length; ++i)
            {
                var clipName = animatorOverride.runtimeAnimatorController.animationClips[i].name;
                var clip = animatorOverride.animationClips[i];

                virtualOverride[clipName] = clip;
            }
        }
    }
}