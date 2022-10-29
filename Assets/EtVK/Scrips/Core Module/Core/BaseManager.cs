using System.Linq;
using EtVK.Ability_Module.Core;
using EtVK.Utyles;
using UnityEngine;

namespace EtVK.Core
{
    public class BaseManager<TManager, TController, TInventoryManager, TEntity > : MonoBehaviour 
        where TManager : BaseManager<TManager, TController, TInventoryManager, TEntity>, IBaseManager
    {
        public AnimatorOverrideController BaseAnimatorOverrideController => baseAnimatorOverrideController;
        
        protected Animator animator;
        protected TController controller;
        protected TInventoryManager inventoryManager;
        private AnimatorOverrideController baseAnimatorOverrideController;
        private TEntity livingEntity;
        private BaseAttackController attackController;
        private AbilityManager abilityManager;
        
        public bool UninterruptibleAction { get; set; }

        protected virtual void InitializeBaseReferences()
        {
            animator = GetComponentInChildren<Animator>();
            controller = GetComponent<TController>();
            inventoryManager = GetComponentInChildren<TInventoryManager>();
            livingEntity = GetComponent<TEntity>();
            baseAnimatorOverrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
            animator.runtimeAnimatorController = baseAnimatorOverrideController;
            attackController = GetComponent<BaseAttackController>();
            abilityManager = GetComponentInChildren<AbilityManager>();
            SceneLinkedSMB<TManager>.Initialise(animator, this as TManager);
            
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

        public BaseAttackController GetAttackController()
        {
            return attackController;
        }

        public AbilityManager GetAbilityManager()
        {
            return abilityManager;
        }
    }
}