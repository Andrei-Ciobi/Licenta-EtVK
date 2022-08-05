using System.Collections.Generic;
using EtVK.AI_Module.Weapons;
using EtVK.Inventory_Module;
using EtVK.Utyles;
using UnityEngine;

namespace EtVK.AI_Module.Inventory
{
    public abstract class BaseEnemyInventoryManager<TWeapon, TWeaponData, TAction> : BaseInventoryManager, IEnemyInventory
        where  TWeapon : BaseEnemyWeapon<TWeaponData, TAction> 
        where TWeaponData : BaseEnemyWeaponData<TAction>
    {
        [SerializeField] private List<TWeaponData> weaponsList = new();
        
        private List<TWeapon> weaponReferences = new();

        public void AddWeaponReference(TWeapon weapon)
        {
            weaponReferences.Add(weapon);
        }

        public TWeapon GetCurrentWeapon()
        {
            var weapon = weaponReferences.Find(x => x.IsArmed);
            
            return weapon != null ? weapon : null;
        }

        public TWeapon GetWeaponByType(WeaponType type)
        {
            var weapon = weaponReferences.Find(x => x.WeaponData.WeaponType.Equals(type));
            
            if (weapon != null) 
                return weapon;
            
            Debug.Log($"No weapon of type: {type}");
            return null;
        }

        public GameObject GetCurrentWeaponObj()
        {
            return weaponReferences.Find(weapon => weapon.IsArmed).gameObject;
        }

        public GameObject GetWeaponObjByType(WeaponType weaponType)
        {
            return weaponReferences.Find(weapon => weapon.WeaponData.WeaponType == weaponType).gameObject;
        }

        protected override void LoadInventory()
        {
            if (weaponsList.Count <= 0) 
                return;
            
            foreach (var item in weaponsList)
            {
                if(!item.IsEquipped)
                    continue;
                    
                var prefab = Instantiate(item.Prefab);
                var newItem = prefab.GetComponent<Item>();

                if (newItem == null)
                {
                    Debug.LogError($"No item component on prefab {prefab.name}");
                    continue;
                }
                newItem.LoadItemFromInventory(this);
            }
        }
        
    }
}