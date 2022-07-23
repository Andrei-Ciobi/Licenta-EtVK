using System.Collections.Generic;
using EtVK.Attacks_Module;
using EtVK.Inventory_Module;
using EtVK.Utyles;
using UnityEngine;

namespace EtVK.Items_Module.Weapons
{
    public abstract class WeaponData : ItemData
    {
        [SerializeField] protected WeaponType weaponType;
        [SerializeField] private float baseWeaponDamage;
        [SerializeField] private AnimatorOverrideController animatorOverride;
        [SerializeField] private SerializableHashMap<AttackType, List<AttackAction>> attacks;

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
            
            SetAttackAnimations();
        }
        
        public int GetMaxComboForAttackType(AttackType attackType)
        {
            if (attacks[attackType] == null)
            {
                Debug.LogError($"No attacks for {attackType} attack type");
                return -1;
            }

            return attacks[attackType].Count;
        }

        public AttackAction GetAttackAction(AttackType attackType, int index)
        {
            if (attacks[attackType] == null)
            {
                Debug.LogError($"No attacks for {attackType} attack type");
                return null;
            }

            if (attacks[attackType][index] == null)
            {
                Debug.LogError($"No attack action for {index} index");
                return null;
            }

            return attacks[attackType][index];
        }

        private void SetAttackAnimations()
        {
            foreach (var varKey in attacks.GetKeys())
            {
                foreach (var varAction in attacks[varKey])
                {
                    virtualOverride[varAction.ClipName] = varAction.AnimationClip;
                }
            }
        }
    }
}