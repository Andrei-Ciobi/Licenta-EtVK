using EtVK.Core.Manager;
using EtVK.Core.Utyles;
using EtVK.Health_Module;
using EtVK.Input_Module;
using EtVK.Inventory_Module;
using EtVK.Player_Module.Controller;
using UnityEngine;
using UnityEngine.InputSystem;

namespace EtVK.Player_Module.Manager
{
    public class PlayerManager : BaseManager<PlayerManager, PlayerController, PlayerInventoryManager, PlayerEntity>,
        IFullGameComponent, IBaseManager
    {
        [SerializeField] private bool startFullGame;
        [SerializeField] private PlayerLocomotionData playerLocomotionData;

        public bool StartFullGame
        {
            get => startFullGame;
            set => startFullGame = value;
        }

        public bool IsJumping { get; set; }
        public Vector3 DownVelocity { get; set; }
        public bool UseRootMotionRotation { get; set; }
        public bool IsDodging { get; set; }
        public bool IsPerformingAttack { get; set; }
        public Transform CameraMainTransform => cameraMainTransform;

        private PlayerAnimationEventController animationEventController;
        private PlayerRootMotionController playerRootMotionController;
        private LockOnController lockOnController;
        private Transform cameraMainTransform;

        private void Awake()
        {
            InitializeBaseReferences();
            InitializeReferences();
            // Cursor.lockState = CursorLockMode.Locked;
            // Cursor.visible = false;
        }

        private void Start()
        {
            InitializeCallbacks();
        }

        private void OnDodge(InputAction.CallbackContext context)
        {
            if (UninterruptibleAction || IsBLocking || IsAiming)
                return;

            if (!IsMoving())
                return;

            var isLockedOn = animator.GetBool(PlayerState.IsLockedOn.ToString());
            animationEventController.SetCanCombo(0);
            animationEventController.DeactivateWeaponCollider();
            if (isLockedOn || IsPerformingAttack)
            {
                var movement = InputManager.Instance.Player.MovementInputClamped;

                animator.SetFloat(PlayerState.LockOnMovementX.ToString(), movement.x);
                animator.SetFloat(PlayerState.LockOnMovementY.ToString(), movement.y);
                animator.CrossFade("Directional Dodge", .1f);
            }
            else
            {
                animator.CrossFade("Normal Dodge", .1f);
            }
        }

        public bool IsMoving()
        {
            return InputManager.Instance.Player.MovementInput != Vector2.zero;
        }

        public bool IsRunning(bool canRun = true)
        {
            return InputManager.Instance.Player.HoldRun && IsMoving() && canRun;
        }

        public bool CanAttack()
        {
            var isAttacking = animator.GetBool(PlayerState.IsAttacking.ToString());
            return (InputManager.Instance.Player.TapAttackInput || InputManager.Instance.Player.TapAttackInputQue) &&
                   !isAttacking && !IsBLocking;
        }

        public PlayerLocomotionData GetLocomotionData()
        {
            return playerLocomotionData;
        }

        public PlayerAnimationEventController GetAnimationEventManager()
        {
            return animationEventController;
        }

        public LockOnController GetLockOnController()
        {
            return lockOnController;
        }

        private void OnFinishLoadingLate()
        {
            animator.enabled = true;
        }

        private void OnGameStateChange(bool state)
        {
            animator.enabled = !state;
        }

        private void InitializeReferences()
        {
            animator = GetComponentInChildren<Animator>();
            animationEventController = GetComponentInChildren<PlayerAnimationEventController>();
            playerRootMotionController = GetComponentInChildren<PlayerRootMotionController>();
            lockOnController = GetComponentInChildren<LockOnController>();
            playerRootMotionController.Initialize(this);
            cameraMainTransform = UnityEngine.Camera.main!.transform;

            if (startFullGame)
            {
                GameManager.Instance.onLateFinishLoading += OnFinishLoadingLate;
                GameManager.Instance.onChangeGameState += OnGameStateChange;
            }
            else
            {
                animator.enabled = true;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

        private void InitializeCallbacks()
        {
            InputManager.Instance.PlayerCallbacks.TapDodge.performed += OnDodge;
        }

        private void OnDestroy()
        {
            if (!startFullGame)
                return;
            GameManager.Instance.onLateFinishLoading -= OnFinishLoadingLate;
            GameManager.Instance.onChangeGameState -= OnGameStateChange;
            InputManager.Instance.PlayerCallbacks.TapDodge.performed -= OnDodge;
        }
    }
}