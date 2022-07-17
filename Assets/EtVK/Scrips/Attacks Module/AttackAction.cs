using UnityEngine;

namespace EtVK.Scrips.Attacks_Module
{
    [CreateAssetMenu( menuName = "ScriptableObjects/Attacks/AttackAction")]
    public class AttackAction : ScriptableObject
    {
        [SerializeField] private string clipName;
        [SerializeField] private AnimationClip animationClip;
        [SerializeField] private bool comboIntoDifferentAttackType;

        public bool ComboIntoDifferentAttackType => comboIntoDifferentAttackType;

        public string ClipName => clipName;

        public AnimationClip AnimationClip => animationClip;
    }
}