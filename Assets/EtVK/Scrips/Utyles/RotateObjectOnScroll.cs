using System;
using EtVK.Input_Module;
using UnityEngine;

namespace EtVK.Utyles
{
    public class RotateObjectOnScroll : MonoBehaviour
    {
        [SerializeField] private float rotationAmount = 1f;
        [SerializeField] private float rotationSpeed = 5f;
        
        
        private Vector3 currentRotation;
        private Vector3 targetRotation;
        private void Start()
        {
            currentRotation = transform.eulerAngles;
            targetRotation = transform.eulerAngles;
        }

        private void Update()
        {
            var scroll = InputManager.Instance.ScrollWheel;
            if (scroll.y > 0)
            {
                targetRotation.y -= rotationAmount;
            }

            if (scroll.y < 0)
            {
                targetRotation.y += rotationAmount;
            }

            currentRotation = Vector3.Lerp(currentRotation, targetRotation, rotationSpeed * Time.deltaTime);
            transform.eulerAngles = currentRotation;
        }
    }
}