using UnityEngine;

namespace EtVK.Items_Module.Weapons.Sword
{
    public class Sword : Weapon
    {
        [SerializeField] private SwordData swordData;

        private void Awake()
        {
            weaponData = swordData;
        }

        public override void DrawWeapon()
        {
            if (swordData.AnimatorOverride == null)
            {
                swordData.Initialize();
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