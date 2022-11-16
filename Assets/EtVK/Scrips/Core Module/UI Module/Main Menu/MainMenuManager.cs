using EtVK.UI_Module.Core;
using EtVK.UI_Module.Main_Menu.Panels;
using UnityEngine.UIElements;

namespace EtVK.UI_Module.Main_Menu
{
    public class MainMenuManager : BaseUiManager<MainMenuManager>
    {
        public MainUi Main => main;
        public StartUi Start => start;
        public LoadUi Load => load;

        private MainUi main;
        private StartUi start;
        private LoadUi load;

        public new class UxmlFactory : UxmlFactory<MainMenuManager, UxmlTraits>
        {
        }

        public new class UxmlTraits : VisualElement.UxmlTraits
        {
        }
        

        protected override void OnGeometryChange(GeometryChangedEvent evt)
        {
            main = this.Q<MainUi>("main-menu");
            start = this.Q<StartUi>("start-menu");
            load = this.Q<LoadUi>("load-menu");

            base.OnGeometryChange(evt);
        }
    }
}