﻿using EtVK.Utyles;
using UnityEngine;

namespace EtVK.Items_Module.Weapons.Sword
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