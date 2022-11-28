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

        public new class UxmlTraits : BasePanel<HudManager>.UxmlTraits
        {
        }

        protected override void OnGeometryChange(GeometryChangedEvent evt)
        {
            var stamina = this.Q<ResourceBarUiComponent>("stamina");
            var health = this.Q<ResourceBarUiComponent>("health");
            var uiData = GetUiData<HudUiData>();

            if (staminaEvent == null)
            {
                staminaEvent = new FloatEventListener(uiData.StaminaEvent);
                staminaEvent.AddCallback(stamina.updateValue);
            }

            if (healthEvent == null)
            {
                healthEvent = new FloatEventListener(uiData.HealthEvent);
                healthEvent.AddCallback(health.updateValue);
            }
            base.OnGeometryChange(evt);
        }
    }
}