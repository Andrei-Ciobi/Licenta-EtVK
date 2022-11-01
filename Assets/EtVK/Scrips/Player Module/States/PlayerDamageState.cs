using EtVK.Core;
using EtVK.Player_Module.Manager;
using UnityEngine;

namespace EtVK.Player_Module.States
{
    public class PlayerDamageState : SceneLinkedSMB<PlayerManager>
    {
        [SerializeField] private bool useRootMotion = true;
        [SerializeField] private bool useRootMotionRotation;
        public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            monoBehaviour.UninterruptibleAction = true;
            monoBehaviour.GetLivingEntity().IsInvulnerable = true;
            monoBehaviour.UseRootMotionRotation = useRootMotionRotation && useRootMotion;
            animator.applyRootMotion = useRootMotion;
        }

        public override void OnSLStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            monoBehaviour.GetLivingEntity().IsInvulnerable = false;
            monoBehaviour.UninterruptibleAction = false;
        }
    }
}