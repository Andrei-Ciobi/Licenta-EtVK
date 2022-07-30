using EtVK.Utyles;
using UnityEngine;

namespace EtVK.Player_Module.Interactable
{
    public abstract class Interactable : MonoBehaviour
    {
        [Header("Remove after one action")] [SerializeField]
        protected bool destroyAfterInteract;

        public abstract void Action();
        public abstract void Response(StatusResponse statusResponse, string message = "");
    }
}