using System;
using EtVK.UI_Module.Core;
using EtVK.Upgrades_Module.Core;
using UnityEngine;
using UnityEngine.UIElements;

namespace EtVK.UI_Module.Hud.Panels
{
    public class UpgradeUi : BasePanel<HudManager>
    {
        public new class UxmlFactory : UxmlFactory<UpgradeUi, UxmlTraits>
        {
        }

        public new class UxmlTraits : BasePanel<HudManager>.UxmlTraits
        {
        }

        public UpgradeUi()
        {
        }
        
        public UpgradeUi(BaseUpgradeData upgradeData, Action<BaseUpgradeData> onClick)
        {
            AddToClassList("upgrade-card");
            var iconContainer = new VisualElement();
            iconContainer.AddToClassList("upgrade-icon");
            if (upgradeData.Icon != null)
            {
                iconContainer.style.backgroundImage = upgradeData.Icon;
                iconContainer.style.backgroundColor = Color.white;
            }
            Add(iconContainer);

            var titleLabel = new Label(){text = upgradeData.Title};
            titleLabel.AddToClassList("title-label-info-small");
            Add(titleLabel);

            var descriptionLabel = new Label() {text = upgradeData.GetDescriptionFormatted()};
            descriptionLabel.AddToClassList("upgrade-description");
            Add(descriptionLabel);
            
            RegisterCallback<ClickEvent>(ev => onClick?.Invoke(upgradeData));
        }
    }
}