using EtVK.Items_Module.Weapons;
using EtVK.Utyles;
using UnityEngine;

namespace EtVK.Inventory_Module
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