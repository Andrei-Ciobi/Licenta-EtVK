﻿using System;
using System.Collections.Generic;
using EtVK.Save_System_Module;
using EtVK.UI_Module.Components;
using EtVK.UI_Module.Core;
using UnityEngine.UIElements;

namespace EtVK.UI_Module.Main_Menu.Panels
{
    public class StartUi : BasePanel<MainMenuManager>
    {
        private Button backButton;

        public new class UxmlFactory : UxmlFactory<StartUi, UxmlTraits>
        {
        }

        public new class UxmlTraits :  BasePanel<MainMenuManager>.UxmlTraits
        {
        }

        protected override void OnGeometryChange(GeometryChangedEvent evt)
        {
            backButton = this.Q<Button>("back-button");

            backButton?.RegisterCallback<ClickEvent>(
                ev => PlayClickButtonSound(() => BaseUiManager.OpenPanelStart(this, BaseUiManager.Main)));

            base.OnGeometryChange(evt);
        }

        public override void Open()
        {
            var gameFiles = new List<SaveFileData>();
            if (GameSaveManager.Instance != null)
            {
                gameFiles = GameSaveManager.Instance.GetSaveFilesData();
            }

            var gameSlots = this.Q("slot-container")?.Query<GameSlotUi>("game-slot").ToList();

            if (gameSlots != null)
            {
                for (var index = 0; index < gameSlots.Count; ++index)
                {
                    //Check if it has a saved slot
                    var data = gameFiles.Find(x => x.SlotId == index + 1);

                    if (data != SaveFileData.Empty)
                    {
                        gameSlots[index].SetData(data.SlotId, data.LastSavedTime, data.GameLevel);
                        continue;
                    }

                    gameSlots[index].SetData(index + 1, DateTime.MinValue);
                }
            }

            base.Open();
        }
    }
}