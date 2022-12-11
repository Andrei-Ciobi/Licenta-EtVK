using System.Collections.Generic;
using EtVK.Core.Manager;
using EtVK.Event_Module.Event_Types;
using EtVK.Event_Module.Listeners;
using EtVK.Input_Module;
using EtVK.UI_Module.Core;
using EtVK.UI_Module.Game_Menu;
using EtVK.Upgrades_Module.Core;
using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

namespace EtVK.UI_Module.Hud.Panels
{
    public class UpgradesContainerUi : BasePanel<HudManager>
    {
        private VisualElement upgradesContainer;
        private UpgradesUiEventListener upgradesListener;

        private List<UpgradeUi> currentUpgrades = new();

        public new class UxmlFactory : UxmlFactory<UpgradesContainerUi, UxmlTraits>
        {
        }

        public new class UxmlTraits : BasePanel<HudManager>.UxmlTraits
        {
            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);
                var ate = ve as UpgradesContainerUi;
                
                ate.Clear();
                var upgradesContainer = new VisualElement();
                upgradesContainer.AddToClassList("upgrades-container");
                ate.upgradesContainer = upgradesContainer;
                ate.Add(upgradesContainer);
                ate.Initialize();

                // var x = Resources.LoadAll<CommonUpgradeData>("Upgrades/Common");
                // for (int i = 0; i < 3; i++)
                // {
                //    ate.upgradesContainer.Add(new UpgradeUi(x[i], ate.OnUpgradeClick));
                // }
            }

            public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription { get{yield break;} }
        }

        private void DisplayUpgrades(UpgradesUiData data)
        {
            Debug.Log("In display");
            foreach (var upgradeData in data.UpgradeList)
            {
                var upgrade = new UpgradeUi(upgradeData, OnUpgradeClick);
                upgradesContainer.Add(upgrade);
                currentUpgrades.Add(upgrade);
            }
            
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            GameManager.Instance.IsGamePaused = true;
            InputManager.Instance.DisablePlayerActionMap();
            GetUiData<GameMenuUiData>()?.CameraInputEvent.Invoke(false);
            OpenInstant();
        }

        private void OnUpgradeClick(BaseUpgradeData upgradeData)
        {
            GetUiData<HudUiData>().SelectUpgradeEvent.Invoke(upgradeData);
            
            foreach (var upgradeUi in currentUpgrades)   
            {
                upgradesContainer.Remove(upgradeUi);
            }
            
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            GameManager.Instance.IsGamePaused = false;
            InputManager.Instance.EnablePlayerActionMap();
            GetUiData<GameMenuUiData>()?.CameraInputEvent.Invoke(true);
            CloseInstant();
        }

        private void Initialize()
        {
            if(upgradesListener != null)
                return;

            upgradesListener = new UpgradesUiEventListener(GetUiData<HudUiData>().DisplayUpgradesEvent);
            upgradesListener.AddCallback(DisplayUpgrades);
            
            CloseInstant();
        }

    }
}