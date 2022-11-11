using EtVK.Core.Utyles;
using UnityEngine;

namespace EtVK.Items_Module.Armors.Pants
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Items/Armor/NewPants")]
    public class PantsData : ArmorData
    {
        private void Awake()
        {
            armorType = ArmorType.Pants;
        }
    }
}