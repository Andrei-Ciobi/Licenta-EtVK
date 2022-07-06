using System;
using EtVK.Scrips.Core_Module;
using UnityEngine;

namespace EtVK.Scrips.Items_Module.Armor_Module.Chestplate
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Armor/NewChestplate")]
    public class ChestplateData : ArmorData
    {
        private void Awake()
        {
            armorType = ArmorType.Chestplate;
        }
    }
}