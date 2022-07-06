using System.Linq;
using EtVK.Scrips.Invenotry_Module;
using UnityEngine;

namespace EtVK.Scrips.Weapons_Module
{
    public class Weapon : Item
    {
        protected WeaponData weaponData;
        public WeaponHolderSlot CurentWeaponSlot
        {
            get => curentWeaponSlot;
            set => curentWeaponSlot = value;
        }

        public bool IsArmed => isArmed;
        
        protected WeaponHolderSlot curentWeaponSlot;
        private bool isArmed;

        public override void LoadItemFromInvetory(InventoryManager inventoryManager)
        {
            var weaponSlotList = inventoryManager.GetAllHolderSlots().FindAll((slot) => slot.HolderSlotType == weaponData.ItemType).Cast<WeaponHolderSlot>().ToList();

            if (weaponSlotList.Count == 0)
            {
                Debug.LogError("No reference to an holder slot");
                return;
            }

            var weaponSlot = weaponSlotList.Find((slot) => slot.WeaponType == weaponData.WeaponType);
            
            if (weaponSlot == null)
            {
                Debug.LogError("No reference to an weapon holder slot");
                return;
            }
            
            //We position it to the inventory slot
            transform.parent = weaponSlot.ParentOverride;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            
            //add a reference of the weapon slot to the weapon
            curentWeaponSlot = weaponSlot;
            weaponSlot.DestroyAndSetCurentWeapon(this);
            inventoryManager.AddWeaponReference(this);
        }

        public override void AddItemToInvetory(InventoryManager inventoryManager)
        {
            var weaponSlotList = inventoryManager.GetAllHolderSlots().FindAll((slot) => slot.HolderSlotType == weaponData.ItemType).Cast<WeaponHolderSlot>().ToList();

            if (weaponSlotList.Count == 0)
            {
                Debug.LogError("No reference to an holder slot");
                return;
            }

            var weaponSlot = weaponSlotList.Find((slot) => slot.WeaponType == weaponData.WeaponType);
            
            if (weaponSlot == null)
            {
                Debug.LogError("No reference to an weapon holder slot");
                return;
            }

            //We position it to the inventory slot
            transform.parent = weaponSlot.ParentOverride;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            
            //add a reference of the weapon slot to the weapon
            curentWeaponSlot = weaponSlot;
            weaponSlot.DestroyAndSetCurentWeapon(this);
            inventoryManager.AddWeaponReference(this);
            inventoryManager.GetInventoryData().AddItem(weaponData);
        }
    }
}