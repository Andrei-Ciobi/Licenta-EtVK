using System;
using System.Collections.Generic;
using System.Linq;
using EtVK.Scrips.Invenotry_Module;
using UnityEngine;

namespace EtVK.Scrips.Weapons_Module.Sword
{
    public class Sword : Weapon
    {
        [SerializeField] private SwordData swordData;

        private void Awake()
        {
            weaponData = swordData;
        }

    }
}