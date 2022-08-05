using EtVK.AI_Module.Actions;
using EtVK.Core_Module;
using UnityEngine;

namespace EtVK.AI_Module.Weapons
{
    public class EnemyWeapon : BaseEnemyWeapon<EnemyWeaponData, EnemyAttackAction>, IWeapon
    {
        public void DrawWeapon()
        {
            if (weaponData.AnimatorOverride == null)
            {
                weaponData.Initialize();
            }
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
    }
}