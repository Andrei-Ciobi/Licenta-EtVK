using System.Collections;
using EtVK.Utyles;
using UnityEngine;

namespace EtVK.Ability_Module.Core
{
    public abstract class BaseAbility : MonoBehaviour
    {
        [Header("Base ability info")]
        [SerializeField] protected AbilityType abilityType;

        public bool OnCooldown => onCooldown;
        
        protected bool onCooldown;
        public AbilityType AbilityType => abilityType;

        public abstract void PerformAbility(BaseAbilityData baseAbilityData, Animator animator);

        protected IEnumerator AbilityCooldownCoroutine(float time)
        {
            yield return new WaitForSeconds(time);
            onCooldown = false;
        }
    }
}