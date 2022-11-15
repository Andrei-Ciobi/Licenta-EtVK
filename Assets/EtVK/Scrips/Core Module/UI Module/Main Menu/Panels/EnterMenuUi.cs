using UnityEngine;
using UnityEngine.UIElements;

namespace EtVK.UI_Module.Main_Menu.Panels
{
    public class EnterMenuUi : BaseMenuPanel
    {
        private Button enterButton;
        
        public new class UxmlFactory : UxmlFactory<EnterMenuUi, UxmlTraits> { }
        public new class UxmlTraits : VisualElement.UxmlTraits { }
        
        protected override void OnGeometryChange(GeometryChangedEvent evt)
        {
            enterButton = this.Q<Button>("enter-button");
            
            RegisterCallback<ClickEvent>(ev =>
                MainMenuManager.OpenPanelStart(this, MainMenuManager.MainMenu));

            base.OnGeometryChange(evt);
        }
    }
}