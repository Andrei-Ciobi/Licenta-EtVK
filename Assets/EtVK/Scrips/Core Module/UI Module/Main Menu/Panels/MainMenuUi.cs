﻿using EtVK.Save_System_Module;
using UnityEngine;
using UnityEngine.UIElements;

namespace EtVK.UI_Module.Main_Menu.Panels
{
    public class MainMenuUi : BaseMenuPanel
    {
        private Button continueGameButton;
        private Button startGameButton;
        private Button loadGameButton;
        private Button optionsButton;
        private Button exitButton;


        public new class UxmlFactory : UxmlFactory<MainMenuUi, UxmlTraits>
        {
        }

        public new class UxmlTraits : VisualElement.UxmlTraits
        {
        }

        protected override void OnGeometryChange(GeometryChangedEvent evt)
        {
            startGameButton = this.Q<Button>("start-game");
            loadGameButton = this.Q<Button>("load-game");
            optionsButton = this.Q<Button>("options");
            exitButton = this.Q<Button>("exit");

            startGameButton?.RegisterCallback<ClickEvent>(ev =>
                MainMenuManager.OpenPanelStart(this, MainMenuManager.StartMenu));
            loadGameButton?.RegisterCallback<ClickEvent>(ev =>
                MainMenuManager.OpenPanelStart(this, MainMenuManager.LoadMenu));


            exitButton?.RegisterCallback<ClickEvent>(ev => ExitGame());


            base.OnGeometryChange(evt);
        }

        public override void Open()
        {
            if (GameSaveManager.Instance != null)
            {
                if (!GameSaveManager.Instance.HasSaveFiles())
                {
                    this.Q<Button>("load-game").style.display = DisplayStyle.None;
                    this.Q<Button>("continue-game").style.display = DisplayStyle.None;
                }
            }

            base.Open();
        }

        private void ExitGame()
        {
#if UNITY_EDITOR
            Debug.Log("Quit game");
            return;
#endif
            if (Application.isPlaying)
                Application.Quit();
        }
    }
}