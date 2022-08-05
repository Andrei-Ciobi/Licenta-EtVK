using System.Collections.Generic;
using EtVK.Items_Module.Armors;
using EtVK.Items_Module.Weapons;
using EtVK.Utyles;
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

        public bool SpaceAvailable(ItemType itemType)
        {
            var inventorySpace = inventoryData.AllItemsByType(itemType);
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

        public Weapon GetWeaponByType(WeaponType type)
        {
            var weapon = weaponReferences.Find(x => x.WeaponData.WeaponType.Equals(type));
            
            if (weapon != null) 
                return weapon;
            
            Debug.Log($"No weapon of type: {type}");
            return null;
        }

        public InventoryData GetInventoryData()
        {
            return inventoryData;
        }
        

        protected override void LoadInventory()
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