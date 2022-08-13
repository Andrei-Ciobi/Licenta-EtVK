using EtVK.Utyles;
using UnityEngine;

namespace EtVK.AI_Module.Inventory
{
    public interface IEnemyInventory
    {
        public GameObject GetCurrentWeaponObj();
        public GameObject GetWeaponObjByType(WeaponType weaponType);
    }
}