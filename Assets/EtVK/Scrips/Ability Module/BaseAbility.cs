using EtVK.Utyles;
using UnityEngine;

namespace EtVK.Ability_Module
{
    public abstract class BaseAbility : MonoBehaviour
    {
        [Header("Base ability info")]
        [SerializeField] protected AbilityType abilityType;

        public AbilityType AbilityType => abilityType;

        public abstract void PerformAbility(BaseAbilityData baseAbilityData);
    }
}