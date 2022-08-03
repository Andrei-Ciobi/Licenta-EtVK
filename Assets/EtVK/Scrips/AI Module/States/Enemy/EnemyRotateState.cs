using EtVK.AI_Module.Managers;
using EtVK.Core_Module;
using EtVK.Utyles;
using UnityEngine;

namespace EtVK.AI_Module.States
{
    public class EnemyRotateState : SceneLinkedSMB<EnemyManager>
    {
        [SerializeField] private RotateAround rotation;
        [Range(0f, 180f)] [SerializeField] private float angleLimit;
        
        private Vector3 rotateTowards;
        private float angle;

        public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            DecideRotation();
            angle = monoBehaviour.GetController().AngleBetweenGivenTarget(animator.transform, rotateTowards);
            // We set the blend tree to the desired rotation
            if (!(Mathf.Abs(angle) > angleLimit)) 
                return;
            
            animator.applyRootMotion = true;
            animator.SetBool(EnemyAIAction.IsRotating.ToString(), true);
            animator.SetFloat(EnemyAIAction.Rotation.ToString(), Mathf.Clamp(angle, -1f, 1f), 0f, Time.deltaTime);
        }

        public override void OnSLTransitionToStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            CheckRotationAngle(animator);
            HandleDetection(animator);
        }

        public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            CheckRotationAngle(animator);
            HandleDetection(animator);
        }

        private void DecideRotation()
        {
            switch (rotation)
            {
                case RotateAround.CurentTarget:
                    rotateTowards = monoBehaviour.GetController().CurrentTarget.position;
                    break;
                case RotateAround.CurentWaypoint:
                    rotateTowards = monoBehaviour.GetPatrolManager().CurrentWaypoint;
                    break;
            }
        }

        private void CheckRotationAngle(Animator animator)
        {
            angle = monoBehaviour.GetController().AngleBetweenGivenTarget(animator.transform, rotateTowards);
            
            if (Mathf.Abs(angle) < angleLimit)
            {
                animator.SetBool(EnemyAIAction.IsRotating.ToString(), false);
            }
        }
        
        private void HandleDetection(Animator animator)
        {
            monoBehaviour.GetController().HandleDetection();
            
            // If we detect a target we go and chase him
            if(!monoBehaviour.GetController().HasCurrentTarget)
                return;
            
            animator.SetInteger(EnemyAIState.AgentState.ToString(), EnemyAIState.CombatState.GetHashCode());
            OnExitState(animator);
        }

        private void OnExitState(Animator animator)
        {
            animator.SetBool(EnemyAIAction.IsPatrolling.ToString(), false);
            animator.SetFloat(EnemyAIAction.Rotation.ToString(), 0f);
            if (rotation != RotateAround.CurentTarget)
                animator.SetBool(EnemyAIAction.IsRotating.ToString(), false);
            
        }
    }
}