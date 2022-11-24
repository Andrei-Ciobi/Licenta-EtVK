using System;
using EtVK.Core.Utyles;
using EtVK.Event_Module.Event_Types;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace EtVK.UI_Module.Core
{
    public class BasePanel<TManager> : VisualElement where TManager : BaseUiManager<TManager>, new()
    {
        protected UiManager uiManager;
        protected TManager BaseUiManager => uiManager.GetRootManager<TManager>();
        protected AudioUiData AudioUiData => uiManager.GameUiData.GetUiData<AudioUiData>();
        protected BasePanel()
        {
            uiManager = Object.FindObjectOfType<UiManager>();
            RegisterCallback<GeometryChangedEvent>(OnGeometryChange);
        }

        protected virtual void OnGeometryChange(GeometryChangedEvent evt)
        {
            RegisterCallback<TransitionEndEvent>(ev => BaseUiManager?.ClosePanelEnd(ev, this));
            RegisterCallback<TransitionEndEvent>(ev => BaseUiManager?.OpenPanelEnd(ev, this));
            UnregisterCallback<GeometryChangedEvent>(OnGeometryChange);
        }

        public virtual void Open()
        {
            style.display = DisplayStyle.Flex;
            AddToClassList("opacity-full-trans");
        }

        public void OpenEnd()
        {
            RemoveFromClassList("opacity-none");
            RemoveFromClassList("opacity-full-trans");
        }

        public void OpenInstant()
        {
            style.display = DisplayStyle.Flex;
            RemoveFromClassList("opacity-none");
        }

        public void Close()
        {
            AddToClassList("opacity-none-trans");
        }

        public virtual void CloseEnd()
        {
            AddToClassList("opacity-none");
            RemoveFromClassList("opacity-none-trans");
            style.display = DisplayStyle.None;
        }

        public void CloseInstant()
        {
            AddToClassList("opacity-none");
            style.display = DisplayStyle.None;
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