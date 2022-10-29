using UnityEngine;

namespace EtVK.Actions_Module
{
    [CreateAssetMenu( menuName = "ScriptableObjects/Actions/AttackAction")]
    public class AttackAction : BaseAction
    {
        [SerializeField] private bool comboIntoDifferentAttackType;
        [SerializeField] private bool useRootMotion;
        [SerializeField] private bool useRotation;
        
        [Header("VFX")] 
        [SerializeField] private GameObject vfx;
        [SerializeField] private GameObject postProcessing;
        [SerializeField] private AnimationCurve vfxCurve;

        public bool UseRotation => useRotation;
        public bool UseRootMotion => useRootMotion;
        public bool ComboIntoDifferentAttackType => comboIntoDifferentAttackType;
        public GameObject VFX => vfx;
        public GameObject PostProcessing => postProcessing;
        public AnimationCurve VFXCurve => vfxCurve;
    }
}