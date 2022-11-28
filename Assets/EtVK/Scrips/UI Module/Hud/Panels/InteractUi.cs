using EtVK.Event_Module.Event_Types;
using EtVK.Event_Module.Listeners;
using EtVK.UI_Module.Core;
using UnityEngine;
using UnityEngine.UIElements;

namespace EtVK.UI_Module.Hud.Panels
{
    public class InteractUi : BasePanel<HudManager>
    {
        private InteractUiEventListener interactEventListener;
        private bool isDisplayed;

        public new class UxmlFactory : UxmlFactory<InteractUi, UxmlTraits>
        {
        }

        public new class UxmlTraits : BasePanel<HudManager>.UxmlTraits
        {
        }

        public InteractUi()
        {
            AddToClassList("interact-container");
        }

        protected override void OnGeometryChange(GeometryChangedEvent evt)
        {
            if (interactEventListener == null)
            {
                interactEventListener = new InteractUiEventListener(GetUiData<HudUiData>().InteractEvent);
                interactEventListener.AddCallback(DisplayContent);
                if (Application.isPlaying)
                {
                    CloseInstant();
                }
            }

            base.OnGeometryChange(evt);
        }

        private void DisplayContent(InteractUiData data)
        {
            if (!data.Display && !data.UpdateOnly)
            {
                CloseInstant();
                isDisplayed = false;
                return;
            }

            if (data.PressLabel != null)
            {
                this.Q<Label>("press-label").text = data.PressLabel;
            }
            
            if (data.InteractLabel != null)
            {
                this.Q<Label>("interact-label").text = data.InteractLabel;
            }

            if (isDisplayed || data.UpdateOnly)
                return;

            OpenInstant();
            isDisplayed = true;
        }
    }
}