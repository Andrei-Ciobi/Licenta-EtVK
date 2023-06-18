using EtVK.Core.Manager;
using EtVK.Core.Utyles;
using EtVK.Input_Module;
using EtVK.UI_Module.Core;
using EtVK.UI_Module.Hud.Panels;
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

#if UNITY_EDITOR
            if (!Application.isPlaying)
                return;
#endif
            ActivateInputCallbacks();
        }

        public override void OnClose()
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
                return;
#endif
            DeactivateInputCallbacks();
            foreach (var abilityHolderUi in this.Query<BasePanel<HudManager>>().ToList())
            {
                abilityHolderUi.CloseLogic();
            }
        }

        public void ActivateInputCallbacks()
        {
            if (!GameManager.Instance.IsFullGame)
                return;
            InputManager.Instance.UICallbacks.Cancel.performed += EscapeCallback;
        }

        public void DeactivateInputCallbacks()
        {
            if (!GameManager.Instance.IsFullGame)
                return;
            InputManager.Instance.UICallbacks.Cancel.performed -= EscapeCallback;
        }

        private void EscapeCallback(InputAction.CallbackContext context)
        {
            uiManager.OpenGameUi(GameUi.GameMenu);
        }
    }
}