using EtVK.Core.Manager;
using EtVK.Core.Utyles;
using UnityEngine;
using UnityEngine.UIElements;

namespace EtVK.UI_Module.Core
{
    public class UiManager : MonoBehaviour
    {
        [SerializeField] private UIDocument uiDocument;
        [SerializeField] private GameUiData gameUiData;
        
        public UIDocument UIDocument => uiDocument;
        public GameUiData GameUiData => gameUiData;
        
        private VisualTreeAsset selectedUi;

        private void Start()
        {
            OnStartInitialize();
        }

        public void ActivateLoadingScreen()
        {
            uiDocument.visualTreeAsset = gameUiData.LoadingUi;
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

            var ui = gameUiData.GetUi(gameUi);

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