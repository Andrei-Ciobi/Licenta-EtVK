using UnityEngine;

namespace EtVK.Items_Module.Armors.Chestplate
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