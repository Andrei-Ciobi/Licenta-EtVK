﻿using System;
using EtVK.Core.Manager;
using EtVK.Core.Utyles;
using EtVK.Save_System_Module;
using UnityEngine;
using UnityEngine.UIElements;

namespace EtVK.UI_Module.Core
{
    public class GameSlotUi : VisualElement
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
            var currentLevel = level == GameLevel.None ? "New file" : GameManager.Instance.GameData.GetLevelName(level);
            slotNumber = id;
            slotName = currentLevel;
            forLoad = usedForLoad;
            iconName = usedForLoad ? "icon-load" : "icon-start";
            slotEmpty = level == GameLevel.None;
            
            this.Q<VisualElement>("img").style.backgroundImage = GameManager.Instance.GameData.GetLevelImg(level);
            this.Q<Label>("slot-name").text = $"Slot {id} | {currentLevel}";
            this.Q<VisualElement>(iconName).style.display = DisplayStyle.Flex;

            if (slotEmpty)
            {
                this.Q<Label>("last-saved").style.opacity = 0f;
                this.Q<Label>("progress").style.opacity = 0f;
                return;
            }

            this.Q<Label>("last-saved").text = "last saved  " + lastSaved.ToString("HH:mm dd/MM/yyyy");
            this.Q<Label>("progress").text = $"chapter status 1/{GameManager.Instance.GameData.TotalLevels}";
            this.Q<VisualElement>("img").style.unityBackgroundImageTintColor = Color.white;
        }

        public void Hide()
        {
            style.display = DisplayStyle.None;
        }

        private void OnGeometryChange(GeometryChangedEvent evt)
        {
            icon = this.Q<VisualElement>(iconName);

            icon.RegisterCallback<ClickEvent>(OnIconCLick);
            
            RegisterCallback<PointerOutEvent>(OnFocusExit);
            RegisterCallback<PointerOverEvent>(OnFocusEnter);
            RegisterCallback<ClickEvent>(ev => OnSlotClick());
            UnregisterCallback<GeometryChangedEvent>(OnGeometryChange);
        }


        private void OnFocusEnter(PointerOverEvent ev)
        {
            if (hover)
                return;

            if (forLoad && slotEmpty)
                return;

            hover = true;
            icon.style.opacity = 1f;
        }

        private void OnFocusExit(PointerOutEvent ev)
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
            if(!forLoad)
                return;
            
            ev.StopPropagation();
            
            GameSaveManager.Instance.DeleteSaveSlot(slotNumber);
            this.Hide();
        }

        private void LoadGame()
        {
            GameManager.Instance.StartLoadingScreen(GameUi.Hud);
            GameSaveManager.Instance.LoadSaveSlot(slotNumber);
            GameManager.Instance.LoadCurrentScenes();
        }

        private void StartNewGame()
        {
            if(!slotEmpty)
                return;
            
            GameSaveManager.Instance.StartNewSaveSlot(slotNumber);

            GameManager.Instance.StartLoadingScreen(GameUi.Hud);
            GameManager.Instance.LoadScene(SceneNames.CharacterCreation);
        }
    }
}