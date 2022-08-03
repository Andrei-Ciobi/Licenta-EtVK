using EtVK.Ability_Module;
using EtVK.Actions_Module;
using EtVK.AI_Module.Inventory;
using EtVK.AI_Module.Weapons;
using EtVK.Core_Module;
using EtVK.Items_Module.Weapons;
using EtVK.Utyles;
using UnityEngine;

namespace EtVK.AI_Module.Controllers
{
    public class BaseEnemyAnimationEventController<T, TWeapon, TWeaponData> : AnimationEventController
        where T : BaseEnemyInventoryManager<TWeapon, TWeaponData> 
        where TWeapon : BaseEnemyWeapon<TWeaponData> 
        where TWeaponData : BaseEnemyWeaponData
    {
        private T inventoryManager;

        private void Awake()
        {
            inventoryManager = transform.root.GetComponentInChildren<T>();
        }

        public override void ActivateWeaponCollider()
        {
            var weaponColliderController = inventoryManager.GetCurrentWeapon()
                .GetComponent<WeaponColliderController>();
            
            weaponColliderController.ActivateColliders();
        }

        public override void DeactivateWeaponCollider()
        {
            var weaponColliderController = inventoryManager.GetCurrentWeapon()
                .GetComponent<WeaponColliderController>();
            
            weaponColliderController.DeactivateColliders();
        }

        public override void DrawWeapon(WeaponType weaponType)
        {
            var inventoryWeapon = inventoryManager.GetWeaponByType(weaponType);
            
            Debug.Log(inventoryWeapon);
            var weapon = inventoryWeapon.GetComponent<IWeapon>();
            if (weapon == null)
            {
                Debug.LogError($"No IWeapon interface on {inventoryWeapon.gameObject.name}");
                return;
            }
            weapon.DrawWeapon();
        }

        public override void WithdrawWeapon(WeaponType weaponType)
        {
            var inventoryWeapon = inventoryManager.GetWeaponByType(weaponType);
            var weapon = inventoryWeapon.GetComponent<IWeapon>();
            if (weapon == null)
            {
                Debug.LogError($"No IWeapon interface on {inventoryWeapon.gameObject.name}");
                return;
            }
            weapon.WithdrawWeapon();
        }

        public override void PerformAbility(BaseAbilityData abilityData)
        {
            // if(abilityData == null)
            // {
            //     Debug.LogError("No ability data given in the aniamtion event");
            //     return;
            // }
            //
            // var ability = inventoryManager.GetAbility(abilityData.AbilityType);
            //
            // ability.PerformAbility(abilityData);
        }
    }
}