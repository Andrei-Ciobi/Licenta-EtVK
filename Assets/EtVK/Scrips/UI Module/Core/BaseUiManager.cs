using System;
using EtVK.Core.Utyles;
using EtVK.Event_Module.Event_Types;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace EtVK.UI_Module.Core
{
    public class BaseUiManager<T> : VisualElement, IBaseUiManager where T : BaseUiManager<T>, new()
    {
        protected BasePanel<T> selectedPanel;
        protected UiManager uiManager;
        protected AudioUiData AudioUiData => uiManager.GameUiData.GetUiData<AudioUiData>();

        private static bool inTransition;

        protected BaseUiManager()
        {
            uiManager = Object.FindObjectOfType<UiManager>();
            RegisterCallback<GeometryChangedEvent>(OnGeometryChange);
        }
        
        protected virtual void OnGeometryChange(GeometryChangedEvent evt)
        {
            UnregisterCallback<GeometryChangedEvent>(OnGeometryChange);
        }

        public void OpenPanelStart(BasePanel<T> from, BasePanel<T> to)
        {
            if(inTransition)
                return;
            
            if (from == null)
            {
                to.Open();
                selectedPanel = to;
                return;
            }

            inTransition = true;
            from.Close();
            selectedPanel = to;
        }
        
        public void OpenPanelEnd(TransitionEndEvent evt, BasePanel<T> basePanel, string className)
        {
            if (!evt.stylePropertyNames.Contains("opacity"))
                return;

            if (!basePanel.ClassListContains(className))
                return;

            basePanel.OpenEnd();

            selectedPanel = null;
        }
        
        public void ClosePanelEnd(TransitionEndEvent evt, BasePanel<T> basePanel, string className)
        {
            if (!evt.stylePropertyNames.Contains("opacity"))
                return;

            if (!basePanel.ClassListContains(className))
                return;
            inTransition = false;
            basePanel.CloseEnd();
            selectedPanel?.Open();
        }

        public virtual void OnOpen()
        {
        }

        public virtual void OnClose()
        {
        }

        protected TK GetUiData<TK>() where TK : BaseUiData
        {
            return uiManager.GameUiData.GetUiData<TK>();
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