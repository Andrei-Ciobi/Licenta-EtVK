using System;
using EtVK.Core;
using EtVK.Input_Module;
using EtVK.Save_System_Module;
using EtVK.Utyles;
using UnityEngine;

namespace EtVK.UI_Module.Game_UI
{
    public class GameMenuUi : BaseGameUi
    {
        private GameUiManager gameUiManager;

        private void Update()
        {
            if (InputManager.Instance.Ui.TapEscape)
            {
                gameUiManager.CloseGameUi(GameUi.Menu);
            }
        }

        public override void Initialize(GameUiManager manager)
        {
            gameUiManager = manager;
        }

        public override void OnOpen()
        {
            InputManager.Instance.DisablePlayerActionMap();
            InputManager.Instance.EnableUIActionMap();
            PauseGame(false);
        }

        public override void OnClose()
        {
            InputManager.Instance.DisableUIActionMap();
            InputManager.Instance.EnablePlayerActionMap();
            UnpauseGame(false);
        }

        public void PauseGame(bool flag = true)
        {
            if (GameManager.Instance.IsGamePaused)
                return;

            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            GameManager.Instance.IsGamePaused = true;
        }

        public void UnpauseGame(bool flag = true)
        {
            if (!GameManager.Instance.IsGamePaused)
                return;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            GameManager.Instance.IsGamePaused = false;

            if (flag)
            {
                gameUiManager.CloseGameUi(GameUi.Menu);
            }

            Time.timeScale = 1f;
        }


        public void OnMainMenu()
        {
            GameSaveManager.Instance.Save();
            GameManager.Instance.UnloadCurrentScenes();
            GameManager.Instance.LoadScene(SceneNames.MainMenu);
        }

        public void OnExit()
        {
            GameSaveManager.Instance.Save();
            Application.Quit();
        }
    }
}