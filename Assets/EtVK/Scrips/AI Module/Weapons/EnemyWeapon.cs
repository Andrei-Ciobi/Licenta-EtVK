using EtVK.AI_Module.Actions;
using EtVK.AI_Module.Inventory;
using EtVK.Core_Module;
using EtVK.Inventory_Module;
using UnityEngine;

namespace EtVK.AI_Module.Weapons
{
    public class EnemyWeapon : BaseEnemyWeapon<EnemyWeaponData, EnemyAttackAction>, IEnemyWeapon
    {
        public void DrawWeapon()
        {
            transform.parent = currentWeaponSlot.DrawParentOverride;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;

            isArmed = true;
        }

        public void WithdrawWeapon()
        {
            transform.parent = currentWeaponSlot.ParentOverride;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;

            isArmed = false;
        }

        public void SetAnimationOverride()
        {
            if (weaponData.AnimatorOverride == null)
            {
                weaponData.Initialize();
            }
        }

        protected override void SetWeaponReference(BaseInventoryManager inventory)
        {
            var enemyInventory = (EnemyInventoryManager) inventory;
            enemyInventory.AddWeaponReference(this);
        }
    }
}