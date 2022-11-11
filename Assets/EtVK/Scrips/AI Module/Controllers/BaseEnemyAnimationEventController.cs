using EtVK.Ability_Module.Core;
using EtVK.AI_Module.Inventory;
using EtVK.Core.Controller;
using EtVK.Core.Utyles;
using EtVK.Items_Module.Off_Hand;
using EtVK.Items_Module.Weapons;
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
            BaseInitialize();
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
            var inventoryWeapon = inventory.GetWeaponObj(weaponType);
            
            var weapon = inventoryWeapon.GetComponent<IWeaponDamageable>();
            if (weapon == null)
            {
                Debug.LogError($"No IWeaponDamageable interface on {inventoryWeapon.gameObject.name}");
                return;
            }
            weapon.DrawWeapon();
        }

        public override void WithdrawWeapon(WeaponType weaponType)
        {
            var inventoryWeapon = inventory.GetWeaponObj(weaponType);
            var weapon = inventoryWeapon.GetComponent<IWeaponDamageable>();
            if (weapon == null)
            {
                Debug.LogError($"No IWeaponDamageable interface on {inventoryWeapon.gameObject.name}");
                return;
            }
            weapon.WithdrawWeapon();
        }

        public override void DrawWeaponOffHand(WeaponType weaponType)
        {
            var inventoryOffHand = inventory.GetWeaponCurrentOffHand(weaponType);
            if(inventoryOffHand == null)
                return;

            var offHand = inventoryOffHand.GetComponent<OffHand>();
            if (offHand == null)
            {
                Debug.LogError($"No offHand on {inventoryOffHand.gameObject.name}");
                return;
            }
            
            offHand.DrawOffHand();
        }
        
        public override void WithdrawWeaponOffHand(WeaponType weaponType)
        {
            var inventoryOffHand = inventory.GetWeaponCurrentOffHand(weaponType);
            if(inventoryOffHand == null)
                return;

            var offHand = inventoryOffHand.GetComponent<OffHand>();
            if (offHand == null)
            {
                Debug.LogError($"No offHand on {inventoryOffHand.gameObject.name}");
                return;
            }
            
            offHand.WithdrawOffHand();
        }

        public override void DrawOffHand(OffHandType offHandType)
        {
        }

        public override void WithdrawOffHand(OffHandType offHandType)
        {
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