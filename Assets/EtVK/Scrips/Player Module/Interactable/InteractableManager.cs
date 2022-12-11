using System.Collections.Generic;
using EtVK.Event_Module.Event_Types;
using EtVK.Event_Module.Events;
using EtVK.Input_Module;
using UnityEngine;

namespace EtVK.Player_Module.Interactable
{
    public class InteractableManager : MonoBehaviour
    {
        [SerializeField] private InteractUiEvent interactUiEven;
        [SerializeField] private StringEvent errorEvent;
        private List<Interactable> currentInteractableObjects = new();
        private Interactable closestInteractable;

        private void Update()
        {
            if (InputManager.Instance.Player.TapInteractInput && currentInteractableObjects.Count > 0)
            {
                OnInteractButton();
            }

            if (currentInteractableObjects.Count > 0)
            {
                UpdateUi(true);
            }
        }

        public void ResponseSuccess(Interactable interactable = null)
        {
            currentInteractableObjects.RemoveAll(x => x == interactable);
            UpdateUi(); 
        }

        public void ResponseFail(string msg, Interactable interactable = null)
        {
            currentInteractableObjects.RemoveAll(x => x == interactable);
            UpdateUi();
            errorEvent.Invoke(msg);
        }

        private void OnInteractButton()
        {
            var closestInteractableObject = GetClosestInteractable();

            if (closestInteractableObject == null)
                return;
            
            closestInteractableObject.Action(this);
        }

        private Interactable GetClosestInteractable()
        {
            var shortestDistance = float.PositiveInfinity;

            Interactable closestInteractableObject = null;
            currentInteractableObjects.RemoveAll(x => x == null);
            
            // Finds the closest interactable obj
            foreach (var obj in currentInteractableObjects)
            {

                var distance = Vector3.Distance(transform.root.position, obj.transform.position);

                if (!(distance < shortestDistance)) 
                    continue;
                shortestDistance = distance;
                closestInteractableObject = obj;
            }

            return closestInteractableObject;
        }

        private void UpdateUi(bool shortUpdate = false)
        {
            var closestInter = GetClosestInteractable();
            
            if(closestInteractable == closestInter)
                return;

            if (closestInter == null)
            {
                closestInteractable = null;
                interactUiEven.Invoke(new InteractUiData(false));
                return;
            }

            
            if (!shortUpdate && closestInteractable == null)
            {
                interactUiEven.Invoke(new InteractUiData(closestInter.PressLabel,
                    closestInter.InteractLabel, true)); 
            }
            else
            {
                interactUiEven.Invoke(new InteractUiData(closestInter.PressLabel,
                    closestInter.InteractLabel)); 
            }
            closestInteractable = closestInter;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == this.gameObject)
                return;

            if (!other.TryGetComponent(out Interactable interactable)) 
                return;
            
            currentInteractableObjects.Add(interactable);
            UpdateUi();
        }

        private void OnTriggerExit(Collider other)
        {
            if(other.gameObject == this.gameObject)
                return;

            if (!other.TryGetComponent(out Interactable interactable)) 
                return;

            if (!currentInteractableObjects.Contains(interactable))
                return;
            
            currentInteractableObjects.Remove(interactable);
            UpdateUi();
        }
    }
}