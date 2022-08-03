using System.Collections.Generic;
using EtVK.AI_Module.Weapons;
using EtVK.Inventory_Module;
using EtVK.Utyles;
using UnityEngine;

namespace EtVK.AI_Module.Inventory
{
    public abstract class BaseEnemyInventoryManager<TWeapon, TWeaponData> : BaseInventoryManager 
        where  TWeapon : BaseEnemyWeapon<TWeaponData> where TWeaponData : BaseEnemyWeaponData
    {
        [SerializeField] private List<ItemData> itemList = new();
        
        private List<TWeapon> weaponReferences = new();

        public void AddWeaponReference(TWeapon weapon)
        {
            weaponReferences.Add(weapon);
        }

        public TWeapon GetCurrentWeapon()
        {
            var weapon = weaponReferences.Find(x => x.IsArmed);
            
            if (weapon != null) 
                return weapon;
            
            Debug.Log("No weapon armed");
            return null;

        }

        public TWeapon GetWeaponByType(WeaponType type)
        {
            var weapon = weaponReferences.Find(x => x.WeaponData.WeaponType.Equals(type));
            
            if (weapon != null) 
                return weapon;
            
            Debug.Log($"No weapon of type: {type}");
            return null;
        }
        protected override void LoadInventory()
        {
            if (itemList.Count <= 0) 
                return;
            
            foreach (var item in itemList)
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