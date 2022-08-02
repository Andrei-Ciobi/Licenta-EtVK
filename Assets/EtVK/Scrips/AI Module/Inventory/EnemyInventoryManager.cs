using System.Collections.Generic;
using EtVK.AI_Module.Weapons;
using EtVK.Inventory_Module;
using UnityEngine;

namespace EtVK.AI_Module.Inventory
{
    public class EnemyInventoryManager : BaseInventoryManager
    {
        [SerializeField] private List<ItemData> itemList = new();
        
        private List<BaseEnemyWeapon> weaponReferences = new();
        
        private void Awake()
        {
            Initialize();
        }
        
        public void AddWeaponReference(BaseEnemyWeapon weapon)
        {
            weaponReferences.Add(weapon);
        }

        public BaseEnemyWeapon GetCurrentWeapon()
        {
            return weaponReferences.Find(x => x.IsArmed);
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