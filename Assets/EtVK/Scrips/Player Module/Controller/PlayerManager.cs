using System.Linq;
using EtVK.Ability_Module;
using EtVK.Core_Module;
using EtVK.Input_Module;
using EtVK.Inventory_Module;
using EtVK.Utyles;
using UnityEngine;

namespace EtVK.Player_Module.Controller
{
    public class PlayerManager : MonoBehaviour
    {
        public bool IsJumping { get; set; }
        public Vector3 DownVelocity { get; set; }
        
        public bool UseRootMotionRotation { get; set; }

        public AnimatorOverrideController BaseAnimatorOverrideController => baseAnimatorOverrideController;

        [SerializeField] private PlayerLocomotionData locomotionData;
        
        private PlayerController controller;
        private Animator animator;
        private AnimatorOverrideController baseAnimatorOverrideController;
        private InventoryManager inventoryManager;
        private AnimationEventManager animationEventManager;
        private RootMotionController rootMotionController;

        private void Awake()
        {
            InitializeReferences();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            SceneLinkedSMB<PlayerManager>.Initialise(animator, this);
        }
        
        public bool IsMoving()
        {
            return InputManager.Instance.MovementInput != Vector2.zero;
        }

        public bool IsRunning()
        {
            return InputManager.Instance.HoldRun && IsMoving();
        }

        public bool CanAttack()
        {
            var isAttacking = animator.GetBool(PlayerState.IsAttacking.ToString());
            return InputManager.Instance.TapAttackInput && !isAttacking;
        }

        public PlayerController GetController()
        {
            return controller;
        }

        public Animator GetAnimator()
        {
            return animator;
        }

        public PlayerLocomotionData GetLocomotionData()
        {
            return locomotionData;
        }

        public InventoryManager GetInventoryManager()
        {
            return inventoryManager;
        }

        public AnimationEventManager GetAnimationEventManager()
        {
            return animationEventManager;
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

        private void InitializeReferences()
        {
            controller = GetComponentInChildren<PlayerController>();
            animator = GetComponentInChildren<Animator>();
            inventoryManager = GetComponentInChildren<InventoryManager>();
            animationEventManager = GetComponentInChildren<AnimationEventManager>();
            rootMotionController = GetComponentInChildren<RootMotionController>();
            rootMotionController.Initialize(this);

            baseAnimatorOverrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        }
    }
}
