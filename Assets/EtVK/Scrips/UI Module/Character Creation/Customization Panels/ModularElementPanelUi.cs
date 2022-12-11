using System.Collections.Generic;
using EtVK.Core.Utyles;
using EtVK.Event_Module.Event_Types;
using EtVK.UI_Module.Core;
using UnityEngine;
using UnityEngine.UIElements;

namespace EtVK.UI_Module.Character_Creation.Customization_Panels
{
    public class ModularElementPanelUi : BasePanel<CharacterCreationManager>
    {
        public ModularOptions bodyPart { get; set; }

        public new class UxmlFactory : UxmlFactory<ModularElementPanelUi, UxmlTraits>
        {
        }

        public new class UxmlTraits : BasePanel<CharacterCreationManager>.UxmlTraits
        {
            private readonly UxmlEnumAttributeDescription<ModularOptions> m_bodyPart = new()
                {name = "body-part", defaultValue = 0};

            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);
                var ate = ve as ModularElementPanelUi;
                ate.bodyPart = m_bodyPart.GetValueFromBag(bag, cc);
            }

            public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
            {
                get { yield break; }
            }
        }

        protected override void OnGeometryChange(GeometryChangedEvent evt)
        {
            var upArrow = this.Q<VisualElement>("up-arrow");
            var downArrow = this.Q<VisualElement>("down-arrow");
            var unsetButton = this.Q<Button>("unset");

            // Click events
            upArrow?.RegisterCallback<PointerDownEvent>(ev => OnClickDownIcon(upArrow, Vector3.down, 10f));
            upArrow?.RegisterCallback<PointerUpEvent>(ev => OnClickUpIcon(upArrow));
            upArrow?.RegisterCallback<ClickEvent>(ev => PlayClickButtonSound(OnNext));

            downArrow?.RegisterCallback<PointerDownEvent>(ev => OnClickDownIcon(downArrow, Vector3.up, 10f));
            downArrow?.RegisterCallback<PointerUpEvent>(ev => OnClickUpIcon(downArrow));
            downArrow?.RegisterCallback<ClickEvent>(ev => PlayClickButtonSound(OnPrevious));

            unsetButton?.RegisterCallback<ClickEvent>(ev => PlayClickButtonSound(OnUnset));
            
            // Hover events
            upArrow?.RegisterCallback<MouseOverEvent>(ev => PlayHoverButtonSound());
            downArrow?.RegisterCallback<MouseOverEvent>(ev => PlayHoverButtonSound());
            unsetButton?.RegisterCallback<MouseOverEvent>(ev => PlayHoverButtonSound());

            base.OnGeometryChange(evt);
        }

        private void OnClickDownIcon(VisualElement arrow, Vector3 dir, float value)
        {
            arrow.style.translate = new Translate(dir.x * value, dir.y * value, dir.z);
        }

        private void OnClickUpIcon(VisualElement arrow)
        {
            arrow.style.translate = new Translate(0, 0, 0);
        }

        private void OnNext()
        {
            var uiData = uiManager.GameUiData.GetUiData<CharacterCreationUiData>();
            if (uiData == null)
                return;
            
            uiData.OnNext.Invoke(new CharacterCreationData(bodyPart));
        }

        private void OnPrevious()
        {
            var uiData = uiManager.GameUiData.GetUiData<CharacterCreationUiData>();
            if (uiData == null)
                return;

            uiData.OnPrevious.Invoke(new CharacterCreationData(bodyPart));
        }

        private void OnUnset()
        {
            var uiData = uiManager.GameUiData.GetUiData<CharacterCreationUiData>();
            if (uiData == null)
                return;

            uiData.OnUnset.Invoke(new CharacterCreationData(bodyPart));
        }
    }
}