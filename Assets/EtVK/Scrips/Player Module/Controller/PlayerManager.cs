using EtVK.Scrips.Core_Module;
using EtVK.Scrips.Input_Module;
using UnityEngine;

namespace EtVK.Scrips.Player_Module.Controller
{
    public class PlayerManager : MonoBehaviour
    {
        public bool IsJumping { get; set; }
        public Vector3 DownVelocity { get; set; }

        [SerializeField] private PlayerLocomotionData locomotionData;
        
        private PlayerController controller;
        private Animator animator;

        private void Awake()
        {
            InitializeReferences();
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

        private void InitializeReferences()
        {
            controller = GetComponentInChildren<PlayerController>();
            animator = GetComponentInChildren<Animator>();
        }
    }
}
