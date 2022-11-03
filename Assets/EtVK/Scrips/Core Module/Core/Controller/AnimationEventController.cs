﻿using EtVK.Ability_Module.Core;
using EtVK.Core.Utyles;
using UnityEngine;

namespace EtVK.Core.Controller
{
    public abstract class AnimationEventController : MonoBehaviour 
    {
        public bool CanCombo => canCombo;

        private BaseAttackController attackController;
        
        private bool canCombo;

        public abstract void ActivateWeaponCollider();
        public abstract void DeactivateWeaponCollider();
        public abstract void DrawWeapon(WeaponType weaponType);
        public abstract void WithdrawWeapon(WeaponType weaponType);
        public abstract void DrawWeaponOffHand(WeaponType weaponType);
        public abstract void WithdrawWeaponOffHand(WeaponType weaponType);
        public abstract void DrawOffHand(OffHandType offHandType);
        public abstract void WithdrawOffHand(OffHandType offHandType);
        public abstract void PerformAbility(BaseAbilityData abilityData);
        
        public void PerformAttack()
        {
            attackController.onAttack?.Invoke(transform.root);
        }
        
        public virtual void SetCanCombo(int value)
        {
            var boolValue = value != 0;
            canCombo = boolValue;
        }

        protected void BaseInitialize()
        {
            attackController = transform.root.GetComponent<BaseAttackController>();
        }

    }
}