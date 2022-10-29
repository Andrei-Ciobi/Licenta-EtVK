using EtVK.Core;
using EtVK.Player_Module.Manager;
using EtVK.Utyles;
using UnityEngine;

namespace EtVK.Player_Module.States
{
    public class PlayerAttackState : SceneLinkedSMB<PlayerManager>
    {
        public AttackType AttackType => attackType;
        public int AttackIndex => attackIndex;

        [SerializeField] private AttackType attackType;
        [SerializeField] private int attackIndex;
        private bool canContinueCombo;
        private bool endAttackContinue;
        private bool checkedForAttack;
        private bool useRotation;
        private bool combo;
        public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool(PlayerState.IsAttacking.ToString(), false);
            animator.SetBool(PlayerState.ComboAttack.ToString(), false);
            var weapon = monoBehaviour.GetInventoryManager().GetCurrentWeapon();
            var attackAction = weapon.WeaponData.GetAttackAction(attackType, attackIndex - 1);
            useRotation = attackAction.UseRotation;
            combo = false;

            endAttackContinue = attackAction.ComboIntoDifferentAttackType;
            canContinueCombo = weapon.WeaponData.GetMaxComboForAttackType(attackType) > attackIndex;

            animator.applyRootMotion = attackAction.UseRootMotion;
            monoBehaviour.UseRootMotionRotation = attackAction.UseRootMotion && useRotation;

            monoBehaviour.UninterruptibleAction = !attackAction.CanBeInterrupted;
            monoBehaviour.IsPerformingAttack = true;
        }

        public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (!monoBehaviour.UseRootMotionRotation && useRotation)
            {
                monoBehaviour.GetController().UpdatePlayerRotation();
            }

            if (monoBehaviour.CanAttack() && monoBehaviour.GetAnimationEventManager().CanCombo && !checkedForAttack)
            {
                checkedForAttack = false;
                
                animator.SetBool(PlayerState.IsAttacking.ToString(), canContinueCombo || endAttackContinue);
                animator.SetBool(PlayerState.ComboAttack.ToString(), canContinueCombo);
                combo = canContinueCombo || endAttackContinue;
            }
        }

        public override void OnSLStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (!combo)
            {
                monoBehaviour.IsPerformingAttack = false;
            }
            
            if(monoBehaviour.IsDodging)
                return;
            
            monoBehaviour.UninterruptibleAction = false;
        }
    }
}