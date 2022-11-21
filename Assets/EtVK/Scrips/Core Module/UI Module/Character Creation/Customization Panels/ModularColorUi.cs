using System.Collections.Generic;
using EtVK.Core.Utyles;
using EtVK.UI_Module.Components;
using EtVK.UI_Module.Core;
using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable InconsistentNaming

namespace EtVK.UI_Module.Character_Creation.Customization_Panels
{
    public class ModularColorUi : BasePanel<CharacterCreationManager>
    {
        public string colorLabel { get; set; }
        public ModularColorOptions colorOption { get; set; }

        public System.Action<ModularColorOptions, Color> onColorChange;
        public ColorFieldComponent colorField;

        private ColorPopupComponent colorPopup;
        private bool popupSet;

        public new class UxmlFactory : UxmlFactory<ModularColorUi, UxmlTraits>
        {
        }

        public new class UxmlTraits : VisualElement.UxmlTraits
        {
            private readonly UxmlStringAttributeDescription m_colorLabel = new()
                {name = "color-label", defaultValue = null};
            private readonly UxmlEnumAttributeDescription<ModularColorOptions> m_colorOption = new()
                {name = "color-option", defaultValue = 0};

            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);
                var ate = ve as ModularColorUi;
                ate.colorOption = m_colorOption.GetValueFromBag(bag, cc);
                ate.colorLabel = m_colorLabel.GetValueFromBag(bag, cc);

                ate.Clear();
                ate.AddToClassList("modular-color-puck");
                
                var colorFiled = new ColorFieldComponent(ate.colorLabel);
                colorFiled.AddToClassList("customization-color-field");
                ate.colorField = colorFiled;
                ate.Add(colorFiled);

                var colorPopup = new ColorPopupComponent();
                colorPopup.AddToClassList("customization-color-popup");
                ate.colorPopup = colorPopup;

                colorFiled.ColorPopup = colorPopup;
                colorPopup.onColorChange += color => ate.onColorChange?.Invoke(ate.colorOption, color);

                var uiData = ate.uiManager.GameUiData.GetUiData<CharacterCreationUiData>();
                if(uiData == null)
                    return;
                colorFiled.SetDefaultColor(uiData.GetBaseColor(ate.colorOption));
            }

            public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
            {
                get { yield break; }
            }
        }
        
        protected override void OnGeometryChange(GeometryChangedEvent evt)
        {
            if (parent != null && colorPopup != null && popupSet == false)
            {
                parent.parent?.Q("popup-container")?.Add(colorPopup);
                popupSet = true;
            }

            base.OnGeometryChange(evt);
        }

    }
}