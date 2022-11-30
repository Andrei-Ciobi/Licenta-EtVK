using EtVK.Core;
using EtVK.Core.Utyles;
using EtVK.Player_Module.Manager;
using UnityEngine;

namespace EtVK.Player_Module.States
{
    public class PlayerNormalDodgeState : SceneLinkedSMB<PlayerManager>
    {
        public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.applyRootMotion = true;
            monoBehaviour.UninterruptibleAction = true;
            monoBehaviour.GetLivingEntity().IsInvulnerable = true;
            monoBehaviour.IsDodging = true;
        }

        public override void OnSLStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            monoBehaviour.UninterruptibleAction = false;
            monoBehaviour.GetLivingEntity().IsInvulnerable = false;
            monoBehaviour.IsDodging = false;
        }
    }
}