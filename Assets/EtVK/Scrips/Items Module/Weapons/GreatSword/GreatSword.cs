using UnityEngine;

namespace EtVK.Items_Module.Weapons.GreatSword
{
    public class GreatSword : Weapon
    {
        [SerializeField] private GreatSwordData greatSwordData;
        private void Awake()
        {
            weaponData = greatSwordData;
        }

        public override void DrawWeapon()
        {
            if (greatSwordData.AnimatorOverride == null)
            {
                greatSwordData.Initialize();
            }
            transform.parent = currentWeaponSlot.DrawParentOverride;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;

            isArmed = true;
        }

        public override void WithdrawWeapon()
        {
            transform.parent = currentWeaponSlot.ParentOverride;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;

            isArmed = false;
        }

        public override void SwitchWeapon(Weapon currentWeapon)
        {
            if (currentWeapon == null)
            {
                DrawWeapon();
            }
            else
            {
                if (currentWeapon == this)
                {
                    WithdrawWeapon();
                }
                else
                {
                    currentWeapon.WithdrawWeapon();
                    DrawWeapon();
                }
            }
        }
    }
}