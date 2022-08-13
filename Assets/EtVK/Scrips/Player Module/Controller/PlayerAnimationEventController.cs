using EtVK.Ability_Module;
using EtVK.AI_Module.Weapons;
using EtVK.Core_Module;
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
        }

        public override void ActivateWeaponCollider()
        {
            var playerManager = transform.root.GetComponent<PlayerManager>();

            var weaponColliderController = playerManager.GetInventoryManager().GetCurrentWeapon()
                .GetComponent<WeaponColliderController>();
            
            weaponColliderController.ActivateColliders();

        }
        
        public override void DeactivateWeaponCollider()
        {

            var weaponColliderController = manager.GetInventoryManager().GetCurrentWeapon()
                .GetComponent<WeaponColliderController>();
            
            weaponColliderController.DeactivateColliders();

        }

        public override void DrawWeapon(WeaponType weaponType)
        {           
            var inventoryWeapon = manager.GetInventoryManager().GetWeaponByType(weaponType);
            var weapon = inventoryWeapon.GetComponent<IEnemyWeapon>();
            if (weapon == null)
            {
                Debug.LogError($"No IWeapon interface on {inventoryWeapon.gameObject.name}");
                return;
            }
            weapon.DrawWeapon();
        }

        public override void WithdrawWeapon(WeaponType weaponType)
        {
            var inventoryWeapon = manager.GetInventoryManager().GetWeaponByType(weaponType);
            var weapon = inventoryWeapon.GetComponent<IEnemyWeapon>();
            if (weapon == null)
            {
                Debug.LogError($"No IWeapon interface on {inventoryWeapon.gameObject.name}");
                return;
            }
            weapon.WithdrawWeapon();
        }


        public override void PerformAbility(BaseAbilityData abilityData)
        {
            if(abilityData == null)
            {
                Debug.LogError("No ability data given in the aniamtion event");
                return;
            }
            
            var ability = manager.GetAbility(abilityData.AbilityType);
            
            ability.PerformAbility(abilityData);
        }
    }
}