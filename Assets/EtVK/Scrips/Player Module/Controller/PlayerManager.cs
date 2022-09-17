using System;
using EtVK.Core;
using EtVK.Health_Module;
using EtVK.Input_Module;
using EtVK.Inventory_Module;
using EtVK.Utyles;
using UnityEngine;

namespace EtVK.Player_Module.Controller
{
    public class PlayerManager : BaseManager<PlayerManager, PlayerController, PlayerInventoryManager, PlayerEntity>, IFullGameComponent
    {
        [SerializeField] private bool startFullGame;
        [SerializeField] private PlayerLocomotionData playerLocomotionData;
        
        public bool StartFullGame { get => startFullGame; set => startFullGame = value; }
        public bool IsJumping { get; set; }
        public Vector3 DownVelocity { get; set; }
        public bool UseRootMotionRotation { get; set; }
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
        
        public bool IsMoving()
        {
            return InputManager.Instance.Player.MovementInput != Vector2.zero;
        }

        public bool IsRunning()
        {
            return InputManager.Instance.Player.HoldRun && IsMoving();
        }

        public bool CanAttack()
        {
            var isAttacking = animator.GetBool(PlayerState.IsAttacking.ToString());
            return InputManager.Instance.Player.TapAttackInput && !isAttacking;
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

        private void OnDestroy()
        {
            if(!startFullGame)
                return;
            GameManager.Instance.onLateFinishLoading -= OnFinishLoadingLate;
            GameManager.Instance.onChangeGameState -= OnGameStateChange;
        }
    }
}
