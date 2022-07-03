using System;
using System.Collections.Generic;
using System.Linq;
using EtVK.Scrips.Weapons_Module;
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

        private void LoadInventory()
        {
            if (inventoryData.InventorySize() > 0)
            {
                foreach (var item in inventoryData.AllItems())
                {
                    var prefab = Instantiate(item.Prefab);
                    var newItem = prefab.GetComponent<Item>();
                    newItem.LoadItem(this);
                }
            }
        }
    }
}