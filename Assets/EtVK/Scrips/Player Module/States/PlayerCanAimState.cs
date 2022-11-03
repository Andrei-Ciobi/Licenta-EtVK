using EtVK.Actions_Module;
using EtVK.Core;
using EtVK.Core.Utyles;
using EtVK.Input_Module;
using EtVK.Player_Module.Manager;
using UnityEngine;

namespace EtVK.Player_Module.States
{
    public class PlayerCanAimState : SceneLinkedSMB<PlayerManager>
    {
        [SerializeField] private bool restrictToNoTransition;
        [SerializeField] private bool detectAimInput = true;

        public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var isBLocking = animator.GetBool(WeaponActionType.Block.ToString());
            
            var weapon = monoBehaviour.GetInventoryManager().GetCurrentWeapon();

            if (weapon == null)
                return;

            var aimAction = weapon.WeaponData.GetBaseActionAs<AimAction>(x => x.ActionType == WeaponActionType.Block);

            if (aimAction == null)
                return;

            if (!detectAimInput)
            {
                animator.SetBool(aimAction.ActionType.ToString(), true);
            }


            if(!isBLocking)
                return;
            animator.SetLayerWeight(aimAction.Layer, 1f);
        }

        public override void OnSLTransitionToStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if(restrictToNoTransition)
                return;
            
            if(detectAimInput)
            {
                CheckAimHold(animator);
            }
            else
            {
                CheckAimRelease(animator);
            }
        }

        public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if(detectAimInput)
            {
                CheckAimHold(animator);
            }
            else
            {
                CheckAimRelease(animator);
            }
        }

        private void CheckAimHold(Animator animator)
        {
            var isAimActionActive = animator.GetBool(AnimatorCommonFileds.IsAimAction.ToString());
            
            if(isAimActionActive)
                return;
            
            if (!InputManager.Instance.Player.Aim)
                return;

            var weapon = monoBehaviour.GetInventoryManager().GetCurrentWeapon();

            if (weapon == null)
                return;

            var aimAction = weapon.WeaponData.GetBaseActionAs<AimAction>(x => true);

            if (aimAction == null)
                return;
            
            animator.SetBool(AnimatorCommonFileds.IsAimAction.ToString(), true);
            animator.CrossFade("Aim Action Movement", aimAction.TransitionDelay);
            monoBehaviour.SetAimActionState(aimAction.ActionType, true, aimAction.Layer);
            
            if(!aimAction.UseLayerBlend)
                return;
            
            animator.CrossFade(aimAction.BlendStateName, aimAction.BlendDelay, aimAction.Layer);
        }

        private void CheckAimRelease(Animator animator)
        {
            var isAimActionActive = animator.GetBool(AnimatorCommonFileds.IsAimAction.ToString());
            
            if(!isAimActionActive)
                return;
            
            if (InputManager.Instance.Player.Aim)
                return;
            
            var weapon = monoBehaviour.GetInventoryManager().GetCurrentWeapon();

            if (weapon == null)
                return;

            var aimAction = weapon.WeaponData.GetBaseActionAs<AimAction>(x => true);

            if (aimAction == null)
                return;
            
            animator.SetBool(AnimatorCommonFileds.IsAimAction.ToString(), false);
            animator.SetBool(aimAction.ActionType.ToString(), false);
            monoBehaviour.SetAimActionState(aimAction.ActionType, false, 0);
            // animator.SetLayerWeight(aimAction.Layer, 0f);
        }

    }
}