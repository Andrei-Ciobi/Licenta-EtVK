using System;
using System.Collections.Generic;
using EtVK.Actions_Module;
using EtVK.Core.Utyles;
using EtVK.Inventory_Module;
using EtVK.Items_Module.Off_Hand;
using UnityEngine;

namespace EtVK.Items_Module.Weapons
{
    public abstract class WeaponData : ItemData
    {
        [SerializeField] protected WeaponType weaponType;
        [SerializeField] private float baseWeaponDamage;
        [SerializeField] private AnimatorOverrideController animatorOverride;
        [SerializeField] private bool hasOffHand;
        [SerializeField] private OffHandData offHandData;
        [Header("List of base actions")] 
        [SerializeField] private List<BaseAction> baseActionList;
        [Header("Attack actions map")]
        [SerializeField] private SerializableHashMap<AttackType, List<AttackAction>> attacks;

        public WeaponType WeaponType => weaponType;
        public float BaseWeaponDamage => baseWeaponDamage;
        public bool HasOffHand => hasOffHand;
        public OffHandData OffHandData => offHandData;

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
        
        public T GetBaseActionAs<T>(Predicate<T> predicate) where T : BaseAction
        {
            bool Filter(BaseAction x) => predicate(x as T);

            return baseActionList.Find(Filter) as T;
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