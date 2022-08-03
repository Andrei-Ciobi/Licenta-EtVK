using UnityEngine;

namespace EtVK.Actions_Module
{
    [CreateAssetMenu( menuName = "ScriptableObjects/Actions/ForceForward")]
    public class ForceForwardAttackAction : AttackAction
    {
        [Header("Force forward parameters")]
        [SerializeField] private bool useForceForward;
        [SerializeField] private AnimationCurve speedGraph;
        [Range(0.01f, 1f)]
        [SerializeField] private float interval;
        [SerializeField] private float speed;

        public bool UseForceForward => useForceForward;

        public AnimationCurve SpeedGraph => speedGraph;

        public float Interval => interval;

        public float Speed => speed;
    }
}