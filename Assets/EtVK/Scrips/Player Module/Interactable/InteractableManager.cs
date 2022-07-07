using System.Collections.Generic;
using EtVK.Scrips.Input_Module;
using UnityEngine;

namespace EtVK.Scrips.Player_Module.Interactable
{
    public class InteractableManager : MonoBehaviour
    {
        [SerializeField] private List<Interactable> curentInteractableObjects = new List<Interactable>();

        private void Update()
        {
            if (InputManager.Instance.TapInteractInput && curentInteractableObjects.Count > 0)
            {
                OnInteractButton();
            }
        }

        private void OnInteractButton()
        {
            var shortestDistance = float.PositiveInfinity;

            Interactable closestInteractableObject = null;

            // Finds the closest interactable obj
            foreach (var obj in curentInteractableObjects)
            {
                var distance = Vector3.Distance(transform.root.position, obj.transform.position);

                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    closestInteractableObject = obj;
                }
            }

            if (closestInteractableObject == null)
                return;
            
            closestInteractableObject.Action();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == this.gameObject)
                return;

            if (other.TryGetComponent(out Interactable interactable))
            {
                curentInteractableObjects.Add(interactable);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if(other.gameObject == this.gameObject)
                return;
            
            if (other.TryGetComponent(out Interactable interactable))
            {
                if (curentInteractableObjects.Contains(interactable))
                {
                    curentInteractableObjects.Remove(interactable);
                }
            }
        }
    }
}