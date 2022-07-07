using EtVK.Scrips.Core_Module;
using EtVK.Scrips.Items_Module.Weapons_Module;
using EtVK.Scrips.Utyles;
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

        public Weapon CurentWeapon => curentWeapon;

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