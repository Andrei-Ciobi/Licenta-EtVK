using EtVK.Core.Utyles;
using UnityEngine;

namespace EtVK.Items_Module.Armors.Helmet
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Items/Armor/NewHelmet")]
    public class HelmetData : ArmorData
    {
        private void Awake()
        {
            itemType = ItemType.Armor;
            armorType = ArmorType.Helmet;
        }
    }
}