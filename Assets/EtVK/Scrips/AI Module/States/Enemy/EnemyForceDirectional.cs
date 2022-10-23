using EtVK.AI_Module.Controllers;
using EtVK.AI_Module.Managers;
using EtVK.Core;
using UnityEngine;

namespace EtVK.AI_Module.States
{
    public class EnemyForceDirectional : SceneLinkedSMB<EnemyManager>
    {
        [SerializeField] private AnimationCurve speedGraph;
        [Range(0.01f, 1f)] [SerializeField] private float interval;
        [SerializeField] private float speed;

        private Vector2 movement;

        public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var attackController = (EnemyAttackController)monoBehaviour.GetAttackController();
            movement = -attackController.Direction;
        }

        public override void OnSLTransitionToStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            ForceDirectional(stateInfo, animator);
        }

        public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            ForceDirectional(stateInfo, animator);
        }

        private void ForceDirectional(AnimatorStateInfo stateInfo, Animator animator)
        {
            if (stateInfo.normalizedTime < Mathf.Clamp(stateInfo.length, 0f, 1f) - interval)
            {
                var evaluatedSpeed = speed * speedGraph.Evaluate(stateInfo.normalizedTime);
                monoBehaviour.GetController().Move(movement, evaluatedSpeed);
            }
        }
    }
}