using EtVK.UI_Module.Core;
using UnityEngine.UIElements;

namespace EtVK.UI_Module.Main_Menu.Panels
{
    public class EnterUi : BasePanel<MainMenuManager>
    {
        public new class UxmlFactory : UxmlFactory<EnterUi, UxmlTraits>
        {
        }

        public new class UxmlTraits : BasePanel<MainMenuManager>.UxmlTraits
        {
        }

        protected override void OnGeometryChange(GeometryChangedEvent evt)
        {
            RegisterCallback<ClickEvent>(ev =>
                PlayClickButtonSound(() => BaseUiManager?.OpenPanelStart(this, BaseUiManager.Main)));

            base.OnGeometryChange(evt);
        }
    }
}