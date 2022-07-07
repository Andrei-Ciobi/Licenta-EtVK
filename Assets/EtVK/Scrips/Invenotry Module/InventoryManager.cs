using System.Collections.Generic;
using System.Linq;
using EtVK.Scrips.Items_Module.Weapons_Module;
using EtVK.Scrips.Utyles;
using UnityEngine;

namespace EtVK.Scrips.Invenotry_Module
{
    public class InventoryManager : MonoBehaviour
    {
        [SerializeField] private InventoryData inventoryData;

        private List<HolderSlot> holderSlots = new List<HolderSlot>();
        private List<Weapon> weaponReferences = new List<Weapon>();

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

        public void AddItemToInventory(Item item)
        {
            
            item.AddItemToInvetory(this);
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
            if (inventoryData.InventorySize() > 0)
            {
                foreach (var item in inventoryData.AllItems())
                {
                    var prefab = Instantiate(item.Prefab);
                    var newItem = prefab.GetComponent<Item>();

                    if (newItem == null)
                    {
                        Debug.LogError($"No item component on prefab {prefab.name}");
                        return;
                    }
                    newItem.LoadItemFromInvetory(this);
                }
            }
        }
    }
}