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
        public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool(PlayerState.IsAttacking.ToString(), false);
            animator.SetBool(PlayerState.ComboAttack.ToString(), false);
            var weapon = monoBehaviour.GetInventoryManager().GetArmedWeapon();

            canContinueCombo = weapon.WeaponData.GetMaxComboForAttackType(attackType) > attackIndex;
        }

        public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            monoBehaviour.GetController().UpdatePlayerRotation();

            if (monoBehaviour.CanAttack() && monoBehaviour.GetAnimationEventManager().CanCombo && !checkedForAttack)
            {
                checkedForAttack = false;
                
                if (!canContinueCombo)
                {
                    var weapon = monoBehaviour.GetInventoryManager().GetArmedWeapon();
                    endAttackContinue = weapon.WeaponData.GetAttackAction(attackType,
                        animator.GetCurrentAnimatorClipInfo(layerIndex)[0].clip).ComboIntoDifferentAttackType;
                    animator.SetBool(PlayerState.IsAttacking.ToString(), endAttackContinue);
                }
                else
                {
                    animator.SetBool(PlayerState.IsAttacking.ToString(), true);
                }
                
               
                animator.SetBool(PlayerState.ComboAttack.ToString(), canContinueCombo);
            }
        }
        
        
    }
}