using System;
using UnityEngine;

namespace EtVK.Core.Utyles
{
    public class ResetBlendState : StateMachineBehaviour
    {
        [SerializeField] [Range(0f, 1f)] private float value;
        [SerializeField] [Range(0f, 1f)] private float lerpWeight;
        [SerializeField] private bool checkForBlock;

        private bool blocked;
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            blocked = animator.IsInTransition(layerIndex) && checkForBlock;
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            if(blocked)
                return;
            
            var weight = animator.GetLayerWeight(layerIndex);
            if (Math.Abs(weight - value) <= 0f)
                return;

            animator.SetLayerWeight(layerIndex, Mathf.Lerp(weight, value, lerpWeight));
        }
    }
}