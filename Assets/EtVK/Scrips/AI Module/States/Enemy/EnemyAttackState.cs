using EtVK.AI_Module.Actions;
using EtVK.AI_Module.Managers;
using EtVK.Core;
using EtVK.Utyles;
using UnityEngine;

namespace EtVK.AI_Module.States
{
    public class EnemyAttackState : SceneLinkedSMB<EnemyManager>
    {
        private EnemyAttackAction action;
        private bool combo;
        public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            monoBehaviour.GetController().MoveAgent(false);
            action = monoBehaviour.GetInventoryManager().GetCurrentWeapon().CurrentAttackAction;
            animator.applyRootMotion = action!.UseRootMotion;
            monoBehaviour.UseRootMotionRotation = action!.UseRotation;
            monoBehaviour.UninterruptibleAction = !action!.CanBeInterrupted;
            combo = false;
        }

        public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            CheckForCombo(animator);
        }

        public override void OnSLStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (combo) 
                return;
            
            animator.SetBool(EnemyAIAction.IsAttacking.ToString(), false);
            monoBehaviour.SetAttackOnCd(action.AttackCd);
            monoBehaviour.GetLivingEntity().IsInvulnerable = false;
            monoBehaviour.UninterruptibleAction = false;
        }

        private void CheckForCombo(Animator animator)
        {
            if(!action.HasCombo || combo)
                return;
            
            var canCombo = monoBehaviour.GetAnimationEventController().CanCombo;
            if(!canCombo)
                return;

            if(!monoBehaviour.InAttackParameters(action.ComboAction))
                return;
            
            var currentWeapon = monoBehaviour.GetInventoryManager().GetCurrentWeapon();
            currentWeapon.CurrentAttackAction = action.ComboAction;
            monoBehaviour.UpdateCurrentAttack(currentWeapon);
            combo = true;
            animator.CrossFade(action.ComboAction.ClipName, action.TransitionDuration);
        }

    }
}