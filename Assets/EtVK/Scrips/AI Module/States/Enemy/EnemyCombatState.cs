using EtVK.AI_Module.Managers;
using EtVK.AI_Module.Weapons;
using EtVK.Core;
using EtVK.Utyles;
using UnityEngine;

namespace EtVK.AI_Module.States
{
    public class EnemyCombatState : SceneLinkedSMB<EnemyManager>
    {
        [Range(0f, 180f)] [SerializeField] private float rotateAngle = 30f;

        
        private float verticalMovementValue;
        private float horizontalMovementValue;
        private float verticalDamp;

        public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var inAttackRange = monoBehaviour.GetController()
                .TargetInRange(monoBehaviour.GetLocomotionData().BaseAttackRadius);
            if (!inAttackRange)
            {
                monoBehaviour.IsChasing = true;
                animator.applyRootMotion = false;
                monoBehaviour.UseRootMotionRotation = false;
                horizontalMovementValue = 0f;
                verticalMovementValue = 2f;
                verticalDamp = 0.1f;
                monoBehaviour.GetController().MoveAgent(true, monoBehaviour.GetLocomotionData().WalkFastSpeed);
                
            }
        }

        public override void OnSLTransitionToStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            HandleCombatBehaviour(animator);
            UpdateMovement(animator);
        }
        public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            HandleCombatBehaviour(animator);
            UpdateMovement(animator);
        }

        private void HandleCombatBehaviour(Animator animator)
        {
            var weapon = monoBehaviour.GetInventoryManager().GetCurrentWeapon();
            var inAggroRange = monoBehaviour.GetController()
                .TargetInRange(monoBehaviour.GetLocomotionData().AggroRange);
            var inCombatAggroRange = monoBehaviour.GetController()
                .TargetInRange(monoBehaviour.GetLocomotionData().CombatAggroRange);
            var inMeleeRange = monoBehaviour.GetController()
                .TargetInRange(MeleeRange(weapon));
            var inAttackRange = monoBehaviour.GetController()
                .TargetInRange(AttackRange(weapon));
            var isAttacking = animator.GetBool(EnemyAIAction.IsAttacking.ToString());
            
            //If we are chasing the target and we got to the melee range we stop chasing
            if (monoBehaviour.IsChasing && inMeleeRange)
            {
                monoBehaviour.GetController().MoveAgent(false);
            }
            
            // If we are in aggro range we walk around the target
            if (!monoBehaviour.IsChasing)
            {
                if (inAggroRange)
                {
                    WalkAroundTarget(inMeleeRange);
                    animator.applyRootMotion = true;
                    monoBehaviour.UseRootMotionRotation = true;
                }
                else
                {
                    animator.applyRootMotion = false;
                    monoBehaviour.UseRootMotionRotation = false;
                    horizontalMovementValue = 0f;
                    verticalMovementValue = 2f;
                    monoBehaviour.GetController().MoveAgent(true, monoBehaviour.GetLocomotionData().WalkFastSpeed);
                }
            }

            if (inAggroRange && weapon != null && monoBehaviour.CanAttack && !isAttacking)
            {
                monoBehaviour.DecideNextAttack(out var success);
                if (!success) 
                    return;
                
                animator.applyRootMotion = false;
                monoBehaviour.UseRootMotionRotation = false;
                monoBehaviour.IsChasing = false;
                horizontalMovementValue = 0f;
                verticalMovementValue = 0f;
            }
        }

        public override void OnSLStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            ExitState(animator);
        }

        private void UpdateMovement(Animator animator)
        {
            animator.SetFloat(EnemyAIAction.CombatHorizontal.ToString(), horizontalMovementValue, .1f, Time.smoothDeltaTime);
            animator.SetFloat(EnemyAIAction.CombatVertical.ToString(), verticalMovementValue, verticalDamp, Time.smoothDeltaTime);
        }

        private void ExitState(Animator animator)
        {
            animator.SetFloat(EnemyAIAction.CombatHorizontal.ToString(), 0f);
            animator.SetFloat(EnemyAIAction.CombatVertical.ToString(), 0f);
        }
        
        private void WalkAroundTarget(bool inRange)
        {
            verticalMovementValue = inRange ? 0f : 1f;
            verticalDamp = 0.6f;

            var lookTarget = monoBehaviour.GetController().CurrentTarget.transform.position;
            var angle = monoBehaviour.GetController().AngleBetweenGivenTarget(monoBehaviour.transform, lookTarget);

            if (angle > rotateAngle)
                horizontalMovementValue = -1f;
            else if (angle < -rotateAngle)
                horizontalMovementValue = 1f;
            else if(horizontalMovementValue == 0)
                horizontalMovementValue = 1f;
        }

        private float MeleeRange(EnemyWeapon weapon)
        {
            return weapon?.WeaponData.MeleeRange ?? monoBehaviour.GetLocomotionData().BaseMeleeRadius;
        }
        
        private float AttackRange(EnemyWeapon weapon)
        {
            return weapon?.WeaponData.AttackRange ?? monoBehaviour.GetLocomotionData().BaseAttackRadius;
        }
    }
}