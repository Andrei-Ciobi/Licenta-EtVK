using System.Collections.Generic;
using System.Linq;
using EtVK.Core;
using EtVK.Input_Module;
using EtVK.Utyles;
using UnityEngine;

namespace EtVK.UI_Module.Game_UI
{
    public class GameUiManager : MonoBehaviour
    {

        private Dictionary<GameUi, BaseGameUi> gameUiDict = new();
        private BaseGameUi currentGameUi;

        private void Awake()
        {
            Initialize();
        }

        private void Update()
        {
            if (InputManager.Instance.Player.TapEscape)
            {
                OpenGameUi(GameUi.Menu);
            }
        }

        public void OpenGameUi(GameUi gameUi)
        {
            if (!gameUiDict.ContainsKey(gameUi))
            {
                Debug.Log($"No game ui of type: {gameUi} found");
                return;
            }

            if (currentGameUi != null)
            {
                currentGameUi.OnClose();
                currentGameUi.UiContainer.SetActive(false);
            }

            currentGameUi = gameUiDict[gameUi];
            currentGameUi.OnOpen();
            currentGameUi.UiContainer.SetActive(true);
        }

        public void CloseGameUi(GameUi gameUi)
        {
            if (!gameUiDict.ContainsKey(gameUi))
            {
                Debug.Log($"No game ui of type: {gameUi} found");
                return;
            }

            if (currentGameUi != gameUiDict[gameUi])
            {
                Debug.Log(
                    $"Different game ui open when it should be de same current={currentGameUi.gameObject.name},  from input={gameUiDict[gameUi].gameObject.name}");
            }
            
            currentGameUi.OnClose();
            currentGameUi.UiContainer.SetActive(false);
        }
        
        private void OnFinishLoading()
        {
            InputManager.Instance.DisableUIActionMap();
            InputManager.Instance.EnablePlayerActionMap();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Initialize()
        {
            GameManager.Instance.onLateFinishLoading += OnFinishLoading;
            
            var uiList = GetComponentsInChildren<BaseGameUi>(true).ToList();
            foreach (var gameUi in uiList)
            {
                if (gameUiDict.ContainsKey(gameUi.GameUiType))
                {
                    Debug.Log(
                        $"Multiple game ui of type: {gameUi.GameUiType} \n This instance is ignored: {gameUi.gameObject.name}");
                    continue;
                }

                gameUiDict.Add(gameUi.GameUiType, gameUi);
                gameUi.Initialize(this);
            }
        }

        private void OnDestroy()
        {
            GameManager.Instance.onLateFinishLoading -= OnFinishLoading;
        }
    }
}