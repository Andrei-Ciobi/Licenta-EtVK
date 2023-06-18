using System.Collections;
using EtVK.Core;
using EtVK.Core.Utyles;
using UnityEngine;
using UnityEngine.InputSystem;

namespace EtVK.Input_Module
{
    public class InputManager : MonoSingleton<InputManager>
    {
        [SerializeField] [Range(0f, 2f)] private float queInputTime = 0.3f;

        public PlayerInputs Player => playerInputs;
        public UiInputs Ui => uiInputs;
        public PlayerInputActions.PlayerActions PlayerCallbacks => playerActions.Player;
        public PlayerInputActions.UIActions UICallbacks => playerActions.UI;

        private PlayerInputs playerInputs;
        private UiInputs uiInputs;
        private PlayerInputActions playerActions;
        private Coroutine blockInputCoroutine;
        private Coroutine blockAttackInputCoroutine;

        private bool inputQueSet;
        private float currentQueInputTime;

        private void Awake()
        {
            InitializeSingleton();
            playerActions = new PlayerInputActions();
            InitializeInputs();
        }

        private void Start()
        {
            InitializeInputCallbacks();
        }

        private void Update()
        {
            HandleQueInputs();
        }

        private void OnEnable()
        {
            playerActions.Enable();
            playerActions.Player.Enable();
            playerActions.UI.Enable();
        }

        private void OnDisable()
        {
            playerActions.Player.Disable();
            playerActions.UI.Disable();
        }

        public void EnablePlayerActionMap()
        {
            playerActions.Player.Enable();
        }

        public void EnableUIActionMap()
        {
            playerActions.UI.Enable();
        }

        public void DisablePlayerActionMap()
        {
            playerActions.Player.Disable();
        }

        public void DisableUIActionMap()
        {
            playerActions.UI.Disable();
        }

        public void BeginJumpCdCoroutine(float jumpCdTime)
        {
            StartCoroutine(JumpCdCoroutine(jumpCdTime));
        }

        public void BeginBlockInputCoroutine(float blockInputCd)
        {
            if (blockInputCd == 0)
            {
                return;
            }

            if (blockInputCoroutine != null)
            {
                StopCoroutine(blockInputCoroutine);
            }

            playerInputs.SpecificInputBlocked = true;
            blockInputCoroutine = StartCoroutine(BlockInputCoroutine(blockInputCd));
        }

        public void BeginBlockAttackInputCoroutine(float blockInputCd)
        {
            if (blockInputCd == 0)
            {
                return;
            }

            if (blockAttackInputCoroutine != null)
            {
                StopCoroutine(blockAttackInputCoroutine);
            }

            playerInputs.AttackInputBlocked = true;
            blockAttackInputCoroutine = StartCoroutine(BlockAttackInputCoroutine(blockInputCd));
        }

        private void InitializeInputCallbacks()
        {
            // Player
            playerActions.Player.HoldRun.performed += _ => playerInputs.HoldRun = true;
            playerActions.Player.HoldRun.canceled += _ => playerInputs.HoldRun = false;
            playerActions.Player.HoldAttack.performed += _ => playerInputs.ChannelingAttack = true;
            playerActions.Player.HoldAttack.canceled += _ => playerInputs.ChannelingAttack = false;
            playerActions.Player.TapRun.performed += _ => SetAbilityPressed(AbilityButtonType.Shift);
            playerActions.Player.TapAttack.performed += _ =>
            {
                ResetQueInput();
                playerInputs.TapAttackInputQue = true;
                QueInput();
            };
            playerActions.Player.HoldAim.performed += _ => playerInputs.Aim = true;
            playerActions.Player.HoldAim.canceled += _ => playerInputs.Aim = false;
            playerActions.Player.MouseLook.performed += OnMouseLook;
            playerActions.Player.Movement.performed += OnMovementInput;
            playerActions.Player.Weapon_1.performed += _ => OnWeaponInput(WeaponType.Sword);
            playerActions.Player.Weapon_2.performed += _ => OnWeaponInput(WeaponType.GreatSword);
            playerActions.Player.DeactivateLockOn.performed += _ => playerInputs.DeactivateLockOn = true;
            playerActions.Player.DeactivateLockOn.canceled += _ => playerInputs.DeactivateLockOn = false;
            //UI
            playerActions.UI.ScrollWheel.performed += OnScrollWheel;
            // playerActions.UI.Escape.performed += OnEscapePerformed;
        }

