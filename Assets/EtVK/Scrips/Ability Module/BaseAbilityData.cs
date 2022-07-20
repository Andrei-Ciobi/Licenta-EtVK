using EtVK.Utyles;
using UnityEngine;

namespace EtVK.Ability_Module
{ 
    public abstract class BaseAbilityData : ScriptableObject
    {
        [SerializeField] private AbilityType abilityType;
        [SerializeField] private float cooldown;
        [SerializeField] private ParticleSystem vfx;   
        
        public AbilityType AbilityType => abilityType;

        public float Cooldown => cooldown;

        public ParticleSystem VFX => vfx;
    }
}