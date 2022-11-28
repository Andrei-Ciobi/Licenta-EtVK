using EtVK.Core;
using EtVK.Core.Utyles;
using EtVK.Input_Module;
using EtVK.Player_Module.Manager;
using UnityEngine;

namespace EtVK.Player_Module.States
{
    public class PlayerCanAttackState : SceneLinkedSMB<PlayerManager>
    {
        public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool(PlayerState.IsAttacking.ToString(), false);
            animator.SetBool(PlayerState.ComboAttack.ToString(), false);
            animator.applyRootMotion = false;
            monoBehaviour.UseRootMotionRotation = false;
        }

        public override void OnSLTransitionToStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            CheckCanAttack(animator);
        }

        public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            CheckCanAttack(animator);
        }

        private void CheckCanAttack(Animator animator)
        {
            if (!monoBehaviour.CanAttack())
                return;

            var weaponDraw = monoBehaviour.GetInventoryManager().GetCurrentWeapon();

            if (weaponDraw == null)
            {
                monoBehaviour.ErrorUiMessage(monoBehaviour.ErrorUiData.AttackWithNoWeaponDraw,
                    InputManager.Instance.Player.TapAttackInput);
                return;
            }

            animator.SetBool(PlayerState.IsAttacking.ToString(), true);
            // animator.CrossFade("Attacks", transitionDelay);
        }
    }
}