using EtVK.Ability_Module.Core;
using EtVK.Core.Controller;
using EtVK.Core.Utyles;
using UnityEngine;

namespace EtVK.Core.Manager
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
        private BlockingManager blockingManager;
        
        public bool UninterruptibleAction { get; set; }
        public bool IsBLocking { get; set; }
        public bool IsAiming { get; set; }

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
            blockingManager = GetComponentInChildren<BlockingManager>();
            SceneLinkedSMB<TManager>.Initialise(animator, this as TManager);
            
        }
        
        public void SetAimActionState(WeaponActionType weaponActionType, bool value, int layer)
        {
            switch (weaponActionType)
            {
                case WeaponActionType.Block:
                    IsBLocking = value;
                    blockingManager?.SetBlocking(value, layer);
                    break;
                case WeaponActionType.Aim:
                    IsAiming = value;
                    break;
            }
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