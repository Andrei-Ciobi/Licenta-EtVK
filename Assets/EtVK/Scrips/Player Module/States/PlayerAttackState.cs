using EtVK.Scrips.Core_Module;
using EtVK.Scrips.Player_Module.Controller;
using EtVK.Scrips.Utyles;
using UnityEngine;

namespace EtVK.Scrips.Player_Module.States
{
    public class PlayerAttackState : SceneLinkedSMB<PlayerManager>
    {
        [SerializeField] private AttackType attackType;
        [SerializeField] private int attackIndex;
        private bool canContinueCombo;
        private bool endAttackContinue;
        private bool checkedForAttack;
        private bool useRotation;
        public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool(PlayerState.IsAttacking.ToString(), false);
            animator.SetBool(PlayerState.ComboAttack.ToString(), false);
            var weapon = monoBehaviour.GetInventoryManager().GetArmedWeapon();
            var attackAction = weapon.WeaponData.GetAttackAction(attackType, attackIndex - 1);
            useRotation = attackAction.UseRotation;

            endAttackContinue = attackAction.ComboIntoDifferentAttackType;
            canContinueCombo = weapon.WeaponData.GetMaxComboForAttackType(attackType) > attackIndex;

            animator.applyRootMotion = attackAction.UseRootMotion;
            monoBehaviour.UseRootMotionRotation = attackAction.UseRootMotion && useRotation;
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
            }
        }
    }
}