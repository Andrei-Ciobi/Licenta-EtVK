using EtVK.AI_Module.Managers;
using EtVK.Core;
using UnityEngine;

namespace EtVK.AI_Module.States
{
    public class EnemyDamageState : SceneLinkedSMB<EnemyManager>
    {
        [SerializeField] private bool useRootMotion = true;
        [SerializeField] private bool useRootMotionRotation;
        public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            monoBehaviour.GetController().MoveAgent(false);
            animator.applyRootMotion = useRootMotion;
            monoBehaviour.UseRootMotionRotation = useRootMotionRotation;
            monoBehaviour.GetLivingEntity().IsInvulnerable = true;
            monoBehaviour.UninterruptibleAction = true;
        }

        public override void OnSLStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            monoBehaviour.GetLivingEntity().IsInvulnerable = false;
            monoBehaviour.UninterruptibleAction = false;
        }
    }
}