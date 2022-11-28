using EtVK.Core.Manager;
using EtVK.Core.Utyles;
using EtVK.Save_System_Module;
using EtVK.UI_Module.Core;
using UnityEngine;
using UnityEngine.UIElements;

namespace EtVK.UI_Module.Main_Menu.Panels
{
    public class MainUi : BasePanel<MainMenuManager>
    {
        private Button continueGameButton;
        private Button startGameButton;
        private Button loadGameButton;
        private Button optionsButton;
        private Button exitButton;


        public new class UxmlFactory : UxmlFactory<MainUi, UxmlTraits>
        {
        }

        public new class UxmlTraits :  BasePanel<MainMenuManager>.UxmlTraits
        {
        }

        protected override void OnGeometryChange(GeometryChangedEvent evt)
        {
            continueGameButton = this.Q<Button>("continue-game");
            startGameButton = this.Q<Button>("start-game");
            loadGameButton = this.Q<Button>("load-game");
            optionsButton = this.Q<Button>("options");
            exitButton = this.Q<Button>("exit");

            startGameButton?.RegisterCallback<ClickEvent>(ev =>
                PlayClickButtonSound(() => BaseUiManager.OpenPanelStart(this, BaseUiManager.Start)));
            loadGameButton?.RegisterCallback<ClickEvent>(ev =>
                PlayClickButtonSound(() => BaseUiManager.OpenPanelStart(this, BaseUiManager.Load)));
            optionsButton?.RegisterCallback<ClickEvent>(ev => PlayClickButtonSound());
            continueGameButton?.RegisterCallback<ClickEvent>(ev =>  PlayClickButtonSound(LoadLastSaveFile));
            exitButton?.RegisterCallback<ClickEvent>(ev =>  PlayClickButtonSound(ExitGame));
            
            startGameButton?.RegisterCallback<MouseOverEvent>(ev => PlayHoverButtonSound());
            loadGameButton?.RegisterCallback<MouseOverEvent>(ev => PlayHoverButtonSound());
            optionsButton?.RegisterCallback<MouseOverEvent>(ev => PlayHoverButtonSound());
            continueGameButton?.RegisterCallback<MouseOverEvent>(ev => PlayHoverButtonSound());
            exitButton?.RegisterCallback<MouseOverEvent>(ev => PlayHoverButtonSound());

            base.OnGeometryChange(evt);
        }

        public override void Open()
        {
            if (!GameSaveManager.Instance.HasSaveFiles())
            {
                this.Q<Button>("load-game").style.display = DisplayStyle.None;
                this.Q<Button>("continue-game").style.display = DisplayStyle.None;
            }

            base.Open();
        }

        private void LoadLastSaveFile()
        {
            GameManager.Instance.StartLoadingScreen(GameUi.Hud);
            var lastSaveData = GameSaveManager.Instance.GetLastSavedFileData();
            GameSaveManager.Instance.LoadSaveSlot(lastSaveData.SlotId);
            GameManager.Instance.LoadCurrentScenes();
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