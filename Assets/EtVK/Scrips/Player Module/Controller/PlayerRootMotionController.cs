using UnityEngine;

namespace EtVK.Player_Module.Controller
{
    public class PlayerRootMotionController : MonoBehaviour
    {
        private PlayerManager playerManager;
        private Animator animator;
        private Vector3 deltaPosition;

        private void Update()
        {
            if (animator.applyRootMotion)
            {
                playerManager.GetController().Move(deltaPosition);
                deltaPosition = Vector3.zero;
            }
            
            if (playerManager.UseRootMotionRotation)
            {
                playerManager.GetController().UpdateRootMotionRotation(animator);
            }
        }

        public void Initialize(PlayerManager manager)
        {
            playerManager = manager;
            animator = playerManager.GetAnimator();
        }
        
        private void OnAnimatorMove()
        {
            deltaPosition = animator.deltaPosition;
            deltaPosition.y = 0f;
        }
    }
}