using EtVK.AI_Module.Managers;
using EtVK.Core_Module;
using EtVK.Utyles;
using UnityEngine;
using UnityEngine.Serialization;

namespace EtVK.AI_Module.States
{
    public class EnemyRotateState : SceneLinkedSMB<EnemyManager>
    {
        [FormerlySerializedAs("rotation")] [SerializeField] private RotateAround rotateTowardsType;
        [Range(0f, 180f)] [SerializeField] private float angleLimit;
        
        private Vector3 rotateTowards;
        private float angle;
        private float rotation;

        public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            DecideRotation();
            angle = monoBehaviour.GetController().AngleBetweenGivenTarget(animator.transform, rotateTowards);
            // We set the blend tree to the desired rotation
            if (Mathf.Abs(angle) < angleLimit) 
                return;
            
            animator.applyRootMotion = true;
            animator.SetBool(EnemyAIAction.IsRotating.ToString(), true);
            rotation = Mathf.Clamp(angle, -1f, 1f);
        }

        public override void OnSLTransitionToStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            // DecideRotation();
            CheckRotationAngle(animator);
            HandleDetection(animator);
        }

        public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            // DecideRotation();
            CheckRotationAngle(animator);
            HandleDetection(animator);
        }

        public override void OnSLStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            UpdateRotation(animator);
        }

        public override void OnSLStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetFloat(EnemyAIAction.Rotation.ToString(), 0f);
        }


        private void DecideRotation()
        {
            switch (rotateTowardsType)
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
            angle = monoBehaviour.GetController().AngleBetweenGivenTarget(animator.transform,
                monoBehaviour.GetController().CurrentTarget.position);
            rotation = Mathf.Clamp(angle, -1f, 1f);

            if (!(Mathf.Abs(angle) < angleLimit)) 
                return;
            
            animator.SetBool(EnemyAIAction.IsRotating.ToString(), false);
        }
        
        private void HandleDetection(Animator animator)
        {
            
            if(rotateTowardsType == RotateAround.CurentTarget)
                return;
            
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
            animator.SetBool(EnemyAIAction.IsRotating.ToString(), false);
            
        }


        private void UpdateRotation(Animator animator)
        {
            animator.SetFloat(EnemyAIAction.Rotation.ToString(), rotation, 0.3f, Time.deltaTime);
        }
    }
}