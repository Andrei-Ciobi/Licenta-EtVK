using UnityEngine;
using UnityEngine.UIElements;

namespace EtVK.UI_Module.Core
{
    public class BasePanel<TManager> : VisualElement where TManager : BaseUiManager<TManager>, new()
    {
        protected UiManager uiManager;
        protected TManager BaseUiManager => uiManager.GetRootManager<TManager>();
        

        protected BasePanel()
        {
            uiManager = Object.FindObjectOfType<UiManager>();
            RegisterCallback<GeometryChangedEvent>(OnGeometryChange);
        }

        protected virtual void OnGeometryChange(GeometryChangedEvent evt)
        {
            RegisterCallback<TransitionEndEvent>(ev => BaseUiManager.ClosePanelEnd(ev, this));
            RegisterCallback<TransitionEndEvent>(ev => BaseUiManager.OpenPanelEnd(ev, this));
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

        public void Close()
        {
            AddToClassList("opacity-none-trans");
        }

        public void CloseEnd()
        {
            AddToClassList("opacity-none");
            RemoveFromClassList("opacity-none-trans");
            style.display = DisplayStyle.None;
        }
    }
}