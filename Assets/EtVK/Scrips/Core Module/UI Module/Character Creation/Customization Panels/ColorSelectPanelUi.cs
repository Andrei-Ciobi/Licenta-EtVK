using EtVK.Core.Utyles;
using EtVK.Event_Module.Event_Types;
using EtVK.UI_Module.Components;
using EtVK.UI_Module.Core;
using UnityEngine;
using UnityEngine.UIElements;

namespace EtVK.UI_Module.Character_Creation.Customization_Panels
{
    public class ColorSelectPanelUi : BasePanel<CharacterCreationManager>
    {
        private ColorPopupComponent currentPopupOpen;

        public new class UxmlFactory : UxmlFactory<ColorSelectPanelUi, UxmlTraits>
        {
        }

        public new class UxmlTraits : VisualElement.UxmlTraits
        {
        }

        protected override void OnGeometryChange(GeometryChangedEvent evt)
        {
            this.Query<ModularColorUi>("modular-color")
                .ForEach(modularColor =>
                {
                    modularColor.onColorChange += UpdateColor;
                    modularColor.colorField.onOpen += OnOpen;
                });

            base.OnGeometryChange(evt);
        }

        private void UpdateColor(ModularColorOptions colorOptions, Color color)
        {
            var uiData = uiManager.GameUiData.GetUiData<CharacterCreationUiData>();
            if (uiData == null)
                return;

            uiData.OnColorChange.Invoke(
                new CharacterCreationData(ModularOptions.Hair, color, colorOptions));
        }

        private void OnOpen(ColorPopupComponent popupToOpen)
        {
            if (currentPopupOpen != null && currentPopupOpen != popupToOpen && currentPopupOpen.isOpen)
            {
                currentPopupOpen.Hide();
            }

            currentPopupOpen = popupToOpen;
        }
    }
}