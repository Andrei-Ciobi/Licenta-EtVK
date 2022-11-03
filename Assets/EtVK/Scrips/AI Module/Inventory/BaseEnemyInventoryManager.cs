using System;
using System.Collections.Generic;
using EtVK.AI_Module.Weapons;
using EtVK.Core.Utyles;
using EtVK.Inventory_Module;
using UnityEngine;

namespace EtVK.AI_Module.Inventory
{
    public abstract class BaseEnemyInventoryManager<TWeapon, TWeaponData, TAction> : BaseInventoryManager,
        IEnemyInventory
        where TWeapon : BaseEnemyWeapon<TWeaponData, TAction>
        where TWeaponData : BaseEnemyWeaponData<TAction>
    {
        [SerializeField] private List<TWeaponData> weaponsList = new();

        private List<TWeapon> weaponReferences = new();

        public void AddWeaponReference(TWeapon weapon)
        {
            weaponReferences.Add(weapon);
        }

        public TWeapon GetCurrentWeapon()
        {
            var weapon = weaponReferences.Find(x => x.IsArmed);

            return weapon != null ? weapon : null;
        }

        public TWeapon GetWeapon(Predicate<TWeapon> predicate)
        {
            var weapon = weaponReferences.Find(predicate);

            if (weapon != null)
                return weapon;

            Debug.Log($"No weapon with predicate : {predicate}");
            return null;
        }

        public GameObject GetCurrentWeaponObj()
        {
            return weaponReferences.Find(weapon => weapon.IsArmed).gameObject;
        }

        public GameObject GetWeaponObj(WeaponType weaponType)
        {
            return weaponReferences.Find(weapon => weapon.WeaponData.WeaponType.Equals(weaponType)).gameObject;
        }

        public GameObject GetWeaponCurrentOffHand(WeaponType weaponType)
        {
            var weapon = weaponReferences.Find(weapon => weapon.WeaponData.WeaponType.Equals(weaponType));

            return weapon?.CurrentOffHand?.gameObject;
        }

        public GameObject GetWeaponObj(Predicate<TWeapon> predicate)
        {
            return weaponReferences.Find(predicate).gameObject;
        }

        protected override void LoadInventory()
        {
            if (weaponsList.Count <= 0)
                return;

            foreach (var item in weaponsList)
            {
                if (!item.IsEquipped)
                    continue;

                var prefab = Instantiate(item.Prefab);
                var newItem = prefab.GetComponent<Item>();

                if (newItem == null)
                {
                    Debug.LogError($"No item component on prefab {prefab.name}");
                    continue;
                }

                newItem.LoadItemFromInventory(this);
            }
        }
    }
}