using EtVK.Scrips.Core_Module;
using EtVK.Scrips.Player_Module.Controller;
using EtVK.Scrips.Utyles;
using UnityEngine;

namespace EtVK.Scrips.Player_Module.States
{
    public class PlayerCanAttackState : SceneLinkedSMB<PlayerManager>
    {
        public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool(PlayerState.IsAttacking.ToString(), false);
            animator.SetBool(PlayerState.ComboAttack.ToString(), false);
            animator.applyRootMotion = false;
        }

        public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (monoBehaviour.CanAttack())
            {
                var weaponDraw = monoBehaviour.GetInventoryManager().GetArmedWeapon();

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