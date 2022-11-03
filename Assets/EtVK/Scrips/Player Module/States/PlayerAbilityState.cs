using EtVK.Core;
using EtVK.Core.Utyles;
using EtVK.Player_Module.Manager;
using UnityEngine;

namespace EtVK.Player_Module.States
{
    public class PlayerAbilityState : SceneLinkedSMB<PlayerManager>
    {
        public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            monoBehaviour.UninterruptibleAction = true;
            animator.SetBool(AnimatorCommonFileds.AbilityFinished.ToString(), false);
        }

        public override void OnSLStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            monoBehaviour.UninterruptibleAction = false;
        }
    }
}