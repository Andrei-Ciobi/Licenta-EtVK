﻿using EtVK.Scrips.Core_Module;
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
        public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool(PlayerState.IsAttacking.ToString(), false);
            animator.SetBool(PlayerState.ComboAttack.ToString(), false);
            var weapon = monoBehaviour.GetInventoryManager().GetArmedWeapon();
            var attackAction = weapon.WeaponData.GetAttackAction(attackType, attackIndex - 1);

            endAttackContinue = attackAction.ComboIntoDifferentAttackType;
            canContinueCombo = weapon.WeaponData.GetMaxComboForAttackType(attackType) > attackIndex;

            animator.applyRootMotion = attackAction.UseRootMotion;
        }

        public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            //monoBehaviour.GetController().UpdatePlayerRotation();

            if (monoBehaviour.CanAttack() && monoBehaviour.GetAnimationEventManager().CanCombo && !checkedForAttack)
            {
                checkedForAttack = false;
                
                animator.SetBool(PlayerState.IsAttacking.ToString(), canContinueCombo || endAttackContinue);
                animator.SetBool(PlayerState.ComboAttack.ToString(), canContinueCombo);
            }
        }

        public override void OnSLStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if(animator.applyRootMotion)
            {
                var newPosition = animator.rootPosition;
                newPosition.y = monoBehaviour.transform.position.y;
                monoBehaviour.transform.position = newPosition;
            }
        }
    }
}