using EtVK.Core.Utyles;
using EtVK.Event_Module.Event_Types;
using EtVK.Event_Module.Events;
using EtVK.Upgrades_Module.Core;
using UnityEngine;

namespace EtVK.Player_Module.Interactable
{
    [RequireComponent(typeof(Rigidbody))]
    public class CommonUpgradeInteractable : Interactable
    {
        [SerializeField] private UpgradesUiEvent displayUpgrades;
        public override void Action(InteractableManager interactableManager)
        {
            if(UpgradesManager.Instance == null)
                return;

            var upgrades = UpgradesManager.Instance.GetCommonUpgrade(3);
            
            displayUpgrades.Invoke(new UpgradesUiData(upgrades));
            
            if(destroyAfterInteract)
                Destroy(gameObject);
        }

        public override void Response(StatusResponse statusResponse, string message = "")
        {
            throw new System.NotImplementedException();
        }
    }
}