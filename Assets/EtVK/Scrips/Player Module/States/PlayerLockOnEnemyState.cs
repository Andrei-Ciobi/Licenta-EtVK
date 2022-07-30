﻿using EtVK.Core_Module;
using EtVK.Input_Module;
using EtVK.Player_Module.Controller;
using EtVK.Utyles;
using UnityEngine;

namespace EtVK.Player_Module.States
{
    public class PlayerLockOnEnemyState : SceneLinkedSMB<PlayerManager>
    {
        public override void OnSLTransitionToStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (InputManager.Instance.LockOnInput)
            {
                if (monoBehaviour.GetLockOnController()?.LockOnEnemy() == true)
                {
                    animator.SetBool(PlayerState.IsLockedOn.ToString(), true);
                }
            }
        }

        public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (InputManager.Instance.LockOnInput)
            {
                if (monoBehaviour.GetLockOnController()?.LockOnEnemy() == true)
                {
                    animator.SetBool(PlayerState.IsLockedOn.ToString(), true);
                }
            }
        }
        
       
    }
}