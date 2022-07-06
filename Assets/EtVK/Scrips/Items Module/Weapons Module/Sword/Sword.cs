using UnityEngine;

namespace EtVK.Scrips.Items_Module.Weapons_Module.Sword
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