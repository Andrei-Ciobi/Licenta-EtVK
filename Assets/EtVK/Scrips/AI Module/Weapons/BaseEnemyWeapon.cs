using System.Linq;
using EtVK.AI_Module.Inventory;
using EtVK.Inventory_Module;
using EtVK.Items_Module.Weapons;
using EtVK.Player_Module.Interactable;
using UnityEngine;

namespace EtVK.AI_Module.Weapons
{
    public abstract class BaseEnemyWeapon : Item, IWeaponDamageable
    {
        public bool IsArmed => isArmed;
        public BaseEnemyWeaponData WeaponData => weaponData;
        
        protected BaseEnemyWeaponData weaponData;
        protected WeaponHolderSlot currentWeaponSlot;
        protected bool isArmed;
        
        public abstract void DrawWeapon();
        public abstract void WithdrawWeapon();
        public abstract void SwitchWeapon(BaseEnemyWeapon currentWeapon);
        
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
            var playerInventory = (EnemyInventoryManager) inventory;
            playerInventory.AddWeaponReference(this);
        }
        public override void AddItemToInventory(BaseInventoryManager inventoryManager, Interactable interactable) { }
        public float DealDamage()
        {
            return weaponData.Damage;
        }
    }
}