using EtVK.Core.Manager;
using EtVK.Save_System_Module;
using UnityEngine;

namespace EtVK.Levels_Module
{
    public class BaseLevelManager : MonoBehaviour
    {
        [SerializeField] private bool saveOnStart;
        [SerializeField] private Transform playerSpawnPoint;
        
        private void Awake()
        {
            if(GameManager.Instance == null)
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
            if(GameManager.Instance == null)
                return;
            
            GameManager.Instance.onFinishLoading -= LoadCurrentLevelData;
            GameManager.Instance.onFinishLoading -= SetPlayerPosition;
            GameManager.Instance.onFinishLoading -= SaveOnStartLevel;
        }
    }
}