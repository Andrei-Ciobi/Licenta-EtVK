using EtVK.AI_Module.Actions;
using EtVK.AI_Module.Managers;
using EtVK.Core_Module;
using EtVK.Utyles;
using UnityEngine;

namespace EtVK.AI_Module.States
{
    public class EnemyAttackState : SceneLinkedSMB<EnemyManager>
    {
        private EnemyAttackAction action;
        public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            monoBehaviour.GetController().MoveAgent(false);
            action = monoBehaviour.GetInventoryManager().GetCurrentWeapon().CurrentAttackAction;
            animator.applyRootMotion = action!.UseRootMotion;
            monoBehaviour.UseRootMotionRotation = action!.UseRotation;
        }

        public override void OnSLStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool(EnemyAIAction.IsAttacking.ToString(), false);
            monoBehaviour.SetAttackOnCd(action.AttackCd);
        }
    }
}