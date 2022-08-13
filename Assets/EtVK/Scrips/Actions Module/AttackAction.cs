using UnityEngine;

namespace EtVK.Actions_Module
{
    [CreateAssetMenu( menuName = "ScriptableObjects/Actions/AttackAction")]
    public class AttackAction : BaseAction
    {
        [SerializeField] private bool comboIntoDifferentAttackType;
        [SerializeField] private bool useRootMotion;
        [SerializeField] private bool useRotation;

        public bool UseRotation => useRotation;

        public bool UseRootMotion => useRootMotion;

        public bool ComboIntoDifferentAttackType => comboIntoDifferentAttackType;

       
    }
}