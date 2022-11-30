using EtVK.Event_Module.Listeners;
using EtVK.UI_Module.Core;
using UnityEngine;
using UnityEngine.UIElements;

namespace EtVK.UI_Module.Hud.Panels
{
    public class ErrorUi : BasePanel<HudManager>
    {
        private StringEventListener errorEventListener;
        private IVisualElementScheduledItem activeScheduler;

        public new class UxmlFactory : UxmlFactory<ErrorUi, UxmlTraits>
        {
        }

        public new class UxmlTraits : BasePanel<HudManager>.UxmlTraits
        {
        }

        public ErrorUi()
        {
            AddToClassList("interact-container");
        }

        protected override void OnGeometryChange(GeometryChangedEvent evt)
        {
            if (errorEventListener == null)
            {
                errorEventListener = new StringEventListener(GetUiData<HudUiData>().ErrorEvent);
                errorEventListener.AddCallback(DisplayError);
                if (Application.isPlaying)
                {
                    CloseInstant();
                }
            }

            base.OnGeometryChange(evt);
        }

        private void DisplayError(string message)
        {
            this.Q<Label>().text = message;
            OpenInstant();

            style.opacity = 1f;
            if (activeScheduler != null)
            {
                activeScheduler.Pause();
                activeScheduler = null;
            }

            activeScheduler = schedule.Execute(CloseFade)
                .StartingIn(1000)
                .Every(100)
                .Until(() => style.opacity.value <= 0f);
        }

        private void CloseFade()
        {
            style.opacity = style.opacity.value - .05f;
            if (style.opacity.value > 0f)
                return;

            CloseInstant();
            activeScheduler = null;
        }
    }
}