using System;
using EtVK.Inventory_Module;
using EtVK.Utyles;
using UnityEngine;

namespace EtVK.AI_Module.Inventory
{
    public interface IEnemyInventory
    {
        public GameObject GetCurrentWeaponObj();
        public GameObject GetWeaponObj(WeaponType weaponType);
        public GameObject GetWeaponCurrentOffHand(WeaponType weaponType);
    }
}