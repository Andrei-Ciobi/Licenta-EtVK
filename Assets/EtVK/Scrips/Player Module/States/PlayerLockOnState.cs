using EtVK.Core;
using EtVK.Core.Utyles;
using EtVK.Input_Module;
using EtVK.Player_Module.Manager;
using UnityEngine;

namespace EtVK.Player_Module.States
{
    public class PlayerLockOnState : SceneLinkedSMB<PlayerManager>
    {
        [Header("Set true = lock on enemy, false = unlock from enemy")]
        [SerializeField] private bool forLock = true;
        public override void OnSLTransitionToStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (forLock)
            {
                LockOnEnemy(animator);
            }
            else
            {
                UnlockFromEnemy(animator);
            }
        }

        public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (forLock)
            {
                LockOnEnemy(animator);
            }
            else
            {
                UnlockFromEnemy(animator);
            }
        }

        private void LockOnEnemy(Animator animator)
        {
            if (!InputManager.Instance.Player.ActivateLockOn) 
                return;
            
            var success = false;
            monoBehaviour.GetLockOnController()?.LockOnEnemy(ref success);
            if (success)
            {
                animator.SetBool(PlayerState.IsLockedOn.ToString(), true);
            }
        }

        private void UnlockFromEnemy(Animator animator)
        {
            if (!InputManager.Instance.Player.DeactivateLockOn) 
                return;
            
            monoBehaviour.GetLockOnController().UnlockFromEnemy();
        }
        
       
    }
}