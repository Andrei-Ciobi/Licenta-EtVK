using EtVK.Utyles;
using UnityEngine;

namespace EtVK.Ability_Module.Core
{ 
    public abstract class BaseAbilityData : ScriptableObject
    {
        [SerializeField] private AbilityType abilityType;
        [SerializeField] private float cooldown;
        [SerializeField] private GameObject vfx;
        [SerializeField] private GameObject postProcessing;
        [SerializeField] private AnimationCurve vfxCurve;
        
        public AbilityType AbilityType => abilityType;
        public float Cooldown => cooldown;
        public GameObject VFX => vfx;
        public GameObject PostProcessing => postProcessing;
        public AnimationCurve VFXCurve => vfxCurve;
    }
}