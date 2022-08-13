﻿using System.Linq;
using EtVK.Inventory_Module;
using EtVK.Player_Module.Interactable;
using EtVK.Utyles;
using UnityEngine;

namespace EtVK.Items_Module.Weapons
{
    public abstract class Weapon : Item, IWeaponDamageable
    {
        public bool IsArmed => isArmed;
        public WeaponData WeaponData => weaponData;
        
        protected WeaponData weaponData;
        protected WeaponHolderSlot currentWeaponSlot;
        protected bool isArmed;

        public abstract void DrawWeapon();
        public abstract void WithdrawWeapon();
        public abstract void SwitchWeapon(Weapon currentWeapon);
        public override void LoadItemFromInventory(BaseInventoryManager inventory)
        {
            var weaponSlotList = inventory.GetAllHolderSlots().FindAll((slot) => slot.HolderSlotType == weaponData.ItemType).Cast<WeaponHolderSlot>().ToList();

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
            currentWeaponSlot = weaponSlot;
            var playerInventory = (PlayerInventoryManager) inventory;
            playerInventory.AddWeaponReference(this);
        }

        public override void AddItemToInventory(BaseInventoryManager inventory, Interactable interactable)
        {
            var playerInventory = (PlayerInventoryManager) inventory;
            if (!playerInventory.SpaceAvailable(weaponData.ItemType))
            {
                interactable.Response(StatusResponse.Fail, "No space available");
                return;
            }
            
            playerInventory.GetInventoryData().AddItem(weaponData);
            interactable.Response(StatusResponse.Success);
            

            // var weaponSlotList = inventoryManager.GetAllHolderSlots().FindAll((slot) => slot.HolderSlotType == weaponData.ItemType).Cast<WeaponHolderSlot>().ToList();
            //
            // if (weaponSlotList.Count == 0)
            // {
            //     Debug.LogError("No reference to an holder slot");
            //     return;
            // }
            //
            // var weaponSlot = weaponSlotList.Find((slot) => slot.WeaponType == weaponData.WeaponType);
            //
            // if (weaponSlot == null)
            // {
            //     Debug.LogError("No reference to an weapon holder slot");
            //     return;
            // }
            //
            // //We position it to the inventory slot
            // transform.parent = weaponSlot.ParentOverride;
            // transform.localPosition = Vector3.zero;
            // transform.localRotation = Quaternion.identity;
            //
            // //add a reference of the weapon slot to the weapon
            // curentWeaponSlot = weaponSlot;
            // weaponSlot.DestroyAndSetCurentWeapon(this);
            // inventoryManager.AddWeaponReference(this);
            // inventoryManager.GetInventoryData().AddItem(weaponData);

        }

        public float DealDamage()
        {
            return weaponData.BaseWeaponDamage;
        }
    }
}