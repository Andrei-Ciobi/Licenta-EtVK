using EtVK.AI_Module.Actions;
using EtVK.AI_Module.Inventory;
using EtVK.Inventory_Module;
using EtVK.Items_Module.Weapons;
using UnityEngine;

namespace EtVK.AI_Module.Weapons
{
    public class EnemyWeapon : BaseEnemyWeapon<EnemyWeaponData, EnemyAttackAction>, IEnemyWeapon, IWeaponDamageable
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
        
        public float DealDamage()
        {
            return CurrentAttackAction?.AttackDamage ?? weaponData.BaseDamage;
        }
        
        protected override void SetWeaponReference(BaseInventoryManager inventory)
        {
            var enemyInventory = (EnemyInventoryManager) inventory;
            enemyInventory.AddWeaponReference(this);
        }


    }
}