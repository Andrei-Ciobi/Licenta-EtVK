using EtVK.UI_Module.Core;
using UnityEngine;
using UnityEngine.UIElements;

namespace EtVK.UI_Module.Main_Menu.Panels
{
    public class BaseMenuPanel : VisualElement
    {
        protected UiManager manager;
        protected MainMenuManager MainMenuManager => manager.GetMainMenuManager();

        protected BaseMenuPanel()
        {
            manager = Object.FindObjectOfType<UiManager>();
            RegisterCallback<GeometryChangedEvent>(OnGeometryChange);
        }

        protected virtual void OnGeometryChange(GeometryChangedEvent evt)
        {
            UnregisterCallback<GeometryChangedEvent>(OnGeometryChange);
        }

        public void Open()
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