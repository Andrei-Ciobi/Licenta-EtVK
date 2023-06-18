using System.Collections;
using EtVK.Core.Utyles;
using EtVK.Event_Module.Event_Types;
using EtVK.Event_Module.Events;
using UnityEngine;

namespace EtVK.Ability_Module.Core
{
    public abstract class BaseAbility : MonoBehaviour
    {
        [Header("Base ability info")] 
        [SerializeField] protected AbilityType abilityType;

        public bool OnCooldown => onCooldown;
        public AbilityType AbilityType => abilityType;

        protected bool onCooldown;
        protected AbilityUiEvent updateUiEvent;

        public AbilityUiEvent SetUpdateEvent()
        {
            if (updateUiEvent == null)
                updateUiEvent = ScriptableObject.CreateInstance<AbilityUiEvent>();

            return updateUiEvent;
        }

        public abstract void PerformAbility(BaseAbilityData baseAbilityData, Animator animator = null,
            Transform obj = null);

        // protected bool SetAnimatorClip(AnimatorController animator, string stateName, AnimationClip clip)
        // {
        //     var abilitySubState = animator.layers[0].stateMachine.stateMachines
        //         .FirstOrDefault(s => s.stateMachine.name.Equals("Abilities"))
        //         .stateMachine;
        //
        //     if (abilitySubState == null)
        //     {
        //         Debug.LogError("No sub state machine with name = Abilities");
        //         return false;
        //     }
        //
        //     var state = abilitySubState.states.FirstOrDefault(s => s.state.name.Equals(stateName)).state;
        //     
        //     if (state == null)
        //     {
        //         Debug.LogError("No state with name = " + stateName);
        //         return false;
        //     }
        //
        //     animator.SetStateEffectiveMotion(state, clip);
        //     return true;
        // }
        
        protected bool SetAnimatorClip(Animator animator, string clipName, AnimationClip clip)
        {
            var animatorController = (AnimatorOverrideController) animator.runtimeAnimatorController;
            
            if (animatorController == null)
            {
                Debug.LogError("No animator override controller on animator = " + animator.gameObject.name);
                return false;
            }
            animatorController[clipName] = clip;
            
            return true;
        }

        protected IEnumerator AbilityCooldownCoroutine(float time)
        {
            var currentTime = time;
            updateUiEvent?.Invoke(new AbilityUiData( Mathf.RoundToInt(time), 1f));
            while (currentTime >= 0)
            {
                yield return null;
                currentTime -= Time.deltaTime;
                var roundedTime = Mathf.RoundToInt(currentTime);
                var percentage = currentTime / time;
                updateUiEvent?.Invoke(new AbilityUiData(roundedTime, percentage));
            }
            
            updateUiEvent?.Invoke(new AbilityUiData(0, 0f));
            onCooldown = false;
        }
    }
}