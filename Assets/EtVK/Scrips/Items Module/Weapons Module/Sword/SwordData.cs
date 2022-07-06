using System;
using EtVK.Scrips.Core_Module;
using UnityEngine;

namespace EtVK.Scrips.Weapons_Module.Sword
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Weapons/NewSwordData")]
    public class SwordData : WeaponData
    {
        private void Awake()
        {
            weaponType = WeaponType.Sword;
        }
    }
}