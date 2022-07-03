using System.Collections.Generic;
using System.Linq;
using EtVK.Scrips.Invenotry_Module;
using UnityEngine;

namespace EtVK.Scrips.Weapons_Module.Sword
{
    public class Sword : Weapon
    {
        [SerializeField] private SwordData swordData;
        public override void LoadItem(List<HolderSlot> holderSlots)
        {
            var weaponSlotList = holderSlots.FindAll((slot) => slot.HolderSlotType == swordData.ItemType).Cast<WeaponHolderSlot>().ToList();

            if (weaponSlotList.Count == 0)
            {
                Debug.LogError("No reference to an holder slot");
                return;
            }

            var weaponSlot = weaponSlotList.Find((slot) => slot.WeaponType == swordData.WeaponType);
            
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

        }
    }
}