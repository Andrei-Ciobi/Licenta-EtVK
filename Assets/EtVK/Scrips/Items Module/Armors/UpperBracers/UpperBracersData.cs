using EtVK.Utyles;
using UnityEngine;

namespace EtVK.Items_Module.Armors.UpperBracers
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Items/Armor/UpperBracers")]
    public class UpperBracersData : ArmorData
    {
        private void Awake()
        {
            armorType = ArmorType.UpperBracers;
        }
    }
}