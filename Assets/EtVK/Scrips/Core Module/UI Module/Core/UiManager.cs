using System;
using System.Collections.Generic;
using EtVK.Core.Manager;
using EtVK.Core.Utyles;
using EtVK.UI_Module.Main_Menu;
using UnityEngine;
using UnityEngine.UIElements;

namespace EtVK.UI_Module.Core
{
    public class UiManager : MonoBehaviour
    {
        [SerializeField] private UIDocument uiDocument;
        [SerializeField] private VisualTreeAsset loadingUi;
        [SerializeField] private List<SerializableSet<GameUi, VisualTreeAsset>> uiList;
        

        public UIDocument UIDocument => uiDocument;

        private VisualTreeAsset selectedUi;

        private void Start()
        {
            OnStartInitialize();
        }

        public void ActivateLoadingScreen()
        {
            uiDocument.visualTreeAsset = loadingUi;
        }

        private void DeactivateLoadingScreen()
        {
            if (selectedUi == null)
            {
                Debug.LogError("No Ui set after loading screen ended");
            }
            
            uiDocument.visualTreeAsset = selectedUi;
            selectedUi = null;
        }

        public void ChangeGameUi(GameUi gameUi)
        {
            if(selectedUi != null)
            {
                Debug.Log($"New Ui change while in loading screen {gameUi}. !Ignored!");
                return;
            }
            
            var ui = uiList.Find(x => x.GetKey() == gameUi)?.GetValue();

            if (ui == null)
            {
                Debug.Log($"No VisualTreeAsset of type '{gameUi}' found");
                return;
            }

            selectedUi = ui;
        }
        
        public T GetRootManager<T>() where T : VisualElement
        {
            return uiDocument.rootVisualElement.Q<T>("root-manager");
        }

        private void OnStartInitialize()
        {
            GameManager.Instance.onLateFinishLoading += DeactivateLoadingScreen;
        }

        private void OnDestroy()
        {
            GameManager.Instance.onLateFinishLoading -= DeactivateLoadingScreen;
        }
    }
}