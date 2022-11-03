using EtVK.Core;
using EtVK.Core.Utyles;
using EtVK.Input_Module;
using EtVK.Player_Module.Manager;
using UnityEngine;

namespace EtVK.Player_Module.States
{
    public class PlayerJumpState : SceneLinkedSMB<PlayerManager>
    {
        [SerializeField] private float jumpCdTime;
        private float currentAirSpeed;
        private Vector2 movement;
        public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            monoBehaviour.GetController().Jump();
            currentAirSpeed = monoBehaviour.GetLocomotionData().WalkFastSpeed;
            movement = Vector2.zero;
        }

        public override void OnSLTransitionToStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            AirMovementUpdate(animator);
        }
        
        public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            AirMovementUpdate(animator);
        }
        public override void OnSLStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (!monoBehaviour.IsJumping)
            {
                animator.SetBool(PlayerState.Jump.ToString(), false);
            }
        }

        public override void OnSLStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            InputManager.Instance.BeginJumpCdCoroutine(jumpCdTime);
        }


        private void AirMovementUpdate(Animator animator)
        {
            if (monoBehaviour.IsRunning())
            {
                currentAirSpeed = monoBehaviour.GetLocomotionData().RunSpeed;
            }

            if (monoBehaviour.IsMoving())
            { 
                movement = InputManager.Instance.Player.MovementInput;
            }

            monoBehaviour.GetController().UpdateNormalMovement(movement, currentAirSpeed);
        }
    }
}
