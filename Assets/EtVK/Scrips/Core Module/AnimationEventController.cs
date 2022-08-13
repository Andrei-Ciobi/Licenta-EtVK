using EtVK.Ability_Module;
using EtVK.Utyles;
using UnityEngine;

namespace EtVK.Core_Module
{
    public abstract class AnimationEventController : MonoBehaviour 
    {
        public bool CanCombo => canCombo;

        private bool canCombo;
        
        
        public abstract void ActivateWeaponCollider();
        public abstract void DeactivateWeaponCollider();
        public abstract void DrawWeapon(WeaponType weaponType);
        public abstract void WithdrawWeapon(WeaponType weaponType);
        public abstract void PerformAbility(BaseAbilityData abilityData);
        
        public void SetCanCombo(int value)
        {
            var boolValue = value != 0;
            canCombo = boolValue;
        }

    }
}