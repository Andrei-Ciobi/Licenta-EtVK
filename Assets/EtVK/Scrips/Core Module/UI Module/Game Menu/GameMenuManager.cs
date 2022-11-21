using EtVK.Core.Utyles;
using EtVK.Input_Module;
using EtVK.UI_Module.Core;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

namespace EtVK.UI_Module.Game_Menu
{
    public class GameMenuManager: BaseUiManager<GameMenuManager>
    {
        public new class UxmlFactory : UxmlFactory<GameMenuManager, UxmlTraits>
        {
        }

        public new class UxmlTraits : VisualElement.UxmlTraits
        {
        }

        public override void OnOpen()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            InputManager.Instance.UICallbacks.Cancel.performed += EscapeCallback;
        }

        public override void OnClose()
        {
            InputManager.Instance.UICallbacks.Cancel.performed -= EscapeCallback;
        }
        
        private void EscapeCallback(InputAction.CallbackContext context)
        {
            uiManager.OpenGameUi(GameUi.Hud);
        }
    }
}