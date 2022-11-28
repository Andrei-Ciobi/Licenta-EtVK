using EtVK.Core.Utyles;
using EtVK.Event_Module.Event_Types;
using EtVK.Event_Module.Events;
using EtVK.Items_Module.Weapons;
using UnityEngine;

namespace EtVK.Player_Module.Interactable
{
    [RequireComponent(typeof(BoxCollider))]
    [RequireComponent(typeof(Rigidbody))]
    public class WeaponInteractable : Interactable
    {
        public ItemEvent equipWeaponEvent;

        public override void Action(InteractableManager interactableManager)
        {
            var weapon = GetComponentInChildren<Weapon>();

            if (weapon == null)
            {
                Debug.LogError($"No weapon set as child of {gameObject.name}");
                return;
            }

            manager = interactableManager;
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
                    manager.ResponseFail(message);
                    break;
            }
        }


        private void OnResponseSuccess()
        {
            if (destroyAfterInteract)
            {
                Destroy(gameObject);
            }
            
            manager.ResponseSuccess(this);
        }
    }
}