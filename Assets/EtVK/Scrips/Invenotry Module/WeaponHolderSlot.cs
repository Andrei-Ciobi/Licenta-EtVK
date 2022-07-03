using EtVK.Scrips.Core_Module;
using EtVK.Scrips.Weapons_Module;
using UnityEngine;

namespace EtVK.Scrips.Invenotry_Module
{
    public class WeaponHolderSlot : HolderSlot
    {
        [SerializeField] private WeaponType weaponType;
        [SerializeField] private Transform drawParentOverride;
        private Weapon curentWeapon;
        public WeaponType WeaponType => weaponType;
        public Transform DrawParentOverride => drawParentOverride;
        
        public void DestroyAndSetCurentWeapon(Weapon newWeapon)
        {
            if(curentWeapon != null)
            {
                Destroy(curentWeapon.gameObject);
            }
            curentWeapon = newWeapon;
        }
        
    }
}