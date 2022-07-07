using EtVK.Scrips.Event_Module.Events;
using EtVK.Scrips.Items_Module.Weapons_Module;
using UnityEngine;

namespace EtVK.Scrips.Player_Module.Interactable
{
    [RequireComponent(typeof(BoxCollider))]
    [RequireComponent(typeof(Rigidbody))]
    public class WeaponInteractable : Interactable
    {
        public WeaponEvent equipWeaponEvent;

        public override void Action()
        {
            var weapon = GetComponentInChildren<Weapon>();

            if (weapon == null)
            {
                Debug.LogError($"No weapon set as child of {gameObject.name}");
                return;
            }
            
            equipWeaponEvent.Invoke(weapon);

            if (destroyAfterInteract)
            {
                Destroy(gameObject);
            }
        }
    }
}