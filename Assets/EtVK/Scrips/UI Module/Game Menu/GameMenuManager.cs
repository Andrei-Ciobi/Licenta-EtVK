using EtVK.Core.Manager;
using EtVK.Core.Utyles;
using EtVK.Input_Module;
using EtVK.UI_Module.Core;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

namespace EtVK.UI_Module.Game_Menu
{
    public class GameMenuManager : BaseUiManager<GameMenuManager>
    {
        public new class UxmlFactory : UxmlFactory<GameMenuManager, UxmlTraits>
        {
        }

        public new class UxmlTraits : VisualElement.UxmlTraits
        {
        }

        protected override void OnGeometryChange(GeometryChangedEvent evt)
        {
            var unpauseButton = this.Q("main-panel").Q<Button>("unpause");
            var optionsButton = this.Q("main-panel").Q<Button>("options");
            var mainMenuButton = this.Q("main-panel").Q<Button>("main-menu");
            var exitButton = this.Q("main-panel").Q<Button>("exit");

            // Click events
            unpauseButton?.RegisterCallback<ClickEvent>(ev => PlayClickButtonSound(OnUnpause));
            optionsButton?.RegisterCallback<ClickEvent>(ev => PlayClickButtonSound());
            mainMenuButton?.RegisterCallback<ClickEvent>(ev => PlayClickButtonSound(OnMainMenu));
            exitButton?.RegisterCallback<ClickEvent>(ev => PlayClickButtonSound(OnExit));
            
            // Hover events
            unpauseButton?.RegisterCallback<MouseOverEvent>(ev => PlayHoverButtonSound());
            optionsButton?.RegisterCallback<MouseOverEvent>(ev => PlayHoverButtonSound());
            mainMenuButton?.RegisterCallback<MouseOverEvent>(ev => PlayHoverButtonSound());
            exitButton?.RegisterCallback<MouseOverEvent>(ev => PlayHoverButtonSound());
            
            base.OnGeometryChange(evt);
        }

        public override void OnOpen()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
#if UNITY_EDITOR
            if (!Application.isPlaying)
                return;
#endif
            GameManager.Instance.IsGamePaused = true;
            InputManager.Instance.DisablePlayerActionMap();
            InputManager.Instance.UICallbacks.Cancel.performed += EscapeCallback;
            GetUiData<GameMenuUiData>()?.CameraInputEvent.Invoke(false);
        }

        public override void OnClose()
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
                return;
#endif
            GameManager.Instance.IsGamePaused = false;
            InputManager.Instance.EnablePlayerActionMap();
            InputManager.Instance.UICallbacks.Cancel.performed -= EscapeCallback;
            GetUiData<GameMenuUiData>()?.CameraInputEvent.Invoke(true);
        }

        private void EscapeCallback(InputAction.CallbackContext context)
        {
            uiManager.OpenGameUi(GameUi.Hud);
        }

        private void OnUnpause()
        {
            uiManager.OpenGameUi(GameUi.Hud);
        }

        private void OnMainMenu()
        {
            if (!Application.isPlaying)
                return;

            GameManager.Instance.StartLoadingScreen(GameUi.MainMenu);
            GameManager.Instance.UnloadCurrentScenes();
        }

        private void OnExit()
        {
#if UNITY_EDITOR
            if(!Application.isPlaying)
                return;
#endif
            
            Application.Quit();
        }
    }
}