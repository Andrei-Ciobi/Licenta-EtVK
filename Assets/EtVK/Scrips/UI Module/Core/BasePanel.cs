using System;
using System.Collections.Generic;
using EtVK.Core.Utyles;
using EtVK.Event_Module.Event_Types;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

// ReSharper disable InconsistentNaming

namespace EtVK.UI_Module.Core
{
    public class BasePanel<TManager> : VisualElement where TManager : BaseUiManager<TManager>, new()
    {
        public string openTransition { get; set; }
        public string closeTransition { get; set; }
        protected UiManager uiManager;
        protected TManager BaseUiManager => uiManager.GetRootManager<TManager>();
        protected AudioUiData AudioUiData => uiManager.GameUiData.GetUiData<AudioUiData>();
        

        public new class UxmlTraits : VisualElement.UxmlTraits
        {
            private readonly UxmlStringAttributeDescription m_openTransition =
                new() {name = "open-transition", defaultValue = "opacity-full-trans"};

            private readonly UxmlStringAttributeDescription m_closeTransition =
                new() {name = "close-transition", defaultValue = "opacity-none-trans"};

            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);
                var ate = ve as BasePanel<TManager>;

                ate.openTransition = m_openTransition.GetValueFromBag(bag, cc);
                ate.closeTransition = m_closeTransition.GetValueFromBag(bag, cc);
            }

            public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
            {
                get { yield break; }
            }
        }

        protected BasePanel()
        {
            uiManager = Object.FindObjectOfType<UiManager>();
            RegisterCallback<GeometryChangedEvent>(OnGeometryChange);
        }

        protected virtual void OnGeometryChange(GeometryChangedEvent evt)
        {
            RegisterCallback<TransitionEndEvent>(ev => BaseUiManager?.ClosePanelEnd(ev, this, closeTransition));
            RegisterCallback<TransitionEndEvent>(ev => BaseUiManager?.OpenPanelEnd(ev, this, openTransition));
            UnregisterCallback<GeometryChangedEvent>(OnGeometryChange);
        }

        public virtual void Open()
        {
            style.display = DisplayStyle.Flex;
            AddToClassList(openTransition);
        }

        public void OpenEnd()
        {
            RemoveFromClassList("opacity-none");
            RemoveFromClassList(openTransition);
        }

        public void OpenInstant()
        {
            style.display = DisplayStyle.Flex;
            RemoveFromClassList("opacity-none");
        }

        public void Close()
        {
            AddToClassList(closeTransition);
        }

        public virtual void CloseEnd()
        {
            AddToClassList("opacity-none");
            RemoveFromClassList(closeTransition);
            style.display = DisplayStyle.None;
        }

        public virtual void CloseInstant()
        {
            AddToClassList("opacity-none");
            style.display = DisplayStyle.None;
        }

        public virtual void CloseLogic()
        {
            
        }

        protected T GetUiData<T>() where T : BaseUiData
        {
            return uiManager.GameUiData.GetUiData<T>();
        }

        protected void PlayClickButtonSound(Action action = null)
        {
            AudioUiData.PlayAudioEvent.Invoke(new AudioData(AudioSourceType.Ui, AudioUiData.ButtonClickSound));
            action?.Invoke();
        }

        protected void PlayHoverButtonSound(Action action = null)
        {
            AudioUiData.PlayAudioEvent.Invoke(new AudioData(AudioSourceType.Ui, AudioUiData.ButtonHoverSound));
            action?.Invoke();
        }
    }
}