using EtVK.UI_Module.Core;
using UnityEngine;
using UnityEngine.UIElements;

namespace EtVK.UI_Module.Main_Menu.Panels
{
    public class EnterUi : BasePanel<MainMenuManager>
    {
        private Button enterButton;
        
        public new class UxmlFactory : UxmlFactory<EnterUi, UxmlTraits> { }
        public new class UxmlTraits : VisualElement.UxmlTraits { }

        public EnterUi()
        {
            
        }
        
        protected override void OnGeometryChange(GeometryChangedEvent evt)
        {
            enterButton = this.Q<Button>("enter-button");
            
            RegisterCallback<ClickEvent>(ev =>
                BaseUiManager.OpenPanelStart(this, BaseUiManager.Main));

            base.OnGeometryChange(evt);
        }
    }
}