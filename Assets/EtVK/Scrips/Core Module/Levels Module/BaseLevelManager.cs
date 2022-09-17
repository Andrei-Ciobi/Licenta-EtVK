using EtVK.Core;
using EtVK.Save_System_Module;
using EtVK.Utyles;
using UnityEngine;

namespace EtVK.Levels_Module
{
    public class BaseLevelManager : MonoBehaviour, IFullGameComponent
    {
        [SerializeField] private bool startFullGame;
        [SerializeField] private bool saveOnStart;
        [SerializeField] private Transform playerSpawnPoint;
        
        public bool StartFullGame { get => startFullGame; set => startFullGame = value; }
        private void Awake()
        {
            if(!startFullGame)
                return;
            
            GameManager.Instance.onFinishLoading += LoadCurrentLevelData;
            GameManager.Instance.onFinishLoading += SetPlayerPosition;
            GameManager.Instance.onFinishLoading += SaveOnStartLevel;
            
        }

        private void LoadCurrentLevelData()
        {
            GameManager.Instance.PreventLoad = true;
            GameSaveManager.Instance.Load();
            GameManager.Instance.PreventLoad = false;
        }

        private void SaveOnStartLevel()
        {
            if(!saveOnStart)
                return;
            
            GameSaveManager.Instance.Save();
        }

        [ContextMenu("Set Player Position")]
        private void SetPlayerPosition()
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            if (player == null)
            {
                Debug.Log("No Player found in the game");
            }
            else
            {
                player.transform.position = playerSpawnPoint.transform.position;
            }
        }

        private void OnDestroy()
        {
            GameManager.Instance.onFinishLoading -= LoadCurrentLevelData;
            GameManager.Instance.onFinishLoading -= SetPlayerPosition;
            GameManager.Instance.onFinishLoading -= SaveOnStartLevel;
        }
    }
}