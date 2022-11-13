using UnityEngine;
using UnityEngine.UIElements;

namespace EtVK.UI_Module.Main_Menu.Panels
{
    public class StartMenuUi : BaseMenuPanel
    {
        private Button backButton;
        
        public new class UxmlFactory : UxmlFactory<StartMenuUi, UxmlTraits> { }
        public new class UxmlTraits : VisualElement.UxmlTraits { }

        protected override void OnGeometryChange(GeometryChangedEvent evt)
        {
            backButton = this.Q<Button>("back-button");

            backButton.RegisterCallback<ClickEvent>(
                ev => MainMenuManager.OpenPanelStart(this, MainMenuManager.MainMenu));
            
            RegisterCallback<TransitionEndEvent>(ev => MainMenuManager.ClosePanelEnd(ev, this));
            RegisterCallback<TransitionEndEvent>(ev => MainMenuManager.OpenPanelEnd(ev, this));

            base.OnGeometryChange(evt);
        }
    }
}