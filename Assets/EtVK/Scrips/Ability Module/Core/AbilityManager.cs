using System;
using System.Collections.Generic;
using System.Linq;
using EtVK.Core;
using EtVK.Input_Module;
using UnityEngine;

namespace EtVK.Ability_Module.Core
{
    public abstract class AbilityManager : MonoBehaviour
    {
        [SerializeField] private Transform abilityHolder;
        [SerializeField] protected List<BaseAbilityData> abilities;
        
        private List<BaseAbility> abilityReferences = new();


        public void PerformAbility(BaseAbilityData abilityData, Animator animator = null, Transform obj = null)
        {
            var ability = abilityReferences.Find(x => x.AbilityType == abilityData.AbilityType);

            if (ability == null)
            {
                Debug.Log("No reference of ability type = " + abilityData.AbilityType);
                return;
            }
            
            if(ability.OnCooldown)
                return;
            
            ability.PerformAbility(abilityData, animator, obj);
        }
        
        // Setters for the reference
        public void AddAbilityReference(BaseAbility ability)
        {
            if(ability == null)
                return;
            
            if (abilityReferences.Contains(ability))
                return;

            abilityReferences.Add(ability);
        }

        public void AddAbilityReference(IEnumerable<BaseAbility> abilityList)
        {
            if(abilityList == null)
                return;
            
            foreach (var ability in abilityList.Where(ability => !abilityReferences.Contains(ability)))
            {
                abilityReferences.Add(ability);
            }
        }
        
        public void AddAbilityReference(Transform gameObjRoot)
        {
            if(gameObjRoot == null)
                return;
            
            var abilityList = gameObjRoot.GetComponentsInChildren<BaseAbility>().ToList()
                .Where(x => !abilityReferences.Contains(x));
            
            abilityReferences.AddRange(abilityList);
        }
        
        public void RemoveAbilityReference(BaseAbility ability)
        {
            if(ability == null)
                return;
            
            if (!abilityReferences.Contains(ability))
                return;

            abilityReferences.Remove(ability);
        }

        public void RemoveAbilityReference(IEnumerable<BaseAbility> abilityList)
        {
            if(abilityList == null)
                return;
            
            foreach (var ability in abilityList.Where(ability => abilityReferences.Contains(ability)))
            {
                abilityReferences.Remove(ability);
            }
        }
        
        public void RemoveAbilityReference(Transform gameObjRoot)
        {
            if(gameObjRoot == null)
                return;
            
            var abilityList = gameObjRoot.GetComponentsInChildren<BaseAbility>().ToList();
            foreach (var ability in abilityList.Where(ability => abilityReferences.Contains(ability)))
            {
                abilityReferences.Remove(ability);
            }
        }

        protected void BaseInitialize()
        {
            AddAbilityReference(abilityHolder);
        }
    }
}