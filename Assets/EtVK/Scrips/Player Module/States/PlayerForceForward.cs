using EtVK.Core_Module;
using EtVK.Player_Module.Controller;
using UnityEngine;

namespace EtVK.Player_Module.States
{
    public class PlayerForceForward : SceneLinkedSMB<PlayerManager>
    {
        [SerializeField] private AnimationCurve speedGraph;
        [Range(0.01f, 1f)]
        [SerializeField] private float interval;
        [SerializeField] private float speed;

        public override void OnSLTransitionToStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            ForceForward(stateInfo);
        }
        public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            ForceForward(stateInfo);
        }
        private void ForceForward(AnimatorStateInfo stateInfo)
        {
            if (stateInfo.normalizedTime < Mathf.Clamp(stateInfo.length, 0f, 1f) - interval)
            {
                monoBehaviour.GetController().UpdateForceMovementForward(speed * speedGraph.Evaluate(stateInfo.normalizedTime));
            }
        }
    }
}