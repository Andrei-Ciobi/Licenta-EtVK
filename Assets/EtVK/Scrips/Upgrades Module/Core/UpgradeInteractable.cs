using System.Collections.Generic;
using EtVK.Core.Utyles;
using EtVK.Event_Module.Event_Types;
using EtVK.Event_Module.Events;
using EtVK.Player_Module.Interactable;
using UnityEngine;

namespace EtVK.Upgrades_Module.Core
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class UpgradeInteractable : Interactable
    {
        [SerializeField] private UpgradesUiEvent displayUpgrades;

        protected abstract List<BaseUpgradeData> GetUpgrades(int amount);
        public override void Action(InteractableManager interactableManager)
        {
            if(UpgradesManager.Instance == null)
                return;

            var upgrades = GetUpgrades(3);
            displayUpgrades.Invoke(new UpgradesUiData(upgrades));
            
            interactableManager.ResponseSuccess(this);
            if(destroyAfterInteract)
                Destroy(gameObject);
        }

        public override void Response(StatusResponse statusResponse, string message = "")
        {
            throw new System.NotImplementedException();
        }
    }
}