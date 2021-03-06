using System.Linq;
using EtVK.Scrips.Inventory_Module;
using UnityEngine;

namespace EtVK.Scrips.Items_Module.Weapons_Module
{
    public abstract class Weapon : Item, IWeaponDamageable
    {
        public bool IsArmed => isArmed;
        public WeaponData WeaponData => weaponData;
        
        protected WeaponData weaponData;
        protected WeaponHolderSlot curentWeaponSlot;
        protected bool isArmed;

        public abstract void DrawWeapon();
        public abstract void WithdrawWeapon();
        public abstract void SwitchWeapon(Weapon curentWeapon);
        public override void LoadItemFromInventory(InventoryManager inventoryManager)
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

        public override void AddItemToInventory(InventoryManager inventoryManager)
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

        public float DealDamage()
        {
            return weaponData.BaseWeaponDamage;
        }
    }
}