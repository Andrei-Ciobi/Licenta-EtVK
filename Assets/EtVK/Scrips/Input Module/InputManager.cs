using System.Collections;
using EtVK.Core_Module;
using EtVK.Utyles;
using UnityEngine;
using UnityEngine.InputSystem;

namespace EtVK.Input_Module
{
    public class InputManager : MonoSingletone<InputManager>
    {
        public bool HoldRun { get; private set; }
        public bool ChannelingAttack { get; private set; }
        public bool Aim { get; private set; }
        public bool AttackInputBlocked { get; private set; }
        public bool SpecificInputBlocked { get; private set; }
        public bool JumpInputBlocked { get; set; }
        public bool SwitchWeaponInput { get; set; }
        public WeaponType SwitchWeaponType { get; set; }
        public bool TapJumpInput => playerActions.Player.TapJump.triggered;
        public bool TapRunInput => playerActions.Player.TapRun.triggered;
        public bool TapInteractInput => playerActions.Player.TapInteract.triggered;
        public bool TapAttackInput => playerActions.Player.TapAttack.triggered;
        public bool TapDodge => playerActions.Player.TapDodge.triggered;
        public bool LockOnInput => playerActions.Player.StateSwitch.triggered;
        public Vector2 MovementInput { get; private set; }
        public Vector2 MouseLook { get; private set; }


        private PlayerInputActions playerActions;
        private Coroutine blockInputCoroutine;
        private Coroutine blockAttackInputCoroutine;

        private void Awake()
        {
            InitializeSingletone();
            playerActions = new PlayerInputActions();
        }
        
        private void Start()
        {
            InitializeInputCallbacks();
        }
        
        private void OnEnable()
        {
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

            SpecificInputBlocked = true;
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

            AttackInputBlocked = true;
            blockAttackInputCoroutine = StartCoroutine(BlockAttackInputCoroutine(blockInputCd));
        }
        
        private void InitializeInputCallbacks()
        {

            // Player
            playerActions.Player.HoldRun.performed += _ => HoldRun = true;
            playerActions.Player.HoldRun.canceled += _ => HoldRun = false;
            playerActions.Player.HoldAttack.performed += _ => ChannelingAttack = true;
            playerActions.Player.HoldAttack.canceled += _ => ChannelingAttack = false;
            // playerActions.Player.HoldAim.performed += OnAimPerformed;
            playerActions.Player.HoldAim.canceled += _ => Aim = false;
            playerActions.Player.MouseLook.performed += OnMouseLook;
            playerActions.Player.Movement.performed += OnMovementInput;
            playerActions.Player.Weapon_1.performed += _ => OnWeaponInput(WeaponType.Sword);
            playerActions.Player.Weapon_2.performed += _ => OnWeaponInput(WeaponType.Bow);
            //UI
            // playerActions.UI.Escape.performed += OnEscapePerformed;

            playerActions.Enable();
        }
        
        private void OnMovementInput(InputAction.CallbackContext context)
        {
            MovementInput = context.ReadValue<Vector2>();
        }

        private void OnMouseLook(InputAction.CallbackContext context)
        {
            MouseLook = context.ReadValue<Vector2>();
        }

        private void OnWeaponInput(WeaponType weaponType)
        {
            if(SwitchWeaponInput)
                return;
            
            SwitchWeaponInput = true;
            SwitchWeaponType = weaponType;
        }

        private IEnumerator JumpCdCoroutine(float jumpCdTime)
        {
            yield return new WaitForSeconds(jumpCdTime);
            JumpInputBlocked = false;
        }

        private IEnumerator BlockInputCoroutine(float blockInputCd)
        {
            yield return new WaitForSeconds(blockInputCd);
            SpecificInputBlocked = false;
        }

        private IEnumerator BlockAttackInputCoroutine(float blockInputCd)
        {
            yield return new WaitForSeconds(blockInputCd);
            AttackInputBlocked = false;
        }
    }
}
