using UnityEngine;

namespace EtVK.Scrips.Player_Module.Interactable
{
    public abstract class Interactable : MonoBehaviour
    {
        [Header("Remove after one action")] [SerializeField]
        protected bool destroyAfterInteract;

        public abstract void Action();
    }
}