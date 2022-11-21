using System.Collections;
using EtVK.Core.Utyles;
using EtVK.Event_Module.Events;
using UnityEngine;

namespace EtVK.Resources_Module.Stamina
{
    public class StaminaManager : MonoBehaviour
    {
        [SerializeField] private StaminaData staminaData;
        [SerializeField] private FloatEvent updateStaminaEvent;

        private Coroutine recuperateCoroutine;
        private int currentStamina;
        private bool isRecuperating;

        private void Start()
        {
            Initialize();
        }

        private void Update()
        {
            if (!isRecuperating)
                return;

            RecuperateStamina();
        }

        public bool CheckCanPerformAction(StaminaCostType costType)
        {
            var cost = staminaData.GetCost(costType);

            var canPerform = currentStamina > cost;
            // Debug.Log(canPerform);

            return canPerform;
        }

        public void PerformStaminaDrain(StaminaCostType costType)
        {
            if (!CheckCanPerformAction(costType))
                return;

            var cost = staminaData.GetCost(costType);
            currentStamina -= cost;

            updateStaminaEvent.Invoke((float)currentStamina / staminaData.Stamina);

            if (recuperateCoroutine != null)
            {
                StopCoroutine(recuperateCoroutine);
            }

            recuperateCoroutine = StartCoroutine(RecuperateWaitCoroutine(staminaData.RecuperateWaitTime));
        }

        private void RecuperateStamina()
        {
            if (currentStamina >= staminaData.Stamina)
            {
                currentStamina = staminaData.Stamina;
                isRecuperating = false;
            }
            else
            {
                currentStamina += staminaData.RecuperateWeight;
            }

            updateStaminaEvent.Invoke((float)currentStamina / staminaData.Stamina);
        }

        private IEnumerator RecuperateWaitCoroutine(float waitTime)
        {
            isRecuperating = false;
            yield return new WaitForSeconds(waitTime);
            isRecuperating = true;
        }

        private void Initialize()
        {
            currentStamina = staminaData.Stamina;
            updateStaminaEvent.Invoke(1f);
        }
    }
}