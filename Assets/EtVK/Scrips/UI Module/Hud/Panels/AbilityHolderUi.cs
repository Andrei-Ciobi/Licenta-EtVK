using System.Collections.Generic;
using EtVK.Core.Utyles;
using EtVK.Event_Module.Event_Types;
using EtVK.Event_Module.Events;
using EtVK.Event_Module.Listeners;
using EtVK.UI_Module.Core;
using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable InconsistentNaming

namespace EtVK.UI_Module.Hud.Panels
{
    public class AbilityHolderUi : BasePanel<HudManager>
    {
        public AbilityButtonType buttonType { get; set; }

        private Label abilityCdLabel;
        private VisualElement abilityCdOverlay;

        private AbilityUiEventListener updateEventListener;

        public new class UxmlFactory : UxmlFactory<AbilityHolderUi, UxmlTraits>
        {
        }

        public new class UxmlTraits : BasePanel<HudManager>.UxmlTraits
        {
            private readonly UxmlEnumAttributeDescription<AbilityButtonType> m_buttonType = new()
                {name = "button-type", defaultValue = 0};

            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);
                var ate = ve as AbilityHolderUi;

                ate.buttonType = m_buttonType.GetValueFromBag(bag, cc);
                ate.AddToClassList("ability");
                ate.Clear();

                var abilityOverlay = new VisualElement();
                abilityOverlay.AddToClassList("ability-overlay");
                ate.Add(abilityOverlay);
                ate.abilityCdOverlay = abilityOverlay;

                var abilityCdContainer = new VisualElement();
                abilityCdContainer.AddToClassList("ability-cd-container");
                var abilityCdLabel = new Label() {text = "0"};
                abilityCdLabel.style.display = DisplayStyle.None;
                abilityCdContainer.Add(abilityCdLabel);
                ate.Add(abilityCdContainer);
                ate.abilityCdLabel = abilityCdLabel;

                var buttonNameLabelText = ate.buttonType == AbilityButtonType.None ? "" : ate.buttonType.ToString();
                var buttonNameLabel = new Label() {text = buttonNameLabelText};
                buttonNameLabel.AddToClassList("ability-label");
                ate.Add(buttonNameLabel);
            }

            public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
            {
                get { yield break; }
            }
        }


        public void Initialize(AbilityUiEvent updateEvent, Texture2D abilityIcon = null)
        {
            updateEventListener ??= new AbilityUiEventListener(updateEvent);

            updateEventListener.RemoveCallbacks();
            updateEventListener.AddCallback(UpdateAbilityCd);

            if (abilityIcon == null)
                return;

            style.backgroundColor = Color.white;
            style.backgroundImage = abilityIcon;
        }

        private void UpdateAbilityCd(AbilityUiData abilityUiData)
        {
            abilityCdLabel.style.display = abilityUiData.CdPercentage switch
            {
                1f => DisplayStyle.Flex,
                0f => DisplayStyle.None,
                _ => abilityCdLabel.style.display
            };

            abilityCdLabel.text = abilityUiData.CdTime.ToString();
            abilityCdOverlay.style.scale = new Scale(new Vector3(1, abilityUiData.CdPercentage, 0));
        }
    }
}