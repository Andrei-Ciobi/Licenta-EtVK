﻿using EtVK.Core_Module;
using EtVK.Player_Module.Controller;
using UnityEngine;

namespace EtVK.Player_Module.States
{
    public class PlayerApplyGravityState : SceneLinkedSMB<PlayerManager>
    {

        public override void OnSLTransitionToStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            monoBehaviour.GetController().UpdateGravity();
        }

        public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            monoBehaviour.GetController().UpdateGravity();
        }
    }
}