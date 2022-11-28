using System;
using System.Linq;
using EtVK.Ability_Module.Core;
using EtVK.Event_Module.Event_Types;
using EtVK.Event_Module.Events;
using EtVK.Input_Module;
using UnityEngine;

namespace EtVK.Player_Module.Manager
{
    public class PlayerAbilityManager : AbilityManager
    {
        [SerializeField] private AbilityUiEvent initializeAbilityUiEvent;
        private PlayerManager manager;

        private void Awake()
        {
            BaseInitialize();
            manager = transform.root.GetComponent<PlayerManager>();
        }

        private void Start()
        {
            OnStart();
        }

        private void Update()
        {
            CheckForAbilityInput();
        }

        private void CheckForAbilityInput()
        {
            if (!InputManager.Instance.Player.TapRunInput)
                return;

            if (manager.UninterruptibleAction || manager.IsBLocking)
                return;

            var abilityButtonType = InputManager.Instance.Player.AbilityButtonPressed;
            var ability = abilities.Find(x => x.AbilityButtonType == abilityButtonType);

            if (ability == null)
                return;

            PerformAbility(ability, manager.GetAnimator(), manager.transform,
                () => manager.ErrorUiMessage(manager.ErrorUiData.AbilityOnCd));
        }

        private void OnStart()
        {
            foreach (var ability in abilities)
            {
                if(!ability.DisplayOnUi)
                    continue;
                    
                var abilityRef = abilityReferences.Find(x => x.AbilityType == ability.AbilityType);
                if (abilityRef == null)
                    continue;

                var updateEvent = ScriptableObject.CreateInstance<AbilityUiEvent>();
                abilityRef.SetUpdateEvent(updateEvent);
                initializeAbilityUiEvent.Invoke(new AbilityUiData(ability.AbilityButtonType,
                    null, updateEvent));
            }
        }
    }
}