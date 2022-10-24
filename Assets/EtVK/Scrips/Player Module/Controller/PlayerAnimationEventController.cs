using EtVK.Ability_Module.Core;
using EtVK.AI_Module.Weapons;
using EtVK.Core;
using EtVK.Items_Module.Weapons;
using EtVK.Utyles;
using UnityEngine;

namespace EtVK.Player_Module.Controller
{
    public class PlayerAnimationEventController : AnimationEventController
    {
        private PlayerManager manager;

        private void Awake()
        {
            manager = transform.root.GetComponent<PlayerManager>();
            BaseInitialize();
        }

        public override void SetCanCombo(int value)
        {
            if(manager.IsDodging)
                return;
            
            base.SetCanCombo(value);
        }

        public override void ActivateWeaponCollider()
        {
            if(manager.IsDodging)
                return;
            
            var weaponColliderController = manager.GetInventoryManager()?.GetCurrentWeapon()?
                .GetComponent<WeaponColliderController>();
            
            if(weaponColliderController == null)
                return;
            
            weaponColliderController.ActivateColliders();

        }
        
        public override void DeactivateWeaponCollider()
        {
            if(manager.IsDodging)
                return;
            
            var weaponColliderController = manager.GetInventoryManager()?.GetCurrentWeapon()?
                .GetComponent<WeaponColliderController>();
            
            if(weaponColliderController == null)
                return;
            
            weaponColliderController.DeactivateColliders();

        }

        public override void DrawWeapon(WeaponType weaponType)
        {         
            return;
            
            var weapon = manager.GetInventoryManager().GetWeaponByType(weaponType);
            if (weapon == null)
            {
                Debug.LogError($"No Weapon of type = {weaponType}");
                return;
            }
            weapon.DrawWeapon();
        }

        public override void WithdrawWeapon(WeaponType weaponType)
        {
            return;
            
            var weapon = manager.GetInventoryManager().GetWeaponByType(weaponType);
            if (weapon == null)
            {
                Debug.LogError($"No Weapon of type = {weaponType}");
                return;
            }
            weapon.WithdrawWeapon();
        }


        public override void PerformAbility(BaseAbilityData abilityData)
        {
            if(abilityData == null)
            {
                Debug.LogError("No ability data given in the animation event");
                return;
            }
            
            manager.GetAbilityManager().PerformAbility(abilityData, manager.GetAnimator());
        }
    }
}