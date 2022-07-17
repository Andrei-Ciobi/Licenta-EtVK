using EtVK.Scrips.Core_Module;
using EtVK.Scrips.Input_Module;
using EtVK.Scrips.Inventory_Module;
using EtVK.Scrips.Utyles;
using UnityEngine;

namespace EtVK.Scrips.Player_Module.Controller
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

        private void Awake()
        {
            InitializeReferences();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            SceneLinkedSMB<PlayerManager>.Initialise(animator, this);
        }

        private void LateUpdate()
        {
            if (UseRootMotionRotation)
            {
                controller.UpdatePlayerRootMotionRotation(animator);
            }
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

        private void InitializeReferences()
        {
            controller = GetComponentInChildren<PlayerController>();
            animator = GetComponentInChildren<Animator>();
            inventoryManager = GetComponentInChildren<InventoryManager>();
            animationEventManager = GetComponentInChildren<AnimationEventManager>();

            baseAnimatorOverrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        }
    }
}
