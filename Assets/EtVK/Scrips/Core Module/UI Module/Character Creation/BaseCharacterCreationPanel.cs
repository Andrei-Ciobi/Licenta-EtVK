using EtVK.UI_Module.Core;
using UnityEngine;
using UnityEngine.UIElements;

namespace EtVK.UI_Module.Character_Creation
{
    public class BaseCharacterCreationPanel : VisualElement
    {
        private UiManager manager;

        protected CharacterCreationManager CharacterCreationManager =>
            manager.GetRootManager<CharacterCreationManager>();

        public BaseCharacterCreationPanel()
        {
            manager = Object.FindObjectOfType<UiManager>();
            RegisterCallback<GeometryChangedEvent>(OnGeometryChange);
        }
        
        protected virtual void OnGeometryChange(GeometryChangedEvent evt)
        {
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