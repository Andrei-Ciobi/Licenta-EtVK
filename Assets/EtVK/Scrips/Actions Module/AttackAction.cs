using EtVK.VFX_Module;
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
        [SerializeField] private VolumeVFX volumeVFX;

        public bool UseRotation => useRotation;
        public bool UseRootMotion => useRootMotion;
        public bool ComboIntoDifferentAttackType => comboIntoDifferentAttackType;

        public VolumeVFX VolumeVFX => volumeVFX;
    }
}