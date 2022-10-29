using System;
using EtVK.Ability_Module.Core;
using EtVK.Input_Module;
using UnityEngine;

namespace EtVK.Player_Module.Manager
{
    public class PlayerAbilityManager : AbilityManager
    {
        private PlayerManager manager;

        private void Awake()
        {
            BaseInitialize();
            manager = transform.root.GetComponent<PlayerManager>();
        }

        private void Update()
        {
            CheckForAbilityInput();
        }

        private void CheckForAbilityInput()
        {
            if(!InputManager.Instance.Player.TapRunInput)
                return;
            
            if(manager.UninterruptibleAction)
                return;

            var abilityType = InputManager.Instance.Player.AbilityPressed;
            var ability = abilities.Find(x => x.AbilityType == abilityType);
            
            if(ability == null)
                return;

            PerformAbility(ability, manager.GetAnimator(), manager.transform);
        }
    }
}