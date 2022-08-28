using EtVK.AI_Module.Managers;
using EtVK.Core;
using EtVK.Utyles;
using UnityEngine;

namespace EtVK.AI_Module.States
{
    public class EnemyPatrolState : SceneLinkedSMB<EnemyManager>
    {
        public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            monoBehaviour.GetPatrolManager()
                .GoToNextWaypoint(monoBehaviour, monoBehaviour.GetLocomotionData().WalkSlowSpeed);
        }

        public override void OnSLTransitionToStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            HandlePatrol(animator);
        }

        public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            HandlePatrol(animator);
        }
        
        private void HandlePatrol(Animator animator)
        {
            monoBehaviour.GetController().HandleDetection();
            
            // If we detect a target we go and chase him
            if (monoBehaviour.GetController().HasCurrentTarget)
            {
                animator.SetInteger(EnemyAIState.AgentState.ToString(), EnemyAIState.CombatState.GetHashCode());
            }
            
            // If we reached the waypoint we go to idle State

            if (!monoBehaviour.GetController().HasCurrentTarget 
                && monoBehaviour.GetPatrolManager().WaypointReached(monoBehaviour))
            {
                animator.SetInteger(EnemyAIState.AgentState.ToString(), EnemyAIState.IdleState.GetHashCode());
            }
        }
    }
}