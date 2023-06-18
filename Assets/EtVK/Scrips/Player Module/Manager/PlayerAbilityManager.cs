using System;
using System.Linq;
using EtVK.Ability_Module.Core;
using EtVK.Core.Manager;
using EtVK.Event_Module.Event_Types;
using EtVK.Event_Module.Events;
using EtVK.Event_Module.Listeners;
using EtVK.Input_Module;
using UnityEngine;
using Void = EtVK.Event_Module.Event_Types.Void;

namespace EtVK.Player_Module.Manager
{
    public class PlayerAbilityManager : AbilityManager
    {
        [SerializeField] private AbilityUiEvent initializeAbilityUiEvent;
        [SerializeField] private VoidEvent startInitAbilityUiEvent;
        private PlayerManager manager;

        private VoidEventListener startInitAbilityUiEventListener;

        private void Awake()
        {
            BaseInitialize();
            manager = transform.root.GetComponent<PlayerManager>();
        }

        private void Start()
        {
            if (GameManager.Instance?.IsFullGame ?? false)
            {
                startInitAbilityUiEventListener ??= new VoidEventListener(startInitAbilityUiEvent);
                startInitAbilityUiEventListener.RemoveCallbacks();
                startInitAbilityUiEventListener.AddCallback(Initialize);
            }
            else
            {
                Initialize(new Void());
            }
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

        private void Initialize(Void arg)
        {
            foreach (var ability in abilities)
            {
                if (!ability.DisplayOnUi)
                    continue;

                var abilityRef = abilityReferences.Find(x => x.AbilityType == ability.AbilityType);
                if (abilityRef == null)
                    continue;

                var updateEvent = abilityRef.SetUpdateEvent();
                initializeAbilityUiEvent.Invoke(new AbilityUiData(ability.AbilityButtonType,
                    null, updateEvent));
            }
        }

        private void OnDestroy()
        {
            if (GameManager.Instance.IsFullGame)
                startInitAbilityUiEventListener.RemoveCallbacks();
        }
    }
}