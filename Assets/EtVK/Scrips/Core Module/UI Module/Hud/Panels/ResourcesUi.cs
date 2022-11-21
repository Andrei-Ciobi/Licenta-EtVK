using EtVK.Event_Module.Custom_Events;
using EtVK.Event_Module.Listeners;
using EtVK.UI_Module.Components;
using EtVK.UI_Module.Core;
using UnityEngine.UIElements;

namespace EtVK.UI_Module.Hud.Panels
{
    public class ResourcesUi : BasePanel<HudManager>
    {
        private FloatEventListener staminaEvent;
        private FloatEventListener healthEvent;
        public new class UxmlFactory : UxmlFactory<ResourcesUi, UxmlTraits>
        {
        }

        public new class UxmlTraits : VisualElement.UxmlTraits
        {
        }

        protected override void OnGeometryChange(GeometryChangedEvent evt)
        {
            var stamina = this.Q<ResourceBarUiComponent>("stamina");
            var health = this.Q<ResourceBarUiComponent>("health");
            var uiData = uiManager.GameUiData.GetUiData<HudUiData>();

            if (staminaEvent == null)
            {
                staminaEvent = new FloatEventListener(uiData.StaminaEvent, new CustomFloatEvent());
                staminaEvent.AddCallback(stamina.updateValue);
            }

            if (healthEvent == null)
            {
                healthEvent = new FloatEventListener(uiData.HealthEvent, new CustomFloatEvent());
                healthEvent.AddCallback(health.updateValue);
            }
            base.OnGeometryChange(evt);
        }
    }
}