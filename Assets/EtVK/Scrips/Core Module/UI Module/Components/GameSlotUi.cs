using System;
using EtVK.Core.Manager;
using EtVK.Core.Utyles;
using EtVK.Event_Module.Event_Types;
using EtVK.Save_System_Module;
using EtVK.UI_Module.Core;
using EtVK.UI_Module.Levels;
using EtVK.UI_Module.Main_Menu;
using UnityEngine;
using UnityEngine.UIElements;

namespace EtVK.UI_Module.Components
{
    public class GameSlotUi : BasePanel<MainMenuManager>
    {
        private VisualElement icon;

        private string iconName = "icon-start";
        private string slotName;

        private int slotNumber;

        private bool slotEmpty;
        private bool forLoad;
        private bool hover;

        public new class UxmlFactory : UxmlFactory<GameSlotUi, UxmlTraits>
        {
        }

        public new class UxmlTraits : VisualElement.UxmlTraits
        {
        }

        public GameSlotUi()
        {
            RegisterCallback<GeometryChangedEvent>(OnGeometryChange);
        }

        public void SetData(int id, DateTime lastSaved, GameLevel level = GameLevel.None,
            bool usedForLoad = false)
        {
            var uiData = uiManager.GameUiData.GetUiData<LevelsUiData>();
            var currentLevel = level == GameLevel.None ? "New file" : uiData.GetLevelName(level);
            slotNumber = id;
            slotName = currentLevel;
            forLoad = usedForLoad;
            iconName = usedForLoad ? "icon-load" : "icon-start";
            slotEmpty = level == GameLevel.None;

            this.Q<VisualElement>("img").style.backgroundImage = uiData.GetLevelImg(level);
            this.Q<Label>("slot-name").text = $"Slot {id} | {currentLevel}";
            this.Q<VisualElement>(iconName).style.display = DisplayStyle.Flex;

            if (slotEmpty)
            {
                this.Q<Label>("last-saved").style.opacity = 0f;
                this.Q<Label>("progress").style.opacity = 0f;
                return;
            }

            this.Q<Label>("last-saved").text = "last saved  " + lastSaved.ToString("HH:mm dd/MM/yyyy");
            this.Q<Label>("progress").text = $"chapter status 1/{uiData.TotalLevels}";
            this.Q<VisualElement>("img").style.unityBackgroundImageTintColor = Color.white;
        }

        public void Hide()
        {
            style.display = DisplayStyle.None;
        }

        protected override void OnGeometryChange(GeometryChangedEvent evt)
        {
            icon = this.Q<VisualElement>(iconName);
            var hoverContainer = this.Q<VisualElement>("hover-container");

            icon?.RegisterCallback<ClickEvent>(ev => PlayClickButtonSound(() => OnIconCLick(ev)));
            
            hoverContainer?.RegisterCallback<ClickEvent>(ev => PlayClickButtonSound());
            hoverContainer?.RegisterCallback<MouseOverEvent>(ev => PlayHoverButtonSound());

            RegisterCallback<MouseOutEvent>(OnFocusExit);
            RegisterCallback<MouseOverEvent>(OnFocusEnter);
            RegisterCallback<ClickEvent>(ev => PlayClickButtonSound(OnSlotClick));
            UnregisterCallback<GeometryChangedEvent>(OnGeometryChange);

            base.OnGeometryChange(evt);
        }


        private void OnFocusEnter(MouseOverEvent ev)
        {
            if (hover)
                return;

            if (forLoad && slotEmpty)
                return;

            hover = true;
            icon.style.opacity = 1f;

        }

        private void OnFocusExit(MouseOutEvent ev)
        {
            if (!hover)
                return;

            if (forLoad && slotEmpty)
                return;

            hover = false;
            icon.style.opacity = 0f;
        }

        private void OnSlotClick()
        {
            if (forLoad)
            {
                LoadGame();
            }
            else
            {
                StartNewGame();
            }
        }

        private void OnIconCLick(ClickEvent ev)
        {
            if (!forLoad)
                return;

            ev.StopPropagation();

            GameSaveManager.Instance.DeleteSaveSlot(slotNumber);
            Hide();
        }

        private void LoadGame()
        {
            GameManager.Instance.StartLoadingScreen(GameUi.Hud);
            GameSaveManager.Instance.LoadSaveSlot(slotNumber);
            GameManager.Instance.LoadCurrentScenes();
        }

        private void StartNewGame()
        {
            if (!slotEmpty)
                return;

            GameSaveManager.Instance.StartNewSaveSlot(slotNumber);

            GameManager.Instance.StartLoadingScreen(GameUi.CharacterCreation);
            GameManager.Instance.LoadScene(SceneNames.CharacterCreation);
        }
    }
}