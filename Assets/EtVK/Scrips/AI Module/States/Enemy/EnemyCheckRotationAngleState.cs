using EtVK.AI_Module.Managers;
using EtVK.Core_Module;
using EtVK.Utyles;
using UnityEngine;

namespace EtVK.AI_Module.States
{
    public class EnemyCheckRotationAngleState : SceneLinkedSMB<EnemyManager>
    {
        [SerializeField] [Range(0f, 180f)] private float angleLimit;
        public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var angle = monoBehaviour.GetController().AngleBetweenGivenTarget(monoBehaviour.transform,
                monoBehaviour.GetPatrolManager().CurrentWaypoint);

            var rotate = Mathf.Abs(angle) > angleLimit;
            animator.SetBool(EnemyAIAction.IsRotating.ToString(), rotate);

        }
    }
}