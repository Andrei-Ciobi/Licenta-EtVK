using UnityEngine;

namespace EtVK.Attacks_Module
{
    [CreateAssetMenu( menuName = "ScriptableObjects/Attacks/AttackAction")]
    public class AttackAction : ScriptableObject
    {
        [SerializeField] private string clipName;
        [SerializeField] private AnimationClip animationClip;
        [SerializeField] private bool comboIntoDifferentAttackType;
        [SerializeField] private bool useRootMotion;
        [SerializeField] private bool useRotation;

        public bool UseRotation => useRotation;

        public bool UseRootMotion => useRootMotion;

        public bool ComboIntoDifferentAttackType => comboIntoDifferentAttackType;

        public string ClipName => clipName;

        public AnimationClip AnimationClip => animationClip;
    }
}