using EtVK.Core;
using EtVK.Input_Module;
using EtVK.Player_Module.Controller;
using EtVK.Utyles;
using UnityEngine;

namespace EtVK.Player_Module.States
{
    public class PlayerLockOnMovementState : SceneLinkedSMB<PlayerManager>
    {
        [SerializeField] [Range(0f, .5f)] private float transitionDelay;
        public override void OnSLTransitionToStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            OnLockOnMovement(animator);
        }

        public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            OnLockOnMovement(animator);
        }


        private void OnLockOnMovement(Animator animator)
        {
            var inTransition = animator.GetBool(PlayerState.Transition.ToString());
            
            // Jump bool variable from animator
            if (InputManager.Instance.Player.TapJumpInput && !inTransition)
            {
                animator.SetBool(PlayerState.Jump.ToString(), true);
                return;
            }

            if (InputManager.Instance.Player.DeactivateLockOn)
            {
                monoBehaviour.GetLockOnController().UnlockFromEnemy();
                return;
            }
            
            // Running bool variable from animator
            animator.SetBool(PlayerState.Running.ToString(), monoBehaviour.IsRunning());
            
            // Idle blendTree
            if (!monoBehaviour.IsMoving() && !monoBehaviour.IsJumping)
            {
                animator.SetFloat(PlayerState.LockOnMovementX.ToString(), 0f, transitionDelay, Time.deltaTime);
                animator.SetFloat(PlayerState.LockOnMovementY.ToString(), 0f, transitionDelay, Time.deltaTime);
            }
            
            // Walk blendTree
            if (monoBehaviour.IsMoving() && !monoBehaviour.IsRunning() && !monoBehaviour.IsJumping)
            {
                var movement = InputManager.Instance.Player.MovementInputClamped;
                
                animator.SetFloat(PlayerState.LockOnMovementX.ToString(), movement.x, transitionDelay, Time.deltaTime);
                animator.SetFloat(PlayerState.LockOnMovementY.ToString(), movement.y, transitionDelay, Time.deltaTime);
                monoBehaviour.GetController().UpdateLockOnMovement(movement);
            }
            
            // Running blendTree
            if (monoBehaviour.IsRunning() && !monoBehaviour.IsJumping)
            {
                var movement = InputManager.Instance.Player.MovementInput;

                animator.SetFloat(PlayerState.LockOnMovementX.ToString(), 0f);
                animator.SetFloat(PlayerState.LockOnMovementY.ToString(), 2f, transitionDelay, Time.deltaTime);
                monoBehaviour.GetController().UpdateNormalMovement(movement, monoBehaviour.GetLocomotionData().RunSpeed);
            }
            
            monoBehaviour.GetController().MoveOnSlope();
        }
    }
}