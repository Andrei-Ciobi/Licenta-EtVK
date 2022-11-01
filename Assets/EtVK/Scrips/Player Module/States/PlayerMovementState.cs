using EtVK.Core;
using EtVK.Input_Module;
using EtVK.Player_Module.Controller;
using EtVK.Player_Module.Manager;
using EtVK.Utyles;
using UnityEngine;

namespace EtVK.Player_Module.States
{
    public class PlayerMovementState : SceneLinkedSMB<PlayerManager>
    {
        [SerializeField] [Range(0f, .5f)] private float transitionDelay;
        [SerializeField] private bool canRun = true;
        [SerializeField] private bool normalMovement = true;

        public override void OnSLTransitionToStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            OnMovementUpdate(animator);
        }

        public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            OnMovementUpdate(animator);
        }

        private void OnMovementUpdate(Animator animator)
        {
            var inTransition = animator.GetBool(PlayerState.Transition.ToString());

            // Jump bool variable from animator
            if (InputManager.Instance.Player.TapJumpInput && !inTransition)
            {
                animator.SetBool(PlayerState.Jump.ToString(), true);
                return;
            }

            // Running bool variable from animator
            animator.SetBool(PlayerState.Running.ToString(), monoBehaviour.IsRunning(canRun));

            // Idle blendTree
            if (!monoBehaviour.IsMoving() && !monoBehaviour.IsJumping)
            {
                if (normalMovement)
                {
                    animator.SetFloat(PlayerState.Movement.ToString(), 0f, transitionDelay, Time.deltaTime);
                }
                else
                {
                    animator.SetFloat(PlayerState.LockOnMovementX.ToString(), 0f, transitionDelay, Time.deltaTime);
                    animator.SetFloat(PlayerState.LockOnMovementY.ToString(), 0f, transitionDelay, Time.deltaTime);
                }
            }

            // Walk blendTree
            if (monoBehaviour.IsMoving() && !monoBehaviour.IsRunning(canRun) && !monoBehaviour.IsJumping)
            {
                var movement = InputManager.Instance.Player.MovementInput;

                if (normalMovement)
                {
                    animator.SetFloat(PlayerState.Movement.ToString(), .5f, transitionDelay, Time.deltaTime);
                    monoBehaviour.GetController().UpdateNormalMovement(movement);
                }
                else
                {
                    animator.SetFloat(PlayerState.LockOnMovementX.ToString(), movement.x, transitionDelay,
                        Time.deltaTime);
                    animator.SetFloat(PlayerState.LockOnMovementY.ToString(), movement.y, transitionDelay,
                        Time.deltaTime);
                    monoBehaviour.GetController().UpdateLockOnMovement(movement);
                }
            }

            // Running blendTree
            if (monoBehaviour.IsRunning(canRun) && !monoBehaviour.IsJumping)
            {
                var movement = InputManager.Instance.Player.MovementInput;
                
                animator.SetFloat(PlayerState.LockOnMovementX.ToString(), 0f);
                animator.SetFloat(PlayerState.LockOnMovementY.ToString(), 2f, transitionDelay, Time.deltaTime);
                if (normalMovement)
                {
                    animator.SetFloat(PlayerState.Movement.ToString(), 1f, transitionDelay, Time.deltaTime);
                }
                else
                {
                    animator.SetFloat(PlayerState.LockOnMovementX.ToString(), 0f);
                    animator.SetFloat(PlayerState.LockOnMovementY.ToString(), 2f, transitionDelay, Time.deltaTime);
                }
                
                monoBehaviour.GetController()
                    .UpdateNormalMovement(movement, monoBehaviour.GetLocomotionData().RunSpeed);
            }

            monoBehaviour.GetController().MoveOnSlope();
        }
    }
}