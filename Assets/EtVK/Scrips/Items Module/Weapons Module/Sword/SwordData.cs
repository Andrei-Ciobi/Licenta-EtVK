using EtVK.Scrips.Utyles;
using UnityEngine;

namespace EtVK.Scrips.Items_Module.Weapons_Module.Sword
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