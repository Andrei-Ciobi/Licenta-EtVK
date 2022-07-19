using EtVK.Utyles;
using UnityEngine;

namespace EtVK.Items_Module.Armors.Chestplate
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