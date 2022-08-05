using System.Linq;
using EtVK.Ability_Module;
using EtVK.Utyles;
using UnityEngine;

namespace EtVK.Core_Module
{
    public class BaseManager<TManager, TController, TInventoryManager, TEntity > : MonoBehaviour 
        where TManager : BaseManager<TManager, TController, TInventoryManager, TEntity>
    {
        protected Animator animator;
        private TController controller;
        private TInventoryManager inventoryManager;
        private TEntity livingEntity;
        
        protected virtual void InitializeBaseReferences()
        {
            animator = GetComponentInChildren<Animator>();
            controller = GetComponent<TController>();
            inventoryManager = GetComponentInChildren<TInventoryManager>();
            livingEntity = GetComponent<TEntity>();
            SceneLinkedSMB<TManager>.Initialise(animator, this as TManager);
        }
        
        public BaseAbility GetAbility(AbilityType abilityType)
        {
            var abilities = GetComponentsInChildren<BaseAbility>().ToList();
            var ability = abilities.Find(element => element.AbilityType.Equals(abilityType));

            if (ability == null)
            {
                Debug.LogError($"No ability of type {abilityType} found under {gameObject.name} gameObject");
                return null;
            }

            return ability;
        }
        
        public Animator GetAnimator()
        {
            return animator;
        }

        public TController GetController()
        {
            return controller;
        }

        public TInventoryManager GetInventoryManager()
        {
            return inventoryManager;
        }

        public TEntity GetLivingEntity()
        {
            return livingEntity;
        }
        
        
    }
}