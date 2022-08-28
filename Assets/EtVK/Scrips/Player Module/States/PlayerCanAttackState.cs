using EtVK.Core;
using EtVK.Player_Module.Controller;
using EtVK.Utyles;
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

        public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (monoBehaviour.CanAttack())
            {
                var weaponDraw = monoBehaviour.GetInventoryManager().GetCurrentWeapon();

                if (weaponDraw == null)
                {
                    Debug.Log("Can't attack without a weapon draw");
                    return;
                }
                
                animator.SetBool(PlayerState.IsAttacking.ToString(), true);
            }
        }
        
    }
}