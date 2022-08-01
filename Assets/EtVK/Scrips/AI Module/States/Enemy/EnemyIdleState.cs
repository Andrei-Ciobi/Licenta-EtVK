using System;
using EtVK.AI_Module.Core;
using EtVK.Core_Module;
using EtVK.Utyles;
using UnityEngine;

namespace EtVK.AI_Module.States
{
    public class EnemyIdleState : SceneLinkedSMB<EnemyManager>
    {
        [Header("Rotate angle limit")] [Range(0f, 180f)] 
        [SerializeField] private float angleLimit;
        
        private float currentWaitTime;

        public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var isPatrolling = animator.GetBool(EnemyAIAction.IsPatrolling.ToString());
            
            if(!isPatrolling)
                return;
            
            animator.SetBool(EnemyAIAction.IsPatrolling.ToString(), false);
            currentWaitTime = 0f;
        }

        public override void OnSLTransitionToStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            HandleDetection(animator);
        }

        public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            HandlePatrolMovement(animator);
            HandleDetection(animator);
        }

        private void HandleDetection(Animator animator)
        {
            monoBehaviour.GetController().HandleDetection();
            
            // If we detect a target we go and chase him
            if(!monoBehaviour.GetController().HasCurrentTarget)
                return;
            
            animator.SetInteger(EnemyAIState.AgentState.ToString(), EnemyAIState.CombatState.GetHashCode());
            
        }
        
        private void HandlePatrolMovement(Animator animator)
        {
            if(!monoBehaviour.HasPatrolPath())
                return;
            
            // if we waited enough we go to the next waypoint
            if (Math.Abs(currentWaitTime - monoBehaviour.GetLocomotionData().PatrolWaitTime) < 0.1f)
            {
                animator.SetBool(EnemyAIAction.IsPatrolling.ToString(), true);
                animator.SetInteger(EnemyAIState.AgentState.ToString(), EnemyAIState.VoidState.GetHashCode());

                var angle = monoBehaviour.GetController().AngleBetweenGivenTarget(monoBehaviour.transform,
                    monoBehaviour.GetPatrolManager().CurrentWaypoint);

                if (Mathf.Abs(angle) > angleLimit)
                {
                    animator.SetFloat(EnemyAIAction.Angle.ToString(), angle);
                    animator.SetBool(EnemyAIAction.IsRotating.ToString(), true);
                }
            }
            
            currentWaitTime += 1f * Time.deltaTime;
            currentWaitTime = Mathf.Clamp(currentWaitTime, 0f, monoBehaviour.GetLocomotionData().PatrolWaitTime);
            
        }
    }
}