using UnityEngine;
using UnityEngine.UIElements;

namespace EtVK.UI_Module.Core
{
    public class BaseUiManager<T> : VisualElement, IBaseUiManager where T : BaseUiManager<T>, new()
    {
        protected static BasePanel<T> selectedPanel;
        protected UiManager uiManager;

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
            if (from == null)
            {
                to.Open();
                selectedPanel = to;
                return;
            }
            
            from.Close();
            selectedPanel = to;
        }
        
        public void OpenPanelEnd(TransitionEndEvent evt, BasePanel<T> basePanel)
        {
            if (selectedPanel == null)
                return;

            if (!evt.stylePropertyNames.Contains("opacity"))
                return;

            if (!basePanel.ClassListContains("opacity-full-trans"))
                return;

            basePanel.OpenEnd();

            selectedPanel = null;
        }
        
        public void ClosePanelEnd(TransitionEndEvent evt, BasePanel<T> basePanel)
        {
            if (selectedPanel == null)
                return;

            if (!evt.stylePropertyNames.Contains("opacity"))
                return;

            if (!basePanel.ClassListContains("opacity-none-trans"))
                return;

            basePanel.CloseEnd();
            selectedPanel.Open();
        }

        public virtual void OnOpen()
        {
        }

        public virtual void OnClose()
        {
        }
    }
}