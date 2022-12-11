using EtVK.Core.Utyles;
using EtVK.VFX_Module;
using UnityEngine;

namespace EtVK.Ability_Module.Core
{ 
    public abstract class BaseAbilityData : ScriptableObject
    {
        [SerializeField] private bool displayOnUi;
        [SerializeField] private AbilityType abilityType;
        [SerializeField] private AbilityButtonType abilityButtonType;
        [SerializeField] private float cooldown;
        [SerializeField] private GameObject vfx;
        [SerializeField] private VolumeVFX volumeVFX;

        public bool DisplayOnUi => displayOnUi;
        public AbilityType AbilityType => abilityType;
        public AbilityButtonType AbilityButtonType => abilityButtonType;
        public float Cooldown => cooldown;
        public GameObject VFX => vfx;
        public VolumeVFX VolumeVFX => volumeVFX;
    }
}