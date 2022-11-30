using EtVK.Core.Utyles;
using UnityEngine;

namespace EtVK.Player_Module.Interactable
{
    public abstract class Interactable : MonoBehaviour
    {
        [Header("Labels")] 
        [SerializeField] private string pressLabel = "press";
        [SerializeField] private string interactLabel = "to interact";
        [Header("Remove after one action")] 
        [SerializeField] protected bool destroyAfterInteract;

        public string PressLabel => pressLabel;
        public string InteractLabel => interactLabel;

        protected InteractableManager manager;

        public abstract void Action(InteractableManager interactableManager);
        public abstract void Response(StatusResponse statusResponse, string message = "");
    }
}