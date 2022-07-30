﻿using System.Collections.Generic;
using System.Linq;
using EtVK.Items_Module.Armors;
using EtVK.Items_Module.Weapons;
using EtVK.Utyles;
using UnityEngine;

namespace EtVK.Inventory_Module
{
    public class InventoryManager : MonoBehaviour
    {
        [SerializeField] private InventoryData inventoryData;

        private List<HolderSlot> holderSlots = new();
        private List<Weapon> weaponReferences = new();
        private List<Armor> armorReferences = new();

        private void Start()
        {
            holderSlots = transform.root.GetComponentsInChildren<HolderSlot>().ToList();
            LoadInventory();
        }

        public List<HolderSlot> GetAllHolderSlots()
        {
            return holderSlots;
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

        public bool SpaceAvailable(ItemType itemType)
        {
            var inventorySpace = inventoryData.AllItemsByType(itemType);
            if (inventorySpace == null)
                return true;

            var maxInventorySpace = inventoryData.GetInventorySpace(itemType);
            
            return inventorySpace.Count < maxInventorySpace;
        }

        public Weapon GetArmedWeapon()
        {
            return weaponReferences.Find((weapon) => weapon.IsArmed);
        }

        public Weapon GetWeapon(WeaponType weaponType)
        {
            return weaponReferences.Find((weapon) => weapon.WeaponData.WeaponType == weaponType);
        }

        public InventoryData GetInventoryData()
        {
            return inventoryData;
        }
        

        private void LoadInventory()
        {
            if (inventoryData.InventorySize() <= 0) 
                return;
            
            foreach (var item in inventoryData.AllItems())
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