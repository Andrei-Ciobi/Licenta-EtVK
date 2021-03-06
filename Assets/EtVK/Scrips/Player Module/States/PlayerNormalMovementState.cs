using EtVK.Scrips.Core_Module;
using EtVK.Scrips.Input_Module;
using EtVK.Scrips.Player_Module.Controller;
using EtVK.Scrips.Utyles;
using UnityEngine;

namespace EtVK.Scrips.Player_Module.States
{
    public class PlayerNormalMovementState : SceneLinkedSMB<PlayerManager>
    {
        [SerializeField] [Range(0f, .5f)] private float transitionDelay;

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
            if (InputManager.Instance.TapJumpInput && !inTransition)
            {
                animator.SetBool(PlayerState.Jump.ToString(), true);
                return;
            }

            // Running bool variable from animator
            animator.SetBool(PlayerState.Running.ToString(), monoBehaviour.IsRunning());

            // Idle blendTree
            if (!monoBehaviour.IsMoving() && !monoBehaviour.IsJumping)
            {
                animator.SetFloat(PlayerState.Movement.ToString(), 0f, transitionDelay, Time.deltaTime);
            }

            // Walk blendTree
            if (monoBehaviour.IsMoving() && !monoBehaviour.IsRunning() && !monoBehaviour.IsJumping)
            {
                var movement = InputManager.Instance.MovementInput;

                animator.SetFloat(PlayerState.Movement.ToString(), .5f, transitionDelay, Time.deltaTime);
                monoBehaviour.GetController().UpdateNormalMovement(movement);
            }

            // Running blendTree
            if (monoBehaviour.IsRunning() && !monoBehaviour.IsJumping)
            {
                var movement = InputManager.Instance.MovementInput;

                animator.SetFloat(PlayerState.Movement.ToString(), 1f, transitionDelay, Time.deltaTime);
                monoBehaviour.GetController().UpdateNormalMovement(movement, monoBehaviour.GetLocomotionData().GetRunSpeed());
            }

            monoBehaviour.GetController().MoveOnSlope();
        }
    }
}