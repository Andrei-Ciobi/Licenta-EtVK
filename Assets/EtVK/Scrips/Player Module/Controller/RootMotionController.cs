using System;
using UnityEngine;

namespace EtVK.Scrips.Player_Module.Controller
{
    public class RootMotionController : MonoBehaviour
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
                playerManager.GetController().UpdatePlayerRootMotionRotation(animator);
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