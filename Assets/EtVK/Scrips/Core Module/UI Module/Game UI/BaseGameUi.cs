using EtVK.Utyles;
using UnityEngine;

namespace EtVK.UI_Module.Game_UI
{
    public abstract class BaseGameUi : MonoBehaviour
    {
        [SerializeField] private GameObject uiContainer;
        [SerializeField] private GameUi gameUiType;

        public GameObject UiContainer => uiContainer;
        public GameUi GameUiType => gameUiType;

        public abstract void Initialize(GameUiManager manager);
        public abstract void OnOpen();
        public abstract void OnClose();
        
    }
}