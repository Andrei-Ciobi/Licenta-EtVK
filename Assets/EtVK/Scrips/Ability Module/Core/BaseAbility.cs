using System.Collections;
using EtVK.Core.Utyles;
using UnityEngine;

namespace EtVK.Ability_Module.Core
{
    public abstract class BaseAbility : MonoBehaviour
    {
        [Header("Base ability info")] 
        [SerializeField] protected AbilityType abilityType;

        public bool OnCooldown => onCooldown;

        protected bool onCooldown;
        public AbilityType AbilityType => abilityType;

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
            yield return new WaitForSeconds(time);
            onCooldown = false;
        }
    }
}