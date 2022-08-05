using EtVK.Ability_Module;
using EtVK.AI_Module.Inventory;
using EtVK.Core_Module;
using EtVK.Items_Module.Weapons;
using EtVK.Utyles;
using UnityEngine;

namespace EtVK.AI_Module.Controllers
{
    public class BaseEnemyAnimationEventController<TEnemyInventory> : AnimationEventController
        where TEnemyInventory : IEnemyInventory

    {
        private TEnemyInventory inventory;

        private void Awake()
        {
            inventory = transform.root.GetComponentInChildren<TEnemyInventory>();
        }

        public override void ActivateWeaponCollider()
        {
            var weaponColliderController = inventory.GetCurrentWeaponObj()
                .GetComponent<WeaponColliderController>();
            
            weaponColliderController.ActivateColliders();
        }

        public override void DeactivateWeaponCollider()
        {
            var weaponColliderController = inventory.GetCurrentWeaponObj()
                .GetComponent<WeaponColliderController>();
            
            weaponColliderController.DeactivateColliders();
        }

        public override void DrawWeapon(WeaponType weaponType)
        {
            var inventoryWeapon = inventory.GetWeaponObjByType(weaponType);
            
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
            var inventoryWeapon = inventory.GetWeaponObjByType(weaponType);
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