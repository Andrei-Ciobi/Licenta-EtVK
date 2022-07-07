using EtVK.Scrips.Core_Module;
using EtVK.Scrips.Player_Module.Controller;
using EtVK.Scrips.Utyles;
using UnityEngine;

namespace EtVK.Scrips.Player_Module.States
{
    public class PlayerAttackState : SceneLinkedSMB<PlayerManager>
    {
        [SerializeField] private int attackIndex;
        private bool canContinueCombo;
        public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool(PlayerState.IsAttacking.ToString(), false);
            var weapon = monoBehaviour.GetInventoryManager().GetArmedWeapon();

            canContinueCombo = weapon.WeaponData.MaxComboAttacks > attackIndex;
        }

        public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            monoBehaviour.GetController().UpdatePlayerRotation();

            if (monoBehaviour.CanAttack() && monoBehaviour.GetAnimationEventManager().CanCombo && canContinueCombo)
            {
                animator.SetBool(PlayerState.IsAttacking.ToString(), true);
            }
        }
        
        
    }
}