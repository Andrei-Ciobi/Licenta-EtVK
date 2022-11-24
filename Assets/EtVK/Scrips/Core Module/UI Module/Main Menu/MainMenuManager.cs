using EtVK.UI_Module.Core;
using EtVK.UI_Module.Main_Menu.Panels;
using UnityEngine;
using UnityEngine.UIElements;

namespace EtVK.UI_Module.Main_Menu
{
    public class MainMenuManager : BaseUiManager<MainMenuManager>
    {
        public EnterUi Enter => this.Q<EnterUi>("enter-menu");
        public MainUi Main => this.Q<MainUi>("main-menu");
        public StartUi Start => this.Q<StartUi>("start-menu");
        public LoadUi Load => this.Q<LoadUi>("load-menu");


        public new class UxmlFactory : UxmlFactory<MainMenuManager, UxmlTraits>
        {
        }

        public new class UxmlTraits : VisualElement.UxmlTraits
        {
        }

        public override void OnOpen()
        {
            Enter?.CloseInstant();
            Main?.OpenInstant();
        }
    }
}