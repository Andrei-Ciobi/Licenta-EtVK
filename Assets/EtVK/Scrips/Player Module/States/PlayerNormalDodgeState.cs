using EtVK.Core;
using EtVK.Player_Module.Controller;
using EtVK.Player_Module.Manager;
using EtVK.Utyles;
using UnityEngine;

namespace EtVK.Player_Module.States
{
    public class PlayerNormalDodgeState : SceneLinkedSMB<PlayerManager>
    {
        public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.applyRootMotion = true;
            animator.SetBool(PlayerState.InDodge.ToString(), true);
            monoBehaviour.UninterruptibleAction = true;
            monoBehaviour.GetLivingEntity().IsInvulnerable = true;
            monoBehaviour.IsDodging = true;
        }

        public override void OnSLStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool(PlayerState.InDodge.ToString(), false);
            monoBehaviour.UninterruptibleAction = false;
            monoBehaviour.GetLivingEntity().IsInvulnerable = false;
            monoBehaviour.IsDodging = false;
        }
    }
}