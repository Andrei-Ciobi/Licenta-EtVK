using EtVK.Core.Manager;
using EtVK.Core.Utyles;
using EtVK.Input_Module;
using EtVK.UI_Module.Core;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

namespace EtVK.UI_Module.Hud
{
    public class HudManager : BaseUiManager<HudManager>
    {
        public new class UxmlFactory : UxmlFactory<HudManager, UxmlTraits>
        {
        }

        public new class UxmlTraits : VisualElement.UxmlTraits
        {
        }


        public override void OnOpen()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            
            if(!GameManager.Instance.IsFullGame)
                return;
            InputManager.Instance.UICallbacks.Cancel.performed += EscapeCallback;
        }

        public override void OnClose()
        {
            if(!GameManager.Instance.IsFullGame)
                return;
            InputManager.Instance.UICallbacks.Cancel.performed -= EscapeCallback;
        }

        private void EscapeCallback(InputAction.CallbackContext context)
        {
            uiManager.OpenGameUi(GameUi.GameMenu);
        }
    }
}