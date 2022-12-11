using System;
using System.Collections.Generic;
using EtVK.Core.Utyles;
using EtVK.Items_Module.Armors;
using EtVK.Items_Module.Weapons;
using UnityEngine;

namespace EtVK.Inventory_Module
{
    public class PlayerInventoryManager : BaseInventoryManager
    {
        [SerializeField] private InventoryData inventoryData;
        
        private List<Weapon> weaponReferences = new();
        private List<Armor> armorReferences = new();

        private void Awake()
        {
            Initialize();
        }

        private void Start()
        {
            LoadInventory();
        }

        public void AddWeaponReference(Weapon weapon)
        {
            weaponReferences.Add(weapon);
        }

        public void AddArmorReference(Armor armor)
        {
            armorReferences.Add(armor);
        }

        public void AddItemToInventory(Event_Module.Event_Types.Item item)
        {
            
            item.InteractItem.AddItemToInventory(this, item.Interactable);
        }

        public bool SpaceAvailable<T>(ItemType itemType) where T : ItemData
        {
            var inventorySpace = inventoryData.GetItemsOfType<T>();
            if (inventorySpace == null)
                return true;

            var maxInventorySpace = inventoryData.GetInventorySpace(itemType);
            
            return inventorySpace.Count < maxInventorySpace;
        }

        public Weapon GetCurrentWeapon()
        {
            var weapon = weaponReferences.Find(x => x.IsArmed);
            
            return weapon != null ? weapon : null;
        }

        public Weapon GetWeapon(Predicate<Weapon> predicate)
        {
            var weapon = weaponReferences.Find(predicate);
            
            if (weapon != null) 
                return weapon;
            
            Debug.Log($"No weapon found with predicate : {predicate}");
            return null;
        }

        public InventoryData GetInventoryData()
        {
            return inventoryData;
        }
        
        protected override void LoadInventory()
        {

            foreach (var item in inventoryData.GetAllItems())
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