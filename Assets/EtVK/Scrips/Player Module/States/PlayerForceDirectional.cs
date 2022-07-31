using EtVK.Core_Module;
using EtVK.Input_Module;
using EtVK.Player_Module.Controller;
using EtVK.Utyles;
using UnityEngine;

namespace EtVK.Player_Module.States
{
    public class PlayerForceDirectional : SceneLinkedSMB<PlayerManager>
    {
        [SerializeField] private AnimationCurve speedGraph;
        [Range(0.01f, 1f)]
        [SerializeField] private float interval;
        [SerializeField] private float speed;

        private Vector2 movement;

        public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            movement = InputManager.Instance.MovementInput;
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
                monoBehaviour.GetController().UpdateForceMovementDirectional(movement, evaluatedSpeed);
            }
        }
    }
}