        private void OnMovementInput(InputAction.CallbackContext context)
        {
            playerInputs.MovementInput = context.ReadValue<Vector2>();
        }

        private void OnMouseLook(InputAction.CallbackContext context)
        {
            playerInputs.MouseLook = context.ReadValue<Vector2>();
        }

        private void OnScrollWheel(InputAction.CallbackContext context)
        {
            uiInputs.ScrollWheel = context.ReadValue<Vector2>();
        }

        private void OnWeaponInput(WeaponType weaponType)
        {
            if (playerInputs.SwitchWeaponInput)
                return;

            playerInputs.SwitchWeaponInput = true;
            playerInputs.SwitchWeaponType = weaponType;
        }

        private IEnumerator JumpCdCoroutine(float jumpCdTime)
        {
            yield return new WaitForSeconds(jumpCdTime);
            playerInputs.JumpInputBlocked = false;
        }

        private IEnumerator BlockInputCoroutine(float blockInputCd)
        {
            yield return new WaitForSeconds(blockInputCd);
            playerInputs.SpecificInputBlocked = false;
        }

        private IEnumerator BlockAttackInputCoroutine(float blockInputCd)
        {
            yield return new WaitForSeconds(blockInputCd);
            playerInputs.AttackInputBlocked = false;
        }

        private void InitializeInputs()
        {
            playerInputs = new PlayerInputs(playerActions);
            uiInputs = new UiInputs(playerActions);
        }

        private void QueInput()
        {
            inputQueSet = true;
            currentQueInputTime = queInputTime;
        }

        private void ResetQueInput()
        {
            playerInputs.TapAttackInputQue = false;
        }

        private void HandleQueInputs()
        {
            if (!inputQueSet)
                return;

            if (currentQueInputTime > 0)
            {
                currentQueInputTime -= Time.deltaTime;
            }
            else
            {
                ResetQueInput();
                inputQueSet = false;
                currentQueInputTime = 0;
            }
        }

        private void SetAbilityPressed(AbilityButtonType abilityType)
        {
            playerInputs.AbilityButtonPressed = abilityType;
        }

        public class PlayerInputs
        {
            private readonly PlayerInputActions playerActions;
            public bool HoldRun { get; set; }
            public bool ChannelingAttack { get; set; }
            public bool Aim { get; set; }
            public bool AttackInputBlocked { get; set; }
            public bool SpecificInputBlocked { get; set; }
            public bool DeactivateLockOn { get; set; }
            public bool JumpInputBlocked { get; set; }
            public bool SwitchWeaponInput { get; set; }
            public bool TapAttackInputQue { get; set; }
            public WeaponType SwitchWeaponType { get; set; }
            public AbilityButtonType AbilityButtonPressed { get; set; }
            public bool TapJumpInput => playerActions.Player.TapJump.triggered;
            public bool TapRunInput => playerActions.Player.TapRun.triggered;
            public bool TapInteractInput => playerActions.Player.TapInteract.triggered;
            public bool TapAttackInput => playerActions.Player.TapAttack.triggered;
            public bool TapDodge => playerActions.Player.TapDodge.triggered;
            public bool ActivateLockOn => playerActions.Player.ActivateLockOn.triggered;
            public bool TapEscape => playerActions.Player.Escape.triggered;
            public Vector2 MovementInput { get; set; }

            public Vector2 MovementInputClamped => new Vector2(Mathf.Clamp(MovementInput.x * 2.1f, -1f, 1f),
                Mathf.Clamp(MovementInput.y * 2.1f, -1f, 1f));

            public Vector2 MouseLook { get; set; }

            public PlayerInputs(PlayerInputActions playerActions)
            {
                this.playerActions = playerActions;
            }
        }

        public class UiInputs
        {
            private PlayerInputActions playerActions;
            public bool TapEscape => playerActions.UI.Cancel.triggered;
            public Vector2 ScrollWheel { get; set; }

            public UiInputs(PlayerInputActions playerActions)
            {
                this.playerActions = playerActions;
            }
        }
    }
}