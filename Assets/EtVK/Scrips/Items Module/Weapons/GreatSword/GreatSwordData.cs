using EtVK.Utyles;
using UnityEngine;

namespace EtVK.Items_Module.Weapons.GreatSword
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Items/Weapons/NewGreatSwordData")]
    public class GreatSwordData : WeaponData
    {
        private void Awake()
        {
            weaponType = WeaponType.GreatSword;
        }
    }
}