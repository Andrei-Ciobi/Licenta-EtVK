using System;
using System.Collections.Generic;
using System.Linq;
using EtVK.Core;
using EtVK.Input_Module;
using UnityEngine;

namespace EtVK.Ability_Module.Core
{
    public class AbilityManager : MonoBehaviour
    {
        [SerializeField] private Transform abilityHolder;
        [SerializeField] private List<BaseAbility> abilities = new();

        private IBaseManager managerData;
        private Animator animator;
        private void Awake()
        {
            AddAbilityReference(abilityHolder);
            managerData = transform.root.GetComponent<IBaseManager>();
            animator = transform.root.GetComponentInChildren<Animator>();
        }

        private void Update()
        {
            CheckForAbilityInput();
        }


        public void PerformAbility(BaseAbilityData abilityData, Animator animator)
        {
            var ability = abilities.Find(x => x.AbilityType == abilityData.AbilityType);

            if (ability == null)
            {
                Debug.Log("No reference of ability type = " + abilityData.AbilityType);
                return;
            }
            
            ability.PerformAbility(abilityData, animator);
        }
        
        // Setters for the reference
        public void AddAbilityReference(BaseAbility ability)
        {
            if(ability == null)
                return;
            
            if (abilities.Contains(ability))
                return;

            abilities.Add(ability);
        }

        public void AddAbilityReference(IEnumerable<BaseAbility> abilityList)
        {
            if(abilityList == null)
                return;
            
            foreach (var ability in abilityList.Where(ability => !abilities.Contains(ability)))
            {
                abilities.Add(ability);
            }
        }
        
        public void AddAbilityReference(Transform gameObjRoot)
        {
            if(gameObjRoot == null)
                return;
            
            var abilityList = gameObjRoot.GetComponentsInChildren<BaseAbility>().ToList()
                .Where(x => !abilities.Contains(x));
            
            abilities.AddRange(abilityList);
        }
        
        public void RemoveAbilityReference(BaseAbility ability)
        {
            if(ability == null)
                return;
            
            if (!abilities.Contains(ability))
                return;

            abilities.Remove(ability);
        }

        public void RemoveAbilityReference(IEnumerable<BaseAbility> abilityList)
        {
            if(abilityList == null)
                return;
            
            foreach (var ability in abilityList.Where(ability => abilities.Contains(ability)))
            {
                abilities.Remove(ability);
            }
        }
        
        public void RemoveAbilityReference(Transform gameObjRoot)
        {
            if(gameObjRoot == null)
                return;
            
            var abilityList = gameObjRoot.GetComponentsInChildren<BaseAbility>().ToList();
            foreach (var ability in abilityList.Where(ability => abilities.Contains(ability)))
            {
                abilities.Remove(ability);
            }
        }

        private void CheckForAbilityInput()
        {
            if(!InputManager.Instance.Player.TapRunInput)
                return;
            
            if(managerData.UninterruptibleAction)
                return;

            var abilityType = InputManager.Instance.Player.AbilityPressed;
            var ability = abilities.Find(x => x.AbilityType == abilityType);
            
            if(ability == null)
                return;
            
            if(ability.OnCooldown)
                return;
            
            animator.CrossFade(abilityType.ToString(), 0.1f);
        }

    }
}