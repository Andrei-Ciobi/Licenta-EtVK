using System;
using UnityEngine;

namespace EtVK.Scrips.Player_Module.Controller
{
    public class RootMotionController : MonoBehaviour
    {
        private PlayerManager playerManager;
        private Vector3 deltaPosition;

        private void Update()
        {
            if (playerManager.GetAnimator().applyRootMotion)
            {
                playerManager.GetController().Move(deltaPosition);
                deltaPosition = Vector3.zero;
            }
        }

        public void Initialize(PlayerManager manager)
        {
            playerManager = manager;
        }
        
        private void OnAnimatorMove()
        {
            deltaPosition = playerManager.GetAnimator().deltaPosition;
            deltaPosition.y = 0f;
        }
    }
}