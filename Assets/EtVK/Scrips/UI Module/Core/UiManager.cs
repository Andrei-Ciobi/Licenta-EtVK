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
            var uiManager = uiDocument.rootVisualElement.Q<VisualElement>("root-manager") as IBaseUiManager;
            uiManager?.OnClose();
            uiDocument.visualTreeAsset = gameUiData.LoadingUi;
        }

        private void DeactivateLoadingScreen()
        {
            if (selectedUi == null)
            {
                Debug.LogError("No Ui set after loading screen ended");
            }
            
            uiDocument.visualTreeAsset = selectedUi;
            var uiManager = uiDocument.rootVisualElement.Q<VisualElement>("root-manager") as IBaseUiManager;
            uiManager?.OnOpen();
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

        public void OpenGameUi(GameUi gameUi)
        {
            var ui = gameUiData.GetUi(gameUi);
            if (ui == null)
            {
                Debug.Log($"No VisualTreeAsset of type '{gameUi}' found");
                return;
            }
            
            var uiManager = uiDocument.rootVisualElement.Q<VisualElement>("root-manager") as IBaseUiManager;
            uiManager?.OnClose();
            
            uiDocument.visualTreeAsset = ui;
            uiManager = uiDocument.rootVisualElement.Q<VisualElement>("root-manager") as IBaseUiManager;
            uiManager?.OnOpen();
        }
        
        public T GetRootManager<T>() where T : VisualElement
        {
            return uiDocument.rootVisualElement.Q<T>("root-manager");
        }

        private void OnStartInitialize()
        {
            GameManager.Instance.onLateFinishLoading += DeactivateLoadingScreen;
            if(GameManager.Instance.IsFullGame)
                return;
            
            var uiManager = uiDocument.rootVisualElement.Q<VisualElement>("root-manager") as IBaseUiManager;
            uiManager?.OnOpen();
        }

        private void OnDestroy()
        {
            GameManager.Instance.onLateFinishLoading -= DeactivateLoadingScreen;
        }
    }
}