using EtVK.Event_Module.Event_Types;
using EtVK.Event_Module.Events;
using EtVK.Items_Module.Weapons;
using EtVK.Utyles;
using UnityEngine;

namespace EtVK.Player_Module.Interactable
{
    [RequireComponent(typeof(BoxCollider))]
    [RequireComponent(typeof(Rigidbody))]
    public class WeaponInteractable : Interactable
    {
        public ItemEvent equipWeaponEvent;

        public override void Action()
        {
            var weapon = GetComponentInChildren<Weapon>();

            if (weapon == null)
            {
                Debug.LogError($"No weapon set as child of {gameObject.name}");
                return;
            }
            
            equipWeaponEvent.Invoke(new Item(weapon, this));
            
        }

        public override void Response(StatusResponse statusResponse, string message = "")
        {
            switch (statusResponse)
            {
                case StatusResponse.Success:
                    OnResponseSuccess();
                    break;
                case StatusResponse.Fail:
                    Debug.Log(message);
                    break;
            }
        }


        private void OnResponseSuccess()
        {
            if (destroyAfterInteract)
            {
                Destroy(gameObject);
            }
        }
    }
}