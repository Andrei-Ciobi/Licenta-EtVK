using System;
using UnityEngine;

namespace EtVK.Scrips.Items_Module.Armor_Module.Chestplate
{
    public class Chestplate : Armor
    {
        [SerializeField] private ChestplateData chestplateData;

        private void Awake()
        {
            armorData = chestplateData;
        }
    }
}