using System.Collections.Generic;
using EtVK.Scrips.Invenotry_Module;
using UnityEngine;

namespace EtVK.Scrips.Weapons_Module
{
    public class Weapon : Item
    {
        public WeaponHolderSlot CurentWeaponSlot
        {
            get => curentWeaponSlot;
            set => curentWeaponSlot = value;
        }

        public bool IsArmed => isArmed;
        
        protected WeaponHolderSlot curentWeaponSlot;
        private bool isArmed;

        public override void LoadItem(List<HolderSlot> holderSlots)
        {
            throw new System.NotImplementedException();
        }
    }
}