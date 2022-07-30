using System.Collections.Generic;
using EtVK.Core_Module;
using EtVK.Event_Module.Event_Types;
using EtVK.Event_Module.Events;
using EtVK.Health_Module;
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
                monoBehaviour.GetLockOnController()?.LockOnEnemy();
            }
        }

        public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (InputManager.Instance.LockOnInput)
            {
                monoBehaviour.GetLockOnController()?.LockOnEnemy();
            }
        }
        
       
    }
}