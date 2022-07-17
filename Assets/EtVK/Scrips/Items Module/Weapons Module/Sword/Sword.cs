using UnityEngine;

namespace EtVK.Scrips.Items_Module.Weapons_Module.Sword
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
            transform.parent = curentWeaponSlot.DrawParentOverride;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;

            isArmed = true;
        }

        public override void WithdrawWeapon()
        {
            transform.parent = curentWeaponSlot.ParentOverride;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;

            isArmed = false;
        }

        public override void SwitchWeapon(Weapon curentWeapon)
        {
            if (curentWeapon == null)
            {
                DrawWeapon();
            }
            else
            {
                if (curentWeapon == this)
                {
                    WithdrawWeapon();
                }
                else
                {
                    curentWeapon.WithdrawWeapon();
                    DrawWeapon();
                }
            }
        }
    }
}