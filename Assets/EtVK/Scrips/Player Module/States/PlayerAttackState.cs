using EtVK.Scrips.Core_Module;
using EtVK.Scrips.Player_Module.Controller;
using EtVK.Scrips.Utyles;
using UnityEngine;

namespace EtVK.Scrips.Player_Module.States
{
    public class PlayerAttackState : SceneLinkedSMB<PlayerManager>
    {
        public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool(PlayerState.IsAttacking.ToString(), false);
        }

        public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            monoBehaviour.GetController().UpdatePlayerRotation();

            if (monoBehaviour.CanAttack() && monoBehaviour.GetAnimationEventManager().CanCombo)
            {
                animator.SetBool(PlayerState.IsAttacking.ToString(), true);
            }
        }
        
        
    }
}