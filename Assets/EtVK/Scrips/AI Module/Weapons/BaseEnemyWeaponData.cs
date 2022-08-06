﻿using System;
using System.Collections.Generic;
using EtVK.Actions_Module;
using EtVK.Inventory_Module;
using EtVK.Utyles;
using UnityEngine;

namespace EtVK.AI_Module.Weapons
{
    public abstract class BaseEnemyWeaponData<TAction> : ItemData
    {
        [SerializeField] protected WeaponType weaponType;
        [SerializeField] private AnimatorOverrideController animatorOverride;

        [Header("Weapon stats")] 
        [Range(0f, 35f)] [SerializeField] private float meleeRange;
        [Range(0f, 35f)] [SerializeField] private float attackRange;
        [Range(0f, 10f)] [SerializeField] private float weaponAttackCd;
        
        [Header("List of base actions")] 
        [SerializeField] private List<BaseAction> baseActionList;
        [Header("List of attack actions")] 
        [SerializeField] private List<TAction> actionList;

        //Public props
        public WeaponType WeaponType => weaponType;
        public float AttackRange => attackRange;
        public float MeleeRange => meleeRange;
        public float WeaponAttackCd => weaponAttackCd;
        public AnimatorOverrideController AnimatorOverride => virtualOverride;
        
        //Private
        private AnimatorOverrideController virtualOverride;

        public abstract void Initialize();

        public List<TAction> GetActions()
        {
            return actionList;
        }
        public TAction GetAction(Predicate<TAction> predicate)
        {
            return actionList.Find(predicate);
        }

        public T GetBaseActionAs<T>(Predicate<T> predicate) where T : BaseAction
        {
            bool Filter(BaseAction x) => predicate(x as T);

            return baseActionList.Find(Filter) as T;
        }
        protected void InitializeVirtualAnimator()
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